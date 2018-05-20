using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Engine
{
	/// <summary>
	/// This class is for handling inputs from a sp
	/// </summary>
	public class InputHandler
	{
		public float XAxis { get; internal set; } = 0;
		public float YAxis { get; internal set; } = 0;
		public bool Start { get; internal set; } = false;
		public bool Select { get; internal set; } = false;
		public bool Jump { get; internal set; } = false;
		public bool Dash { get; internal set; } = false;
		public bool Attack1 { get; internal set; } = false;
		public bool Attack2 { get; internal set; } = false;
		public bool Block { get; internal set; } = false;

		public InputHandler()
		{

		}

		public void Update()
		{
			var ks = Keyboard.GetState();
			var gs = GamePad.GetState(PlayerIndex.One);

			// Axis controls

			if (ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.Down))
				YAxis = 0; // If both are pressed, they cancel out and Y is 0
			else if (ks.IsKeyDown(Keys.Down))
				YAxis = 1; // Down sets Y to 1
			else if (ks.IsKeyDown(Keys.Up))
				YAxis = -1; // Up sets Y to -1
			else
				YAxis = 0; // Else, nothing.

			if (ks.IsKeyDown(Keys.Left) && ks.IsKeyDown(Keys.Right))
				XAxis = 0; // If both are pressed, they cancel out and X is 0
			else if (ks.IsKeyDown(Keys.Left))
				XAxis = -1; // Left sets X to -1
			else if (ks.IsKeyDown(Keys.Right))
				XAxis = 1; // Right sets X to 1
			else
				XAxis = 0; // Else, nothing.

			if (gs.ThumbSticks.Left.X != 0)
				XAxis = gs.ThumbSticks.Left.X;
			if (gs.ThumbSticks.Left.Y != 0)
				YAxis = gs.ThumbSticks.Left.Y;
		}
	}
}
