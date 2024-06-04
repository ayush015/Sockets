using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up the TCP client to connect to the server on localhost:13000
            TcpClient client = new TcpClient("127.0.0.1", 3000);
            Console.WriteLine("Connected to server!");

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // Enter a message to send to the server
                Console.Write("Enter a message: ");
                string message = Console.ReadLine();

                // Translate the message into bytes
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Send the message to the server
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                // Buffer to store the response bytes
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Translate the bytes back into a string
                string responseData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: {0}", responseData);
            }

            // Close the client
            client.Close();
        }
    }
}
