using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Broccoli.Engine;
using Broccoli.Engine.Input;

namespace Broccoli.Engine.Entities
{
    public class Player : GameObject
    {
        private readonly float Width;
        private readonly float Height;
        public override Rectangle HitBox { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); } }

        public bool IsOnGround { get; private set; }
        public InputHandler Input;
        private readonly float Speed;
        public int JumpCount;
        private bool _canJump = true;

        public Player(Texture2D texture,Rectangle size,InputHandler input) : base(texture)
        {
            Width = size.Width;
            Height = size.Height;
            Position = size.Location.ToVector2();
            Texture = texture;
            Input = input;
            Speed = 700;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle _rect;
            _rect = new Rectangle(HitBox.X-((Texture.Width/2)-(HitBox.Width/2)), HitBox.Y - ((Texture.Height) - (HitBox.Height)), Texture.Width, Texture.Height);
            spriteBatch.Draw(Texture, _rect, Color.White);

            Texture2D _texture;
            _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { new Color(255,0,0,10) });
            //red is hitbox, black is texture
            spriteBatch.Draw(_texture, HitBox, Color.White);
        }

        public override void Update(GameTime gameTime, List<GameObject> entities)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move(delta);
            Velocity = Gravity + Velocity;
            var pos = Collide(delta,entities);

            
            pos += Velocity * delta;
            Position = pos;
        }

        private void Move(float delta)
        {
            if (Input.Jump && Velocity.Y < 0)
                Gravity.Y += 2000 * (float)(Math.Pow((double)delta, 2));
            else
                Gravity.Y += 8000 * (float)(Math.Pow((double)delta, 2));

            if (Input.JumpPress && JumpCount >= 1)
            {
                Gravity = Vector2.Zero;
                Velocity.Y = -500;
                JumpCount -= 1;
            }

            if(IsOnGround)
                JumpCount = 2;

            if ((Input.XAxis < 0.1 && Input.XAxis > -0.1) && Velocity.X != 0)
                Velocity.X = 0;

            Velocity.X += Input.XAxis * Speed;
            Velocity.X = MathHelper.Clamp(Velocity.X, -Speed, Speed);
        }

        private Vector2 Collide(float delta,List<GameObject> entities)
        {
            var pos = Position;

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if ((Velocity.X > 0 && IsTouchingLeft(sprite, delta)))
                {
                    Velocity.X = 0;
                    pos.X = sprite.HitBox.Left - HitBox.Width;
                }

                if ((Velocity.X < 0 && IsTouchingRight(sprite, delta)))
                {
                    Velocity.X = 0;
                    pos.X = sprite.HitBox.Right;
                }
            }

            Velocity = Gravity + Velocity;
            Position = pos + Velocity * delta;

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if ((Velocity.Y < 0 && IsTouchingBottom(sprite, delta)))
                {
                    Velocity.Y = 0;
                    pos.Y = sprite.HitBox.Bottom;
                }

                if ((Velocity.Y > 0 && IsTouchingTop(sprite, delta)))
                {
                    JumpCount = 2;
                    IsOnGround = true;
                    Velocity.Y = 0;
                    Gravity = Vector2.Zero;
                    pos.Y = sprite.HitBox.Top - HitBox.Height;
                }
                else
                    IsOnGround = false;
            }
            return pos;
        }
    }
}