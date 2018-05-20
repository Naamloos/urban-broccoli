using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpMistro
{
    abstract class UdpBase
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
            return new UdpMessage()
            {
                Opcode = opcode,
                Message = Maestro.DecodeMessage(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }

    internal struct UdpMessage
    {
        public Opcode Opcode;
        public object Message;
        public IPEndPoint Sender;
    }

    internal class Maestro
    {
        private static readonly System.Collections.Immutable.ImmutableDictionary<>

        public static object DecodeMessage(byte[] b, int pos, int off)
        {
            
        }
    }

    internal enum Opcode : byte
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
