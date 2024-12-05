using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO.Ports;
using System.Threading;

namespace UnitTestProject_Hardware
{
        [TestClass]
        public class ESP32CommunicationTests
        {
            private SerialPort _serialPort;
            private const string PortName = "COM3"; // Заміна на відповідний порт
            private const int BaudRate = 115200;

            [TestInitialize]
            public void Setup()
            {
                _serialPort = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One)
                {
                    ReadTimeout = 5000,
                    WriteTimeout = 5000
                };
                _serialPort.Open();
            }

            [TestCleanup]
            public void Cleanup()
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();
            }

            [TestMethod]
            public void Test_AvailablePort()
            {
                Assert.IsTrue(SerialPort.GetPortNames().Length > 0, "Немає доступних портів");
                Assert.IsTrue(Array.Exists(SerialPort.GetPortNames(), port => port == PortName), $"Порт {PortName} не знайдено.");
            }

            [TestMethod]
            public void Test_SendJsonRequest_Mode1()
            {
                var jsonRequest = "{\"GameMode\":\"AI vs AI\"}";
                _serialPort.WriteLine(jsonRequest);
                Thread.Sleep(5000); // Затримка для отримання відповіді

                string response = _serialPort.ReadExisting().Trim();
                Assert.IsFalse(string.IsNullOrEmpty(response), "Відповідь пуста");
                Assert.IsTrue(response.Contains("AI"), $"Невірна відповідь: {response}");
            }

            [TestMethod]
            public void Test_SendJsonRequest_Mode2()
            {
                var jsonRequest = "{\"GameMode\":\"Player vs AI\", \"PlayerChoice\":\"Rock\"}";
                _serialPort.WriteLine(jsonRequest);
                Thread.Sleep(5000);

                string response = _serialPort.ReadExisting().Trim();
                Assert.IsFalse(string.IsNullOrEmpty(response), "Відповідь пуста");
                Assert.IsTrue(response.Contains("wins") || response.Contains("Draw"), $"Невірна відповідь: {response}");
            }


            [TestMethod]
            public void Test_SendJsonRequest_Mode3()
            {
                var jsonRequest = "{\"GameMode\":\"Player vs AI\", \"PlayerChoice\":\"Rock\"}";
                _serialPort.WriteLine(jsonRequest);
                Thread.Sleep(5000);

                string response = _serialPort.ReadExisting().Trim();
                Assert.IsFalse(string.IsNullOrEmpty(response), "Відповідь пуста");
                Assert.IsTrue(response.Contains("wins") || response.Contains("Draw"), $"Невірна відповідь: {response}");
            }

    }

}
