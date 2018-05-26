namespace UdpMistro
{
    public struct UdpMessage
    {
        public readonly Opcode Opcode;
        public readonly object Message;

        public UdpMessage(Opcode opcode, object message)
        {
            Opcode = opcode;
            Message = message;
        }
    }
}