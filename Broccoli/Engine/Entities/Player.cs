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
        public override Vector2 OverallVelocity { get { return InputVelocity + Velocity + Gravity; } }

        public bool IsOnGround { get; private set; }
        public InputHandler Input;
        public Vector2 InputVelocity;
        private readonly float Speed;
        public int JumpCount;
        private bool _canJump = true;

        public override void ResetVelocity()
        {
            Velocity = Vector2.Zero;
            InputVelocity = Vector2.Zero;
            Gravity = Vector2.Zero;
        }

        public Player(Texture2D texture,Rectangle size,InputHandler input) : base(texture)
        {
            Width = size.Width;
            Height = size.Height;
            Position = size.Location.ToVector2();
            Texture = texture;
            Input = input;
            Speed = 7;
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
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Move(delta);
            var pos = Collide(delta,entities);

            pos += OverallVelocity / 10 * delta;
            Position = pos;
        }

        private void Move(float delta)
        {
            if (Input.JumpPress && JumpCount >= 1)
            {
                Gravity = Vector2.Zero;
                InputVelocity.Y = -1*delta;
                JumpCount -= 1;
            }

            if(IsOnGround)
                JumpCount = 2;

            if (Input.Jump && OverallVelocity.Y < 0)
                Gravity.Y += 0.02f * delta;
            else
                Gravity.Y += 0.05f * delta;
            Gravity.Y = MathHelper.Clamp(Gravity.Y, 0, 40);

            if ((Input.XAxis < 0.1 && Input.XAxis > -0.1) && InputVelocity.X != 0)
                InputVelocity.X -= (InputVelocity.X / 3.5f) / 10 * delta;
            if (InputVelocity.X < 0.05 && InputVelocity.X > -0.05)
                InputVelocity.X = 0;

            InputVelocity.X += Input.XAxis * Speed;
            InputVelocity.X = MathHelper.Clamp(InputVelocity.X, -Speed, Speed);
        }

        private Vector2 Collide(float delta,List<GameObject> entities)
        {
            var pos = Position;

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if ((OverallVelocity.X > 0 && IsTouchingLeft(sprite)))
                {
                    Velocity.X = 0;
                    InputVelocity.X = 0;
                    pos.X = sprite.HitBox.Left - HitBox.Width;
                }

                if ((OverallVelocity.X < 0 && IsTouchingRight(sprite)))
                {
                    Velocity.X = 0;
                    InputVelocity.X = 0;
                    pos.X = sprite.HitBox.Right;
                }
            }

            Position = pos + OverallVelocity/10 *delta;

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if ((OverallVelocity.Y < 0 && IsTouchingBottom(sprite)))
                {
                    Velocity.Y = 0;
                    InputVelocity.Y = 0;
                    pos.Y = sprite.HitBox.Bottom;
                }

                if ((OverallVelocity.Y > 0 && IsTouchingTop(sprite)))
                {
                    JumpCount = 2;
                    IsOnGround = true;
                    Gravity.Y = 0;
                    Velocity.Y = 0;
                    InputVelocity.Y = 0;
                    pos.Y = sprite.HitBox.Top - HitBox.Height;
                }
                else
                    IsOnGround = false;
            }
            return pos;
        }
    }
}