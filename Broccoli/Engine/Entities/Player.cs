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
        private float Width;
        private float Height;
        public Vector2 InputVelocity = Vector2.Zero;
        public override Rectangle HitBox { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); } }
        public Vector2 OverallVelocity { get { return InputVelocity + Velocity + new Vector2(0, _gravity); } }
        public InputHandler Input;

        private float _speed;
        private float _gravity;

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
            Vector2 pos = Position;
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _gravity += 0.5f;

            if ((Input.XAxis < 0.1 && Input.XAxis > -0.1) && InputVelocity.X != 0)
                InputVelocity.X -= (InputVelocity.X / 3.5f) / 10 * delta;
            if (InputVelocity.X < 0.05 && InputVelocity.X > -0.05)
                InputVelocity.X = 0;

            InputVelocity.X += Input.XAxis * _speed;
            InputVelocity.X = MathHelper.Clamp(InputVelocity.X, -_speed, _speed);


            if ((Input.YAxis < 0.1 && Input.YAxis > -0.1) && InputVelocity.Y != 0)
                InputVelocity.Y -= (InputVelocity.Y / 3.5f) / 10 * delta;
            if (InputVelocity.Y < 0.05 && InputVelocity.Y > -0.05)
                InputVelocity.Y = 0;

            InputVelocity.Y += Input.YAxis * _speed;
            InputVelocity.Y = MathHelper.Clamp(InputVelocity.Y, -_speed, _speed);

            if (Input.Jump)
                InputVelocity.Y = -10;

            foreach (var sprite in entities)
            {
                if (sprite == this)
                    continue;

                if (sprite.Collision == true && Collision == true)
                {   

                }
            }

            pos += Velocity / 10 * delta;
            pos += InputVelocity / 10 * delta;
            pos += new Vector2(0, _gravity) / 10 * delta;
            Position = pos;
        }
    }
}