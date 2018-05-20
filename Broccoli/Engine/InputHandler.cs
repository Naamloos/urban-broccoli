using Broccoli.Engine.Entities;
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

		private Keybinds _keybinds;

		public InputHandler(Keybinds keybinds)
		{
			this._keybinds = keybinds;
		}

		public void Update()
		{
			var ks = Keyboard.GetState();
			var gs = GamePad.GetState(PlayerIndex.One);

			// Axis controls
			if (ks.IsKeyDown(_keybinds.Up) && ks.IsKeyDown(_keybinds.Down))
				YAxis = 0; // If both are pressed, they cancel out and Y is 0
			else if (ks.IsKeyDown(_keybinds.Down))
				YAxis = 1; // Down sets Y to 1
			else if (ks.IsKeyDown(_keybinds.Up))
				YAxis = -1; // Up sets Y to -1
			else
				YAxis = 0; // Else, nothing.

			if (ks.IsKeyDown(_keybinds.Left) && ks.IsKeyDown(_keybinds.Right))
				XAxis = 0; // If both are pressed, they cancel out and X is 0
			else if (ks.IsKeyDown(_keybinds.Left))
				XAxis = -1; // Left sets X to -1
			else if (ks.IsKeyDown(_keybinds.Right))
				XAxis = 1; // Right sets X to 1
			else
				XAxis = 0; // Else, nothing.

			if (gs.ThumbSticks.Left.X != 0)
				XAxis = gs.ThumbSticks.Left.X;
			if (gs.ThumbSticks.Left.Y != 0)
				YAxis = gs.ThumbSticks.Left.Y * -1f;

			// Button controls
			if (ks.IsKeyDown(_keybinds.Jump) || gs.IsButtonDown(Buttons.B))
				Jump = true;
			else
				Jump = false;

			if (ks.IsKeyDown(_keybinds.Block) || gs.IsButtonDown(Buttons.X))
				Block = true;
			else
				Block = false;

			if (ks.IsKeyDown(_keybinds.Attack1) || gs.IsButtonDown(Buttons.A))
				Attack1 = true;
			else
				Attack1 = false;

			if (ks.IsKeyDown(_keybinds.Attack2) || gs.IsButtonDown(Buttons.Y))
				Attack2 = true;
			else
				Attack2 = false;

			if (ks.IsKeyDown(_keybinds.Dash) || gs.IsButtonDown(Buttons.RightShoulder))
				Dash = true;
			else
				Dash = false;

			if (ks.IsKeyDown(_keybinds.Start) || gs.IsButtonDown(Buttons.Start))
				Start = true;
			else
				Start = false;

			if (ks.IsKeyDown(_keybinds.Select) || gs.IsButtonDown(Buttons.Back))
				Select = true;
			else
				Select = false;;
		}
	}
}
