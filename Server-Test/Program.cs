using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerListener();
            server.Listen(8080);

            server.ClientStateChanged += Server_ClientStateChanged;
            server.ClientReadPacket += Server_ClientReadPacket;
            server.ClientWritePacket += Server_ClientWritePacket;

            while (true) { }
        }

        private static void Server_ClientWritePacket(ServerListener sender, ServerClient client, int size)
        {
            Console.WriteLine(
                "[client] {0}: sent: {1}",
                client.EndPoint.ToString(),
                size
            );
        }

        private static void Server_ClientReadPacket(ServerListener sender, ServerClient client, byte[] data)
        {
            Console.WriteLine(
                "[client] {0}: received: {1}",
                client.EndPoint.ToString(),
                Encoding.UTF8.GetString(data)
            );
            client.Send(Encoding.UTF8.GetBytes("wow"));
        }

        private static void Server_ClientStateChanged(ServerListener sender, ServerClient client, bool connected)
        {
            Console.WriteLine(
                "[client] {0}: {1}connected",
                client.EndPoint.ToString(),
                (!connected ? "dis" : "")
            );
        }
    }
}
