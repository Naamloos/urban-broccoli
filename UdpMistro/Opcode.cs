namespace UdpMistro
{
    public enum Opcode : byte
    {
        // client => server
        ClientConnect, // args: <version: ushort> <nick: string>
        ClientSendInput, // args: TODO
        
        // server => client
        ServerConnectSuccess, // args: <motd: string> 
        ServerConnectFail, // args <errorMessage: string>
        ServerSendSnapshot, // args: <size: ushort> <entities: (entityId ushort, entityType ushort, entityData)...>
    }
}