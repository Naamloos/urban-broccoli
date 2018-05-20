using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UdpMistro
{
    public abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<UdpMessage> Receive()
        {
            var result = await Client.ReceiveAsync();
            var opcode = (Opcode) result.Buffer[0];
            return new UdpMessage
            {
                Opcode = opcode,
                Message = Maestro.DecodeMessage(opcode, result.Buffer, sizeof(byte), result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }

    public struct UdpMessage
    {
        public Opcode Opcode;
        public object Message;
        public IPEndPoint Sender;
    }

    internal class Maestro
    {
        private static readonly ImmutableDictionary<Opcode, Func<BinaryReader, IOperation>> Decoders;

        static Maestro()
        {
            var builder = ImmutableDictionary.CreateBuilder<Opcode, Func<BinaryReader, IOperation>>();
            builder.Add(Opcode.ClientConnect, ClientConnect.Parse);
            Decoders = builder.ToImmutable();
        }

        public static object DecodeMessage(Opcode op, byte[] b, int off, int len)
        {
            using (var memoryStream = new MemoryStream(b, off, len))
            using (var binaryReader = new BinaryReader(memoryStream))
                return Decoders[op](binaryReader);
        }
    }

    public class ClientConnect : IOperation
    {
        public ushort Version;
        public char[] Nick;

        public static ClientConnect Parse(BinaryReader reader)
        {
            return new ClientConnect
            {
                Version = reader.ReadUInt16(),
                Nick = reader.ReadChars(reader.ReadUInt16())
            };
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write((ushort) Nick.Length);
            writer.Write(Nick);
        }
    }

    public interface IOperation
    {
        void Write(BinaryWriter writer);
    }

    public enum Opcode : byte
    {
        // client => server
        ClientConnect, // args: <version: ushort> <nick: string>
        ClientGetEntities, // args: <ourEntityLength: ushort> <ourEntityIds: ushort...>

        // server => client
        ServerConnectSuccess, // args: <motd: string> 
        ServerConnectFail, // args <errorMessage: string>
        ServerReturnEntities, // args: <addedEntityLength: ushort> <addedEntities: (entityType ushort, entityData)...> <removedEntityLength: ushort> <removedEntityIds: ushort...>
    }
}
