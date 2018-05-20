using Broccoli.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Broccoli.Engine;

namespace Broccoli
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        
        public static int ScreenWidth;
        public static int ScreenHeight;

        List<GameObject> _entities;
        private Camera _camera;
        private Player _player1;

        public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = true;
            var time = TimeSpan.FromMilliseconds((1f / 120f) * 1000); // framerate limit
            TargetElapsedTime = time;
        }

        protected override void Initialize()
        {
            Fullscreen(true);
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            Window.IsBorderless = true;

            Activated += Game1_Activated;
            Deactivated += Game1_Deactivated;
            base.Initialize();
        }

        private void Game1_Deactivated(object sender, EventArgs e)
        {
            Fullscreen(false);
        }

        private void Game1_Activated(object sender, EventArgs e)
        {
            Fullscreen(true);
        }

        private void Fullscreen(bool fullscreen)
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
        }

        protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D texture = Content.Load<Texture2D>("box");
            _player1 = new Player(new Rectangle(0, 0, 100, 100), texture);
            _entities = new List<GameObject>()
            {
                _player1
            };
            _camera = new Camera();
        }

		protected override void UnloadContent()
		{

        }

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

            foreach(GameObject ent in _entities)
            {
                ent.Update(gameTime, _entities);
            }
            _camera.Follow(_player1);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin(transformMatrix: _camera.Transform);
            foreach (GameObject ent in _entities)
            {
                ent.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
		}
	}
}
