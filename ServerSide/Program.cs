using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsServer
{
    /// <summary>
    /// Основний клас сервера для гри "Камінь, Ножиці, Папір".
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Точка входу для сервера.
        /// </summary>
        static void Main(string[] args)
        {
            // Налаштування серверного сокета
            TcpListener server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
            Console.WriteLine("Сервер запущено, очікування підключення клієнтів...");

            // Прийняття підключень клієнта
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Клієнт підключився!");

            // Створення потоків для читання та запису
            NetworkStream stream = client.GetStream();

            // Основні варіанти гри
            string[] options = { "камінь", "ножиці", "папір" };
            Random random = new Random();

            while (true)
            {
                // Прийом вибору від клієнта
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientChoice = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Вибір клієнта: {clientChoice}");

                // Перевірка на вихід
                if (clientChoice.ToLower() == "вихід")
                {
                    Console.WriteLine("Клієнт вийшов із гри.");
                    break;
                }

                // Випадковий вибір сервера
                string serverChoice = options[random.Next(options.Length)];
                Console.WriteLine($"Вибір сервера: {serverChoice}");

                // Визначення переможця
                string result;
                if (clientChoice == serverChoice)
                {
                    result = "Нічия!";
                }
                else if ((clientChoice == "камінь" && serverChoice == "ножиці") ||
                         (clientChoice == "ножиці" && serverChoice == "папір") ||
                         (clientChoice == "папір" && serverChoice == "камінь"))
                {
                    result = "Ви перемогли!";
                }
                else
                {
                    result = "Ви програли!";
                }

                // Надсилання результату клієнту
                string message = $"Ваш вибір: {clientChoice}, Вибір сервера: {serverChoice} - {result}";
                byte[] dataToSend = Encoding.UTF8.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);
            }

            // Закриття з'єднання
            client.Close();
            server.Stop();
            Console.WriteLine("Сервер завершив роботу.");
        }
    }
}
