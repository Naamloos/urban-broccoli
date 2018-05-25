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

		public InputType CurrentInput = InputType.Unknown;

		private Keybinds _keybinds;

		private KeyboardState OldKS;
		private GamePadState OldGS;

		public InputHandler(Keybinds keybinds)
		{
			this._keybinds = keybinds;
		}

		public void Update()
		{
			var ks = Keyboard.GetState();
			var gs = GamePad.GetState(PlayerIndex.One);

			if (ks.IsKeyDown(_keybinds.Start))
				CurrentInput = InputType.Keyboard;
			else if (gs.IsButtonDown(Buttons.Start))
				CurrentInput = InputType.Gamepad;

			// possibly replacable with switch statement
			if (CurrentInput == InputType.Keyboard)
				UpdateKeyboard(ks);
			else if (CurrentInput == InputType.Gamepad)
				UpdateGamepad(gs);

			OldKS = ks;
			OldGS = gs;
		}

		public void UpdateKeyboard(KeyboardState ks)
		{
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

			// Button controls
			if (ks.IsKeyDown(_keybinds.Jump))
				Jump = true;
			else
				Jump = false;

			if (ks.IsKeyDown(_keybinds.Block))
				Block = true;
			else
				Block = false;

			if (ks.IsKeyDown(_keybinds.Attack1))
				Attack1 = true;
			else
				Attack1 = false;

			if (ks.IsKeyDown(_keybinds.Attack2))
				Attack2 = true;
			else
				Attack2 = false;

			if (ks.IsKeyDown(_keybinds.Dash))
				Dash = true;
			else
				Dash = false;

			if (ks.IsKeyDown(_keybinds.Start))
				Start = true;
			else
				Start = false;

			if (ks.IsKeyDown(_keybinds.Select))
				Select = true;
			else
				Select = false;
		}

		public void UpdateGamepad(GamePadState gs)
		{
			// Axis controls
			XAxis = gs.ThumbSticks.Left.X;
			YAxis = gs.ThumbSticks.Left.Y * -1f;

			// Button controls
			if (gs.IsButtonDown(Buttons.B))
				Jump = true;
			else
				Jump = false;

			if (gs.IsButtonDown(Buttons.X))
				Block = true;
			else
				Block = false;

			if (gs.IsButtonDown(Buttons.A))
				Attack1 = true;
			else
				Attack1 = false;

			if (gs.IsButtonDown(Buttons.Y))
				Attack2 = true;
			else
				Attack2 = false;

			if (gs.IsButtonDown(Buttons.RightShoulder))
				Dash = true;
			else
				Dash = false;

			if (gs.IsButtonDown(Buttons.Start))
				Start = true;
			else
				Start = false;

			if (gs.IsButtonDown(Buttons.Back))
				Select = true;
			else
				Select = false;
		}
	}

	public enum InputType
	{
		Keyboard,
		Gamepad,
		Touchscreen, // for possible future mobile ports
		Unknown
	}
}
