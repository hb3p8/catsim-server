using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Example
{
    public class CatServer : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Console.WriteLine("New user!");

            foreach (string id in Sessions.IDs)
            {
                Console.WriteLine(id);
                Sessions.SendTo("Asd", id);
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Message received: " + e.Data);
            var msg = e.Data == "HELLO"
                      ? "Nya"
                      : "Pff";

            Send(msg);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://127.0.0.1");
            wssv.AddWebSocketService<CatServer>("/cat");
            wssv.Start();
            Console.WriteLine("Server started");
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}