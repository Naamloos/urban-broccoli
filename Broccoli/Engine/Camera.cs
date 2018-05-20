using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Broccoli.Engine.Entities;

namespace Broccoli.Engine
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(GameObject target)
        {
            var pos = Matrix.CreateTranslation(
                -target.Position.X - (target.HitBox.Width / 2),
                -target.Position.Y - (target.HitBox.Height / 2),
                0);
            var offset = Matrix.CreateTranslation(
                BroccoliGame.ScreenWidth / 2,
                BroccoliGame.ScreenHeight / 2,
                0);
            Transform = pos * offset;
        }
    }
}
