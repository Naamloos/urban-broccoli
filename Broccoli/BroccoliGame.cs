using Broccoli.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Broccoli.Engine;

namespace Broccoli
{
	public class BroccoliGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        
        public static int ScreenWidth;
        public static int ScreenHeight;

        /// <summary>
        /// Client-only entities such as props
        /// </summary>
	    private List<GameObject> _clientEntities;
        
        /// <summary>
        /// Remote entitities like bullet trails and other players
        /// </summary>
        private List<RemoteGameObject> _remoteEntities;

        private Camera _camera;
        private Player _localPlayer;

        public BroccoliGame()
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
            var texture = Content.Load<Texture2D>("box");
            _localPlayer = new Player(new Rectangle(0, 0, 100, 100), texture);
		    _clientEntities = new List<GameObject>();
		    _remoteEntities = new List<RemoteGameObject>();
            _camera = new Camera();
        }

		protected override void UnloadContent()
		{

        }

		protected override async void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

            // tell server to send remote entities (send our remote entity ids)
            // server sends remote entities
            
            // do remote entities need updating? if so do that here

            // update player
            _localPlayer.Update(gameTime, _clientEntities);

            // update props and such
            foreach (var ent in _clientEntities)
            {
                ent.Update(gameTime, _clientEntities);
            }

            // send player position to server
            // server sends back real client positions
            // we adjust to compensate for lag and reposition all the entities

            _camera.Follow(_localPlayer);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin(transformMatrix: _camera.Transform);
            _localPlayer.Draw(gameTime, spriteBatch);
		    foreach (var ent in _clientEntities)
		    {
		        ent.Draw(gameTime, spriteBatch);
            }
		    foreach (var ent in _remoteEntities)
		    {
		        ent.Draw(gameTime, spriteBatch);
		    }
            spriteBatch.End();

            base.Draw(gameTime);
		}
	}
}
