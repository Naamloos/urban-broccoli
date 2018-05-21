using Broccoli.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Broccoli.Engine
{
	/// <summary>
	/// This class is for handling inputs from a sp
	/// </summary>
	public class InputHandler
	{
		public float XAxis
		{
			get
			{
				if (_currentGamePadState.ThumbSticks.Left.X != 0)
					return _currentGamePadState.ThumbSticks.Left.X;
				if (_currentKeyboardState.IsKeyDown(_keybinds.Left) && _currentKeyboardState.IsKeyDown(_keybinds.Right))
					return 0;
				if (_currentKeyboardState.IsKeyDown(_keybinds.Left))
					return -1;
				if (_currentKeyboardState.IsKeyDown(_keybinds.Right))
					return 1;
				return 0;
			}
		}

		public float YAxis
		{
			get
			{
				if (_currentGamePadState.ThumbSticks.Left.Y != 0)
					return -_currentGamePadState.ThumbSticks.Left.Y;
				if (_currentKeyboardState.IsKeyDown(_keybinds.Up) && _currentKeyboardState.IsKeyDown(_keybinds.Down))
					return 0; 
				if (_currentKeyboardState.IsKeyDown(_keybinds.Down))
					return 1;
				if (_currentKeyboardState.IsKeyDown(_keybinds.Up))
					return -1;
				return 0;
			}
		}

		public bool IsStartDown => IsDown(_keybinds.Start, Buttons.Start);
		public bool IsStartUp => IsUp(_keybinds.Start, Buttons.Start);
		public bool IsStartPressed => IsPressed(_keybinds.Start, Buttons.Start);

		public bool IsSelectDown => IsDown(_keybinds.Select, Buttons.Back);
		public bool IsSelectUp => IsUp(_keybinds.Select, Buttons.Back);
		public bool IsSelectPressed => IsPressed(_keybinds.Select, Buttons.Back);
		
		public bool IsJumpDown => IsDown(_keybinds.Jump, Buttons.B);
		public bool IsJumpUp => IsUp(_keybinds.Jump, Buttons.B);
		public bool IsJumpPressed => IsPressed(_keybinds.Jump, Buttons.B);

		public bool IsDashDown => IsDown(_keybinds.Dash, Buttons.RightShoulder);
		public bool IsDashUp => IsUp(_keybinds.Dash, Buttons.RightShoulder);
		public bool IsDashPressed => IsPressed(_keybinds.Dash, Buttons.RightShoulder);
		
		public bool IsAttack1Down => IsDown(_keybinds.Attack1, Buttons.A);
		public bool IsAttack1Up => IsUp(_keybinds.Attack1, Buttons.A);
		public bool IsAttack1Pressed => IsPressed(_keybinds.Attack1, Buttons.A);

		public bool IsAttack2Down => IsDown(_keybinds.Attack2, Buttons.Y);
		public bool IsAttack2Up => IsUp(_keybinds.Attack2, Buttons.Y);
		public bool IsAttack2Pressed => IsPressed(_keybinds.Attack2, Buttons.Y);
		
		public bool IsBlockDown => IsDown(_keybinds.Block, Buttons.X);
		public bool IsBlockUp => IsUp(_keybinds.Block, Buttons.X);
		public bool IsBlockPressed => IsPressed(_keybinds.Block, Buttons.X);

		private bool IsDown(Keys key, Buttons button) =>
			_currentKeyboardState.IsKeyDown(key) || _currentGamePadState.IsButtonDown(button);
		
		private bool IsUp(Keys key, Buttons button) =>
			_currentKeyboardState.IsKeyUp(key) && _currentGamePadState.IsButtonUp(button);
		
		private bool IsPressed(Keys key, Buttons button) =>
			(_previusKeyboardState.IsKeyUp(key) && _previusGamePadState.IsButtonUp(button)) && 
			(_currentKeyboardState.IsKeyDown(key) || _currentGamePadState.IsButtonDown(button));

		private KeyboardState _currentKeyboardState;
		private KeyboardState _previusKeyboardState;
		
		private GamePadState _currentGamePadState;
		private GamePadState _previusGamePadState;
		private Keybinds _keybinds;

		public InputHandler(Keybinds keybinds)
		{
			_currentKeyboardState = Keyboard.GetState();
			_previusKeyboardState = new KeyboardState();

			_currentGamePadState = GamePad.GetState(PlayerIndex.One);
			_previusGamePadState = new GamePadState();
			_keybinds = keybinds;
		}

		public void Update()
		{
			_previusKeyboardState = _currentKeyboardState;
			_currentKeyboardState = Keyboard.GetState();

			_previusGamePadState = _currentGamePadState;
			_currentGamePadState = GamePad.GetState(PlayerIndex.One);
		}
	}
}
