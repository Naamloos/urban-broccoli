using System.IO;

namespace UdpMistro
{
    public struct ServerConnectSuccess : INetworkable
    {
        public readonly char[] MessageOfTheDay;

        public ServerConnectSuccess(char[] motd)
        {
            MessageOfTheDay = motd;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(MessageOfTheDay.Length);
            writer.Write(MessageOfTheDay);
        }
        
        public static INetworkable Deserialize(BinaryReader reader)
        {
            return new ServerConnectSuccess(
                motd: reader.ReadChars(reader.ReadUInt16())
            );
        }
    }
}