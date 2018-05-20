using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Engine.Entities
{
    public class Player : GameObject
    {
        private float Width;
        private float Height;
        public override Rectangle HitBox { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height); } }

        public Player(Rectangle size, Texture2D texture) : base(texture)
        {
            Width = size.Width;
            Height = size.Height;
            Position = size.Location.ToVector2();
            Texture = texture;
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
            //make it move or something
        }
    }
}
