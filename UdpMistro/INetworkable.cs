using System.Collections.Generic;
using System.IO;

namespace UdpMistro
{
    public interface INetworkable
    {
        void Serialize(BinaryWriter writer);
    }

    public delegate INetworkable ReadOperation(BinaryReader reader);
}