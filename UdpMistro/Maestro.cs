using System.Collections.Immutable;
using System.IO;

namespace UdpMistro
{
    internal class Maestro
    {
        private static readonly ImmutableDictionary<Opcode, ReadOperation> Decoders;

        static Maestro()
        {
            var builder = ImmutableDictionary.CreateBuilder<Opcode, ReadOperation>();
            //
            builder[Opcode.ClientConnect] = ClientConnect.Deserialize;
            builder[Opcode.ServerConnectSuccess] = ServerConnectSuccess.Deserialize;
            builder[Opcode.ServerConnectFail] = ServerConnectFail.Deserialize;
            builder[Opcode.ServerSendSnapshot] = ServerSendSnapshot.Deserialize;
            //
            Decoders = builder.ToImmutable();
        }

        public static INetworkable DecodeMessage(Opcode op, byte[] b, int off, int len)
        {
            using (var memoryStream = new MemoryStream(b, off, len))
            using (var binaryReader = new BinaryReader(memoryStream))
                return Decoders[op](binaryReader);
        }

        public static void EncodeMessage(BinaryWriter writer, Opcode op, INetworkable obj)
        {
            writer.Write((byte)op);
            obj.Serialize(writer);
        }
    }
}