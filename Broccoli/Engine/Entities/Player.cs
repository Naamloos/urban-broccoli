using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Broccoli.Engine;

namespace Broccoli.Engine.Entities
{
    public class Player : GameObject
    {
        private float Width;
        private float Height;
        public Vector2 InputVelocity = Vector2.Zero;
        public override Rectangle HitBox { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); } }
        public InputHandler Input;

        private float _speed;

        public Player(Texture2D texture,Rectangle size,InputHandler input) : base(texture)
        {
            Width = size.Width;
            Height = size.Height;
            Position = size.Location.ToVector2();
            Texture = texture;
            Input = input;
            _speed = 7;
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
                if ((Velocity.X > 0 && IsTouchingLeft(sprite)))
                {

                    this.Velocity.X = 0;
                    InputVelocity.X = sprite.HitBox.Left - HitBox.Width;
                }

                if ((Velocity.X < 0 && IsTouchingRight(sprite)))
                {
                    this.Velocity.X = 0;
                    InputVelocity.X = sprite.HitBox.Right;
                }
            }

            Position += ((Velocity / 10) * delta) + ((InputVelocity / 10) * delta);
            // need to do these seperately otherwise the player will spaz out and get stuck in corners
            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if ((Velocity.Y < 0 && IsTouchingBottom(sprite)))
                {
                    this.Velocity.Y = 0;
                    InputVelocity.Y = sprite.HitBox.Bottom;
                }

                if ((Velocity.Y > 0 && IsTouchingTop(sprite)))
                {
                    this.Velocity.Y = 0;
                    InputVelocity.Y = sprite.HitBox.Top - HitBox.Height;
                }

            }

            Position += 
                (((Velocity) / 10) * delta)+
                ((InputVelocity / 10) * delta);
        }
    }
}