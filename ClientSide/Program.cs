using System;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsClient
{
    /// <summary>
    /// Основний клас клієнта для гри "Камінь, Ножиці, Папір".
    /// </summary>
    class Client
    {
        /// <summary>
        /// Точка входу для клієнта.
        /// </summary>
        static void Main(string[] args)
        {
            // Підключення до сервера
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            Console.WriteLine("Підключення до сервера успішне!");

            // Створення потоку для зв’язку з сервером
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // Введення вибору користувача
                Console.WriteLine("Введіть свій вибір (камінь, ножиці, папір) або 'вихід' для завершення:");
                string userChoice = Console.ReadLine();

                // Відправка вибору на сервер
                byte[] dataToSend = Encoding.UTF8.GetBytes(userChoice);
                stream.Write(dataToSend, 0, dataToSend.Length);

                // Перевірка на завершення гри
                if (userChoice.ToLower() == "вихід")
                {
                    Console.WriteLine("Ви вийшли з гри.");
                    break;
                }

                // Отримання результату від сервера
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Результат: {result}");
            }

            // Закриття з'єднання
            client.Close();
        }
    }
}
