using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AKPS_Labs
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();

            // Ініціалізація SerialPort
            serialPort = new SerialPort("COM3", 9600); 
            serialPort.DataReceived += SerialPort_DataReceived;

            // Підказка для input_textBox
            input_textBox.Text = "Put text here...";
            input_textBox.ForeColor = Color.Gray;

            // Підключення обробників подій для input_textBox
            input_textBox.Enter += input_textBox_Enter;
            input_textBox.Leave += input_textBox_Leave;
        }

        private void input_textBox_Enter(object sender, EventArgs e)
        {
            // Якщо текст = "Put text here...", очищаємо та змінюємо колір
            if (input_textBox.Text == "Put text here...")
            {
                input_textBox.Text = "";
                input_textBox.ForeColor = Color.Black;
            }
        }

        private void input_textBox_Leave(object sender, EventArgs e)
        {
            // Якщо поле порожнє, повертаємо підказку та сірий колір
            if (string.IsNullOrWhiteSpace(input_textBox.Text))
            {
                input_textBox.Text = "Put text here...";
                input_textBox.ForeColor = Color.Gray;
            }
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка відкриття порту: " + ex.Message);
                    return;
                }
            }

            // Відправляємо текст на сервер (ESP8266)
            string message = input_textBox.Text;

            if(message == "Put text here...")
            {
                message = "";
            }

            serialPort.WriteLine(message);

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string response = serialPort.ReadLine(); // Зчитуємо лише один рядок
            string trimmedResponse = response.Trim(); // Обрізаємо пробіли

            // Оновлюємо output_textBox через Invoke, щоб уникнути конфліктів між потоками
            Invoke((MethodInvoker)delegate
            {
                output_textBox.AppendText(trimmedResponse + Environment.NewLine); // Додаємо нове повідомлення
            });
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Закриття порту при закритті форми
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }
    }
}
