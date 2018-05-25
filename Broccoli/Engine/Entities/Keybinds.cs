using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Broccoli.Engine.Entities
{
	public class Keybinds
	{
		[JsonProperty("left")]
		public Keys Left = Keys.Left;

		[JsonProperty("right")]
		public Keys Right = Keys.Right;

		[JsonProperty("up")]
		public Keys Up = Keys.Up;

		[JsonProperty("down")]
		public Keys Down = Keys.Down;

		[JsonProperty("start")]
		public Keys Start = Keys.Enter;

		[JsonProperty("select")]
		public Keys Select = Keys.Escape;

		[JsonProperty("jump")]
		public Keys Jump = Keys.X;

		[JsonProperty("dash")]
		public Keys Dash = Keys.C;

		[JsonProperty("atk1")]
		public Keys Attack1 = Keys.Z;

		[JsonProperty("atk2")]
		public Keys Attack2 = Keys.A;

		[JsonProperty("block")]
		public Keys Block = Keys.S;
	}
}
