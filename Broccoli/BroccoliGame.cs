using Broccoli.Engine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Broccoli.Engine;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Broccoli
{
	public class BroccoliGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        
        public static int ScreenWidth;
        public static int ScreenHeight;

		SpriteFont _debugfont;

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
        private SolidObject _testground;

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
			Fullscreen(false);
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            Window.IsBorderless = false;

			#if !DEBUG
			Activated += Game1_Activated;
            Deactivated += Game1_Deactivated;
			#endif
			// Nicer to work windowed when developing..
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
            if(fullscreen)
            {
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 1600;
                graphics.PreferredBackBufferHeight = 900;
            }
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;
        }

        protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
            var texture = Content.Load<Texture2D>("box");
            
            // Load keybinds from a file (if exists, else create file)
			Keybinds kb;
			if (File.Exists("keybinds.json"))
			{
				kb = JsonConvert.DeserializeObject<Keybinds>(File.ReadAllText("keybinds.json"));
			}
			else
			{
				kb = new Keybinds();
				File.Create("keybinds.json").Close();
				File.WriteAllText("keybinds.json", JsonConvert.SerializeObject(kb));
			}
            _localPlayer = new Player(texture,new Rectangle(0, 0, 100, 100), new InputHandler(kb));

            _testground = new SolidObject(new Rectangle(0, 300, 500,50), texture);
		    _clientEntities = new List<GameObject>()
            {
                _testground
            };
		    _remoteEntities = new List<RemoteGameObject>();
            _camera = new Camera();
			_debugfont = Content.Load<SpriteFont>("Fonts/debug");


        }

		protected override void UnloadContent()
		{

        }

		protected override async void Update(GameTime gameTime)
		{
			// Update inbput before everything else so we get no input lag (by one frame. ok.)
			_localPlayer.Input.Update();

            // tell server to send remote entities (send our remote entity ids)
            // server sends remote entities

            // do remote entities need updating? if so do that here

            // update player
            
            _localPlayer.Update(gameTime, _clientEntities);
            _camera.Follow(_localPlayer);

            // update props and such
            foreach (var ent in _clientEntities)
            {
                ent.Update(gameTime, _clientEntities);
            }

            // send player position to server
            // server sends back real client positions
            // we adjust to compensate for lag and reposition all the entities

            

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

			// Debug data.
			spriteBatch.Begin();
			var sb = new StringBuilder();
			sb.AppendLine("Input data");
			sb.AppendLine($"Input Device: {_localPlayer.Input.CurrentInput.ToString()}");
			sb.AppendLine($"X Axis: {_localPlayer.Input.XAxis}, Y Axis: {_localPlayer.Input.YAxis}");
			sb.AppendLine($"Start: {_localPlayer.Input.Start}, Select: {_localPlayer.Input.Select}");
			sb.AppendLine($"Atk1: {_localPlayer.Input.Attack1}, Atk2: {_localPlayer.Input.Attack2}");
			sb.AppendLine($"Jump: {_localPlayer.Input.Jump}, Block: {_localPlayer.Input.Block}, Dash: {_localPlayer.Input.Dash}");
			sb.AppendLine($"Velocity: {_localPlayer.Velocity}");
			sb.AppendLine($"IVelocity: {_localPlayer.InputVelocity}");

            spriteBatch.DrawString(_debugfont, sb.ToString(), new Vector2(3, 3), Color.ForestGreen);
            spriteBatch.End();

            base.Draw(gameTime);
		}
	}
}

// poops