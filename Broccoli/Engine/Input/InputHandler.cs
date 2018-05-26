using Broccoli.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Broccoli.Engine.Input
{
	/// <summary>
	/// This class is for handling inputs from a sp
	/// </summary>
	public class InputHandler : Input
	{
		public InputType CurrentInput = InputType.Unknown;

		private readonly Keybinds _keybinds;

		public InputHandler(Keybinds keybinds)
		{
			this._keybinds = keybinds;
		}
		
		//???
		//private KeyboardState _oldKs;
		//private GamePadState _oldGs;

		public override void Update()
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

			//_oldKs = ks;
			//_oldGs = gs;
			base.Update();
		}

		private void UpdateKeyboard(KeyboardState ks)
		{
			// Axis controls
			var upDown = ks.IsKeyDown(_keybinds.Up);
			var downDown = ks.IsKeyDown(_keybinds.Down);
			if (upDown && downDown)
				YAxis = 0; // If both are pressed, they cancel out and Y is 0
			else if (downDown)
				YAxis = 1; // Down sets Y to 1
			else if (upDown)
				YAxis = -1; // Up sets Y to -1
			else
				YAxis = 0; // Else, nothing.
			
			var leftDown = ks.IsKeyDown(_keybinds.Left);
			var rightDown = ks.IsKeyDown(_keybinds.Right);
			if (leftDown && rightDown)
				XAxis = 0; // If both are pressed, they cancel out and X is 0
			else if (leftDown)
				XAxis = -1; // Left sets X to -1
			else if (rightDown)
				XAxis = 1; // Right sets X to 1
			else
				XAxis = 0; // Else, nothing.
			
			// Button controls
			Jump = ks.IsKeyDown(_keybinds.Jump);
			Block = ks.IsKeyDown(_keybinds.Block);
			Attack1 = ks.IsKeyDown(_keybinds.Attack1);
			Attack2 = ks.IsKeyDown(_keybinds.Attack2);
			Dash = ks.IsKeyDown(_keybinds.Dash);
			Start = ks.IsKeyDown(_keybinds.Start);
			Select = ks.IsKeyDown(_keybinds.Select);
		}

		private void UpdateGamepad(GamePadState gs)
		{
			// Axis controls
			XAxis = gs.ThumbSticks.Left.X;
			YAxis = gs.ThumbSticks.Left.Y * -1f;

			// Button controls
			Jump = gs.IsButtonDown(Buttons.B);
			Block = gs.IsButtonDown(Buttons.X);
			Attack1 = gs.IsButtonDown(Buttons.A);
			Attack2 = gs.IsButtonDown(Buttons.Y);
			Dash = gs.IsButtonDown(Buttons.RightShoulder);
			Start = gs.IsButtonDown(Buttons.Start);
			Select = gs.IsButtonDown(Buttons.Back);
		}
	}

	public enum InputType : byte
	{
		Keyboard,
		Gamepad,
		Touchscreen, // for possible future mobile ports
		Unknown
	}
}
