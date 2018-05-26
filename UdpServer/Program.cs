using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lidgren.Network;

namespace UdpServer
{
    internal static class Program
    {
        internal static NetServer Server;

        private static void Main(string[] args)
        {
            var config = new NetPeerConfiguration(UdpMistro.Globals.AppIdentifier) {Port = 14242};

            Server = new NetServer(config);
            Server.Start();

            new Thread(GameUpdate).Start();
            ServerRelay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server">The UDP server</param>
        private static void ServerRelay()
        {
            while (true)
            {
                NetIncomingMessage msg;
                while ((msg = Server.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            Console.WriteLine(msg.ReadString());
                            break;
                        case NetIncomingMessageType.Data:
                            // TODO
                            break;
                        default:
                            Console.WriteLine("Unhandled type: " + msg.MessageType);
                            break;
                    }

                    Server.Recycle(msg);
                }
            }
        }

        /// <summary>
        /// Game update logic goes here
        /// </summary>
        private static void GameUpdate()
        {
            while (true)
            {
                // TODO
            }
        }
    }
}