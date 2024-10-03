using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsServer
{
    class Server
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Сервер запущено, очiкування пiдключення клiєнтiв...");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клiєнт пiдключився!");
            NetworkStream stream = client.GetStream();
            string[] options = { "камiнь", "ножицi", "папiр" };
            Random random = new Random();

            while (true)
            {

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientChoice = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Вибiр клiєнта: {clientChoice}");


                if (clientChoice.ToLower() == "вихiд")
                {
                    Console.WriteLine("Клiєнт вийшов iз гри.");
                    break;
                }

                string serverChoice = options[random.Next(options.Length)];
                Console.WriteLine($"Вибiр сервера: {serverChoice}");
                string result;
                if (clientChoice == serverChoice)
                {
                    result = "Нiчия!";
                }
                else if ((clientChoice == "камiнь" && serverChoice == "ножицi") ||
                         (clientChoice == "ножицi" && serverChoice == "папiр") ||
                         (clientChoice == "папiр" && serverChoice == "камiнь"))
                {
                    result = "Ви перемогли!";
                }
                else
                {
                    result = "Ви програли!";
                }

                string message = $"Ваш вибiр: {clientChoice}, Вибiр сервера: {serverChoice} - {result}";
                byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);
            }

            client.Close();
            server.Stop();
            Console.WriteLine("Сервер завершив роботу.");
        }
    }
}
