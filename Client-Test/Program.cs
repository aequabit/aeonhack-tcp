using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UserClient();
            client.Connect("127.0.0.1", 8080);

            client.StateChanged += Client_StateChanged;
            client.ReadPacket += Client_ReadPacket;
            client.WritePacket += Client_WritePacket;

            while(true) { }
        }

        private static void Client_WritePacket(UserClient sender, int size)
        {
            Console.WriteLine(
                "[client] sent: {0}",
                size
            );
        }

        private static void Client_ReadPacket(UserClient sender, byte[] data)
        {
            Console.WriteLine(
                "[client] received: {0}",
                Encoding.UTF8.GetString(data)
            );
        }

        private static void Client_StateChanged(UserClient sender, bool connected)
        {
            Console.WriteLine(
                "[client] {0}connected {1} server",
                (!connected ? "dis" : ""),
                (!connected ? "from" : "to")
            );
            sender.Send(Encoding.UTF8.GetBytes("ayy"));
        }
    }
}
