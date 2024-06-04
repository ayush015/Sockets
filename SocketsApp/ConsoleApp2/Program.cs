using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        const int port = 3000; 
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start server...");
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"Server is up and running at port : {port}");
            int clientCounter = 0;
            while(true)
            {
                Console.WriteLine("Connecting new Clients");
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine($"{++clientCounter} Clients Connected");

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));

                clientThread.Start(client);
            }
        }

        private static void HandleClient(object? client)
        {
            TcpClient newClient = (TcpClient)client;
            NetworkStream stream = newClient.GetStream();
            byte[] buffer = new byte[1024];  
            int bytesRead = 0;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: {0}", dataReceived);
                byte[] dataToSend = Encoding.ASCII.GetBytes(dataReceived);
                stream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine("Sent: {0}", dataReceived);
            }
        }

    }
}
