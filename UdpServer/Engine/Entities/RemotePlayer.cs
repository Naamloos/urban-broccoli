using System.IO;
using Broccoli.Engine.Input;
using Microsoft.Xna.Framework;
using UdpMistro;

namespace UdpServer.Engine.Entities
{
    public class RemotePlayer : Player
    {
        public RemotePlayer(Rectangle size, Input input) : base(null, size, input)
        {
        }
    }
}