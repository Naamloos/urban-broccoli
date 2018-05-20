using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Engine.Entities
{
	public class GameObject
	{
        public Vector2 Position;
        public Vector2 Velocity;
        public Texture2D Texture;
        public bool Collision;
		public virtual Rectangle HitBox { get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); } }

        public GameObject(Texture2D texture)
        {
            Texture = texture;
            Collision = true;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture,HitBox,Color.White);
        }

        public virtual void Update(GameTime gameTime, List<GameObject> entities)
        {

        }
    }
}
