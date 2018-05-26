using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Broccoli.Engine;
using Broccoli.Engine.Input;
using UdpMistro;

namespace Broccoli.Engine.Input
{
    public class Player : GameObject
    {
        private float Width;
        private float Height;
        public Vector2 InputVelocity = Vector2.Zero;
        public override Rectangle HitBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        public Input Input;

        private float _speed;

        public Player(Texture2D texture, Rectangle size, Input input, ushort id) : base(texture, id)
        {
            Width = size.Width;
            Height = size.Height;
            Position = size.Location.ToVector2();
            Input = input;
            _speed = 7;
        }

        public override void Update(GameTime gameTime, List<GameObject> entities)
        {
            Input.Update();
            
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(Input.XAxis < 0.1 && Input.XAxis > -0.1)
                InputVelocity.X /= 1.1f;
            if (InputVelocity.X < 0.05 && InputVelocity.X > -0.05)
                InputVelocity.X = 0;

            InputVelocity.X += Input.XAxis * _speed;
            InputVelocity.X = MathHelper.Clamp(InputVelocity.X,-_speed,_speed);

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;
                if (Velocity.X > 0 && IsTouchingLeft(sprite))
                {

                    this.Velocity.X = 0;
                    InputVelocity.X = sprite.HitBox.Left - HitBox.Width;
                }

                if (Velocity.X < 0 && IsTouchingRight(sprite))
                {
                    this.Velocity.X = 0;
                    InputVelocity.X = sprite.HitBox.Right;
                }
            }

            Position += Velocity / 10 * delta + InputVelocity / 10 * delta;
            // need to do these seperately otherwise the player will spaz out and get stuck in corners
            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if (Velocity.Y < 0 && IsTouchingBottom(sprite))
                {
                    this.Velocity.Y = 0;
                    InputVelocity.Y = sprite.HitBox.Bottom;
                }

                if (Velocity.Y > 0 && IsTouchingTop(sprite))
                {
                    this.Velocity.Y = 0;
                    InputVelocity.Y = sprite.HitBox.Top - HitBox.Height;
                }

            }

            Position += 
                Velocity / 10 * delta+
                InputVelocity / 10 * delta;
        }
        
        public override void Serialize(BinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(Width);
            writer.Write(Height);
            writer.Write(InputVelocity.X);
            writer.Write(InputVelocity.Y);
            writer.Write(HitBox.X);
            writer.Write(HitBox.Y);
            writer.Write(HitBox.Width);
            writer.Write(HitBox.Height);
            Input.Serialize(writer);
        }
    }
}