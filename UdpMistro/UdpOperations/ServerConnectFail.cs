using System.IO;

namespace UdpMistro
{
    public struct ServerConnectFail : INetworkable
    {
        public readonly char[] ErrorMessage;

        public ServerConnectFail(char[] errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(ErrorMessage.Length);
            writer.Write(ErrorMessage);
        }
        
        public static INetworkable Deserialize(BinaryReader reader)
        {
            return new ServerConnectFail(
                errorMessage: reader.ReadChars(reader.ReadUInt16())
            );
        }
    }
}