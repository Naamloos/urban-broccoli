using System.IO;

namespace UdpMistro
{
    public struct ClientConnect : INetworkable
    {
        public readonly ushort Version;
        public readonly char[] Nick;

        public ClientConnect(ushort version, char[] nick)
        {
            Version = version;
            Nick = nick;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write((ushort) Nick.Length);
            writer.Write(Nick);
        }
        
        public static INetworkable Deserialize(BinaryReader reader)
        {
            return new ClientConnect(
                version: reader.ReadUInt16(),
                nick: reader.ReadChars(reader.ReadUInt16())
            );
        }
    }
}