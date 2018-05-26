using System.IO;

namespace UdpMistro
{
    public struct ServerSendSnapshot : INetworkable
    {
        public readonly ushort Size;
        public readonly BinaryReader ReceivingData;
        public readonly MemoryStream SendingData;

        public ServerSendSnapshot(ushort size, BinaryReader receivingData)
        {
            Size = size;
            ReceivingData = receivingData;
            SendingData = null;
        }

        public ServerSendSnapshot(ushort size, MemoryStream sendingData)
        {
            Size = size;
            ReceivingData = null;
            SendingData = sendingData;
        }

        public void Serialize(BinaryWriter writer)
        {
            if (SendingData == null)
                throw new InvalidDataException("Cannot serialize a ServerSendSnapshot meant for deserializing");
            
            writer.Write(Size);
            writer.Write(SendingData.GetBuffer(), 0, (int)SendingData.Length);
        }

        public static INetworkable Deserialize(BinaryReader reader)
        {
            return new ServerSendSnapshot(
                size: reader.ReadUInt16(),
                receivingData: reader
            );
        }
    }
}