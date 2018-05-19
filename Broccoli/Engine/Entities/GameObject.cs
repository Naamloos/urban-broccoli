using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Engine.Entities
{
	public class GameObject
	{
		public float Width;
		public float Height;
		public Rectangle HitBox { get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); } }
		public float X;
		public float Y;

		public GameObject(float width, float height)
		{
			this.Width = width;
			this.Height = height;
			this.X = 0;
			this.Y = 0;
		}
	}
}
