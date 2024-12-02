using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APKS_Lab3
{

    /// <summary>
    /// Головна форма клієнтської частини гри "Камінь-Ножиці-Папір".
    /// </summary>
    public partial class main_Form : Form
    {

        /// <summary>
        /// Клієнт для роботи з послідовним портом.
        /// </summary>
        private SerialGameClient serialGameClient;


        /// <summary>
        /// Колір для вимкненої кнопки.
        /// </summary>
        Color off_color = ColorTranslator.FromHtml("#949494");

        /// <summary>
        /// Колір для ввімкненої кнопки.
        /// </summary>
        Color on_color = Color.White;

        /// <summary>
        /// Таймер для гри "AI проти AI".
        /// </summary>
        private Timer aiVsAiTimer;

        /// <summary>
        /// Конструктор головної форми.
        /// </summary>
        public main_Form()
        {
            InitializeComponent();
            InitializeGameModes();
        }

        /// <summary>
        /// Ініціалізація режимів гри.
        /// </summary>
        // вивід режимів для гри
        private void InitializeGameModes()
        {
            // Додаємо варіанти режимів у ComboBox
            mod_comboBox.Items.Add("Player vs AI");
            mod_comboBox.Items.Add("AI vs AI");
            mod_comboBox.SelectedIndex = 0; // Встановлюємо перший варіант за замовчуванням
        }

        /// <summary>
        /// Обробник натискання кнопки "Ножиці".
        /// </summary>
        // Гравець вибрав ножниці
        private void scissors_button_Click(object sender, EventArgs e)
        {
            //HandlePlayerChoice(GameLogic.Choice.Scissors);

            send_button_click("Scissors");

        }

        /// <summary>
        /// Обробник натискання кнопки "Папір".
        /// </summary>
        // Гравець вибрав папір
        private void paper_button_Click(object sender, EventArgs e)
        {
            //HandlePlayerChoice(GameLogic.Choice.Paper);

            send_button_click("Paper");


        }

        /// <summary>
        /// Обробник натискання кнопки "Камінь".
        /// </summary>
        // Гравець вибрав камінь
        private void rock_button_Click(object sender, EventArgs e)
        {
            //HandlePlayerChoice(GameLogic.Choice.Rock);

            send_button_click("Rock");

        }


        /// <summary>
        /// Обробник натискання кнопки "Грати".
        /// </summary>
        // Старт гри
        private void play_button_Click(object sender, EventArgs e)
        {
            //GameLogic gameLogic = new GameLogic();

            var selectedMode = mod_comboBox.SelectedItem?.ToString();
            results_textBox.AppendText($"Game has started!{Environment.NewLine} {Environment.NewLine}");
            results_textBox.AppendText($"Selected mode: {selectedMode}{Environment.NewLine} {Environment.NewLine}");

            //дизайн
            {

                // Вимкнути кнопку Play
                play_button.Enabled = false;
                play_button.FlatStyle = FlatStyle.Standard; // Зробити вигляд втопленим
                //play_button.BackColor = off_color;

                // Увімкнути кнопку Stop
                stop_button.Enabled = true;
                stop_button.FlatStyle = FlatStyle.Popup; // Повернути звичайний вигляд кнопки Stop
                //stop_button.BackColor = on_color;

                // Вимкнути можливість змінювати режим гри
                mod_comboBox.Enabled = false;


                // ввімкнути кнопки
                rock_button.Enabled = true;
                rock_button.FlatStyle = FlatStyle.Popup;
                rock_button.BackColor = on_color;

                paper_button.Enabled = true;
                paper_button.FlatStyle = FlatStyle.Popup;
                paper_button.BackColor = on_color;

                scissors_button.Enabled = true;
                scissors_button.FlatStyle = FlatStyle.Popup;
                scissors_button.BackColor = on_color;
            }


            // відкрити порт 

            try
            {
                serialGameClient = new SerialGameClient("COM3", 115200); // Задайте порт і швидкість
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open serial port: {ex.Message}");
            }


            if (selectedMode == "AI vs AI")
            {
                //дизайн
                {
                    // вимкнути кнопки
                    rock_button.Enabled = false;
                    rock_button.FlatStyle = FlatStyle.Standard;
                    rock_button.BackColor = off_color;

                    paper_button.Enabled = false;
                    paper_button.FlatStyle = FlatStyle.Standard;
                    paper_button.BackColor = off_color;

                    scissors_button.Enabled = false;
                    scissors_button.FlatStyle = FlatStyle.Standard;
                    scissors_button.BackColor = off_color;
                }


                //var (ai1Choice, ai2Choice, result) = gameLogic.PlayAiVsAi();
                //results_textBox.AppendText($"AI 1 chose: {ai1Choice}{Environment.NewLine}");
                //results_textBox.AppendText($"AI 2 chose: {ai2Choice}{Environment.NewLine}");
                //results_textBox.AppendText($"{result}{Environment.NewLine}");

                send_button_click(String.Empty); // AI vs AI
            }



            
           

        }


        /// <summary>
        /// Обробник натискання кнопки "Стоп".
        /// </summary>
        // Стоп гри
        private void stop_button_Click(object sender, EventArgs e)
        {
            results_textBox.AppendText($"Game has been stopped! {Environment.NewLine} {Environment.NewLine}");

            //дизайн
            {
                // Вимкнути кнопку Stop
                stop_button.Enabled = false;
                stop_button.FlatStyle = FlatStyle.Standard; // Зробити кнопку Stop втопленою
                //stop_button.BackColor= off_color;

                // Увімкнути кнопку Play
                play_button.Enabled = true;
                play_button.FlatStyle = FlatStyle.Popup; // Повернути звичайний вигляд кнопки Play
                //play_button.BackColor = on_color;

                // Дозволити змінювати режим гри після зупинки гри
                mod_comboBox.Enabled = true;

                // вимкнути кнопки
                rock_button.Enabled = false;
                rock_button.FlatStyle = FlatStyle.Standard;
                rock_button.BackColor = off_color;

                paper_button.Enabled = false;
                paper_button.FlatStyle = FlatStyle.Standard;
                paper_button.BackColor= off_color;

                scissors_button.Enabled = false;
                scissors_button.FlatStyle = FlatStyle.Standard;
                scissors_button.BackColor = off_color;
            }


            // закрити порт
            if (serialGameClient != null)

                serialGameClient.Close();


        }


        //// Player vs AI
        //private void HandlePlayerChoice(GameLogic.Choice playerChoice)
        //{
        //    // AI вибирає
        //    var aiChoice = GameLogic.GetRandomChoice();

        //    // Визначаємо результат
        //    var result = GameLogic.DetermineWinner(playerChoice, aiChoice);

        //    // Виводимо у `results_textBox`
        //    results_textBox.AppendText($"Player chose: {playerChoice}{Environment.NewLine}");
        //    results_textBox.AppendText($"AI chose: {aiChoice}{Environment.NewLine}");
        //    results_textBox.AppendText($"Result: {result}{Environment.NewLine}{Environment.NewLine}");
        //}



        // при запуску головного вікна
        private void main_Form_Load(object sender, EventArgs e)
        {

            //дизайн
            {
                // кнопка Stop вимкнена
                stop_button.Enabled = false;
                stop_button.FlatStyle = FlatStyle.Standard; // Зробити кнопку Stop утопленою


                // вимкнути кнопки
                rock_button.Enabled = false;
                rock_button.FlatStyle = FlatStyle.Standard;
                rock_button.BackColor = off_color;


                paper_button.Enabled = false;
                paper_button.FlatStyle = FlatStyle.Standard;
                paper_button.BackColor = off_color;



                scissors_button.Enabled = false;
                scissors_button.FlatStyle = FlatStyle.Standard;
                scissors_button.BackColor = off_color;
            }


           

        }


        /// <summary>
        /// Обробка JSON повідомлення
        /// </summary>
        private void send_button_click (string playerChoice)
        {
            if (serialGameClient == null)
            {
                MessageBox.Show("Serial connection is not established.");
                return;
            }

            string gameMode = mod_comboBox.SelectedItem.ToString(); // Припустимо, у вас є ComboBox для режиму

            if (gameMode == "Player vs AI")
            {
                try
                {
                    string response = serialGameClient.PlayGame(gameMode, playerChoice);

                    // Десеріалізуйте та відобразіть результат
                    var result = JsonConvert.DeserializeObject<dynamic>(response); // Розшифрувати JSON-відповідь


                    // Форматуємо результат для виведення
                    string formattedResponse = $"Player Choice: {result.PlayerChoice}{Environment.NewLine}" +
                                               $"AI Choice: {result.AIChoice}{Environment.NewLine}" +
                                               $"Result: {result.Result}{Environment.NewLine}";

                    // Додаємо форматований результат в текстове поле
                    results_textBox.AppendText(formattedResponse + Environment.NewLine);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during communication: {ex.Message}");
                }
            }
            else // AI vs AI
            {

                aiVsAiTimer = new Timer();
                aiVsAiTimer.Interval = 5000; // Інтервал 5 секунд
                aiVsAiTimer.Tick += (sender, e) =>
                {
                    if(serialGameClient != null)
                    {
                        try
                        {
                            string response = serialGameClient.PlayGame("AI vs AI", string.Empty);

                            var result = JsonConvert.DeserializeObject<dynamic>(response);

                            string formattedResponse = $"AI 1 Choice: {result.AI1Choice}{Environment.NewLine}" +
                                                       $"AI 2 Choice: {result.AI2Choice}{Environment.NewLine}" +
                                                       $"Result: {result.Result}{Environment.NewLine}";

                            results_textBox.AppendText(formattedResponse + Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show($"Error during communication: {ex.Message}");
                            StopAiVsAiGame(); // Зупинити гру у разі помилки
                        }
                    }

                   
                };

                aiVsAiTimer.Start();
            }

          
        }


        /// <summary>
        /// Зупиняє гру "AI проти AI".
        /// </summary>
        private void StopAiVsAiGame()
        {
            aiVsAiTimer?.Stop();
            aiVsAiTimer?.Dispose();
            aiVsAiTimer = null;
        }



    }
}
