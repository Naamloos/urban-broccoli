using System.IO;
using Broccoli.Engine.Input;
using UdpMistro;

namespace Broccoli.Engine.Input
{
    public class Input : INetworkable
    {
        public float XAxis;
        public float YAxis;
	    
        public bool Start;
        public bool Select;
        public bool Jump;
        public bool Dash;
        public bool Attack1;
        public bool Attack2;
        public bool Block;

        public bool StartPress => Start && !_oldstart;
        public bool SelectPress => Select && !_oldselect;
        public bool JumpPress => Jump && !_oldjump;
        public bool DashPress => Dash && !_olddash;
        public bool Attack1Press => Attack1 && !_oldattack1;
        public bool Attack2Press => Attack2 && !_oldattack2;
        public bool BlockPress => Block && !_oldblock;

        private bool _oldstart;
        private bool _oldselect;
        private bool _oldjump;
        private bool _olddash;
        private bool _oldattack1;
        private bool _oldattack2;
        private bool _oldblock;

        public virtual void Update()
        {
			_oldstart = Start;
			_oldselect = Select;
			_oldjump = Jump;
			_olddash = Dash;
			_oldattack1 = Attack1;
			_oldattack2 = Attack2;
			_oldblock = Block;
        }

	    public void Serialize(BinaryWriter writer)
	    {
		    writer.Write(XAxis);
		    writer.Write(YAxis);
		    writer.Write(Start);
		    writer.Write(Select);
		    writer.Write(Jump);
		    writer.Write(Dash);
		    writer.Write(Attack1);
		    writer.Write(Attack2);
		    writer.Write(Block);
	    }

	    public static Input Deserialize(BinaryReader reader)
	    {
		    return new Input
		    {
			    XAxis = reader.ReadSingle(),
			    YAxis = reader.ReadSingle(),
			    Start = reader.ReadBoolean(),
			    Select = reader.ReadBoolean(),
			    Jump = reader.ReadBoolean(),
			    Dash = reader.ReadBoolean(),
			    Attack1 = reader.ReadBoolean(),
			    Attack2 = reader.ReadBoolean(),
			    Block = reader.ReadBoolean(),
		    };
	    }
    }
}