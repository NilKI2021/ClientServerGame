using System;
using System.Net.Sockets;
using System.Text;

namespace RockPaperScissorsClient
{
    class Client
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            Console.WriteLine("Пiдключення до сервера успiшне!");
            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.WriteLine("Введiть свiй вибiр (камiнь, ножицi, папiр) або 'вихiд' для завершення:");
                string userChoice = Console.ReadLine();
                byte[] dataToSend = Encoding.UTF8.GetBytes(userChoice);
                stream.Write(dataToSend, 0, dataToSend.Length);
                if (userChoice.ToLower() == "вихiд")
                {
                    Console.WriteLine("Ви вийшли з гри.");
                    break;
                }

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string result = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Результат: {result}");
            }
            client.Close();
        }
    }
}
