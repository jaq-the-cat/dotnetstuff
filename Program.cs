using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace dotnetstuff {
    class Client {
        ClientWebSocket Socket;
        CancellationTokenSource CTS;

        public Client() {
            Socket = new ClientWebSocket();
            CTS = new CancellationTokenSource();
        }

        static string Read(string prompt) {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public async void Connect(string url, int port) {
            await Socket.ConnectAsync(new Uri($"{url}:{port}"), CTS.Token);

            string message;
            while ((message = Read("> ")) != "q")
                await Socket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CTS.Token);

            await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CTS.Token);
        }
    }

    class Program {
        const string URL = "ws://127.0.0.1";
        const int PORT = 8000;

        static void Main(string[] args) {
            Client Client = new Client();
            Console.WriteLine($"Connecting to {URL}:{PORT}");
            Client.Connect(URL, PORT);
        }
    }
}
