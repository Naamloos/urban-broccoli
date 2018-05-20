using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Broccoli.Engine.Entities
{
    public class RemoteGameObject : GameObject
    {
        /// <summary>
        /// 
        /// </summary>
        public ushort Id = ushort.MaxValue;

        public RemoteGameObject(Texture2D texture) : base(texture)
        {
        }
    }
}
