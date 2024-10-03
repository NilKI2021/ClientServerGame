using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Moq;
using Xunit;
using RockPaperScissorsServer;  // ������ ���� ������ �������

namespace ServerSide.Tests
{
    public class ServerTests
    {
        [Fact]
        public void TestDetermineWinner_DrawScenario()
        {
            // Arrange
            string clientChoice = "�����";
            string serverChoice = "�����";
            var expected = "ͳ���!";

            // Act
            string result = DetermineWinner(clientChoice, serverChoice);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("�����", "������", "�� ���������!")]  // ����� �������� ������
        [InlineData("������", "����", "�� ���������!")]   // ������ ����������� ����
        [InlineData("����", "�����", "�� ���������!")]   // ���� �������� �����
        [InlineData("�����", "����", "�� ��������!")]    // ����� ������ ������
        [InlineData("������", "�����", "�� ��������!")]   // ������ ��������� ������
        [InlineData("����", "������", "�� ��������!")]    // ���� ������ �������
        public void TestDetermineWinner_VariousScenarios(string clientChoice, string serverChoice, string expected)
        {
            // Act
            string result = DetermineWinner(clientChoice, serverChoice);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// ����� ���������� ��������� (�������� � ������� ����� ��� ��������� ����������).
        /// </summary>
        private string DetermineWinner(string clientChoice, string serverChoice)
        {
            if (clientChoice == serverChoice)
            {
                return "ͳ���!";
            }
            else if ((clientChoice == "�����" && serverChoice == "������") ||
                     (clientChoice == "������" && serverChoice == "����") ||
                     (clientChoice == "����" && serverChoice == "�����"))
            {
                return "�� ���������!";
            }
            else
            {
                return "�� ��������!";
            }
        }
    }
}
