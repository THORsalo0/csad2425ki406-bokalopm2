using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO.Ports;


namespace APKS_Lab3
{

    public interface ISerialGameClient
    {
        string PlayGame(string gameMode, string playerChoice);
        void Close();
    }

    public class SerialGameClient : ISerialGameClient
    {
        private SerialPort serialPort;

        public SerialGameClient(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
        }


        /// <summary>
        /// Створення, відправка й отримання JSON запиту
        /// </summary>
        public virtual string PlayGame(string gameMode, string playerChoice)
        {
            if (gameMode == "Player vs AI")
            {
                // Формування JSON-запиту для Player vs AI
                var request = new
                {
                    GameMode = gameMode,
                    PlayerChoice = playerChoice
                };
                string jsonRequest = JsonConvert.SerializeObject(request);


                // Надсилання запиту
                serialPort.WriteLine(jsonRequest);

                // Отримання відповіді
                string jsonResponse = serialPort.ReadLine();
                return jsonResponse;
            }
            else
            {
                // Формування JSON-запиту для AI vs AI
                var request = new
                {
                    GameMode = gameMode

                };
                string jsonRequest = JsonConvert.SerializeObject(request);


                // Надсилання запиту
                serialPort.WriteLine(jsonRequest);

                // Отримання відповіді
                string jsonResponse = serialPort.ReadLine();
                return jsonResponse;
            }


        }

        public virtual void Close()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
        }
    }


}
