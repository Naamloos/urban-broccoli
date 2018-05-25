using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Engine.Input
{
	public class NetworkInputHandler : InputHandler
	{
		public ushort Id;

		public NetworkInputHandler(ushort id) : base(null)
		{
			this.Id = id;
		}

		public override void Update()
		{
			// Do network based update shit here
		}
	}
}
