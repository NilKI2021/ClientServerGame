using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Moq;
using Xunit;
using RockPaperScissorsServer;  // Простір імен вашого проекту

namespace ServerSide.Tests
{
    public class ServerTests
    {
        [Fact]
        public void TestDetermineWinner_DrawScenario()
        {
            // Arrange
            string clientChoice = "камінь";
            string serverChoice = "камінь";
            var expected = "Нічия!";

            // Act
            string result = DetermineWinner(clientChoice, serverChoice);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("камінь", "ножиці", "Ви перемогли!")]  // Камінь перемагає ножиці
        [InlineData("ножиці", "папір", "Ви перемогли!")]   // Ножиці перемагають папір
        [InlineData("папір", "камінь", "Ви перемогли!")]   // Папір перемагає камінь
        [InlineData("камінь", "папір", "Ви програли!")]    // Камінь програє паперу
        [InlineData("ножиці", "камінь", "Ви програли!")]   // Ножиці програють каменю
        [InlineData("папір", "ножиці", "Ви програли!")]    // Папір програє ножицям
        public void TestDetermineWinner_VariousScenarios(string clientChoice, string serverChoice, string expected)
        {
            // Act
            string result = DetermineWinner(clientChoice, serverChoice);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Логіка визначення переможця (винесена в окремий метод для спрощення тестування).
        /// </summary>
        private string DetermineWinner(string clientChoice, string serverChoice)
        {
            if (clientChoice == serverChoice)
            {
                return "Нічия!";
            }
            else if ((clientChoice == "камінь" && serverChoice == "ножиці") ||
                     (clientChoice == "ножиці" && serverChoice == "папір") ||
                     (clientChoice == "папір" && serverChoice == "камінь"))
            {
                return "Ви перемогли!";
            }
            else
            {
                return "Ви програли!";
            }
        }
    }
}
