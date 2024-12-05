using APKS_Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using Moq;
using System.Drawing;

namespace UnitTestProject_Lab3_Software
{
    [TestClass]
    public class ButtonExistenceTests
    {
        // Тест для кнопки "Scissors"
        [TestMethod]
        public void TestScissorsButtonClick_CallsSendButtonClickWithScissors()
        {
            // Arrange
            var form = new main_Form();

            // Act
            form.scissors_button_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Scissors", form.lastSentValue, "The method should pass 'Scissors' to send_button_click.");
        }

        // Тест для кнопки "Paper"
        [TestMethod]
        public void TestPaperButtonClick_CallsSendButtonClickWithPaper()
        {
            // Arrange
            var form = new main_Form();

            // Act
            form.paper_button_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Paper", form.lastSentValue, "The method should pass 'Paper' to send_button_click.");
        }

        // Тест для кнопки "Rock"
        [TestMethod]
        public void TestRockButtonClick_CallsSendButtonClickWithRock()
        {
            // Arrange
            var form = new main_Form();

            // Act
            form.rock_button_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Rock", form.lastSentValue, "The method should pass 'Rock' to send_button_click.");
        }
    }


    [TestClass]
    public class PlayButtonTests
    {
        [TestMethod]
        public void TestPlayButtonClick_UIStateChanges()
        {
            // Arrange
            var form = new main_Form();

            // Симулюємо початковий стан
            form.play_button.Enabled = true;
            form.stop_button.Enabled = false;
            form.mod_comboBox.SelectedItem = "Player vs AI"; // Імітуємо вибір режиму
            form.rock_button.Enabled = false;

            // Act
            form.play_button_Click(null, EventArgs.Empty); // Викликаємо метод

            // Assert
            Assert.IsFalse(form.play_button.Enabled, "Play button should be disabled.");
            Assert.IsTrue(form.stop_button.Enabled, "Stop button should be enabled.");
            Assert.IsFalse(form.mod_comboBox.Enabled, "Mode combo box should be disabled.");
            Assert.IsTrue(form.rock_button.Enabled, "Rock button should be enabled.");
        }



        [TestMethod]
        public void TestPlayButtonClick_AIVsAI_Mode()
        {
            // Arrange
            var form = new main_Form();
            form.mod_comboBox.SelectedItem = "AI vs AI";

            // Act
            form.play_button_Click(null, EventArgs.Empty);

            // Assert
            Assert.IsFalse(form.rock_button.Enabled, "Rock button should be disabled in AI vs AI mode.");
            Assert.IsFalse(form.paper_button.Enabled, "Paper button should be disabled in AI vs AI mode.");
            Assert.IsFalse(form.scissors_button.Enabled, "Scissors button should be disabled in AI vs AI mode.");
        }

    }


    [TestClass]
    public class CloseButtonTests
    {
        [TestMethod]
        public void TestStopButtonClick()
        {
            // Arrange
            var form = new main_Form();
            form.stop_button = new Button(); // Створити кнопку
            form.play_button = new Button(); // Створити кнопку Play
            form.rock_button = new Button(); // Створити кнопку Rock
            form.paper_button = new Button(); // Створити кнопку Paper
            form.scissors_button = new Button(); // Створити кнопку Scissors
            form.mod_comboBox = new ComboBox(); // Створити ComboBox
            form.results_textBox = new TextBox(); // Створити TextBox

            // Встановити значення властивостей кнопок перед натисканням
            form.stop_button.Enabled = true;
            form.play_button.Enabled = false;
            form.rock_button.Enabled = true;
            form.paper_button.Enabled = true;
            form.scissors_button.Enabled = true;

            // Мокування serialGameClient, тепер використовуємо інтерфейс
            var serialGameClientMock = new Mock<ISerialGameClient>();
            form.serialGameClient = serialGameClientMock.Object;  // Використовуємо Object для отримання мокованого інтерфейсу

            // Act
            form.stop_button_Click(null, EventArgs.Empty);

            // Assert
            Assert.IsFalse(form.stop_button.Enabled);
            Assert.AreEqual(FlatStyle.Standard, form.stop_button.FlatStyle);

            Assert.IsTrue(form.play_button.Enabled);
            Assert.AreEqual(FlatStyle.Popup, form.play_button.FlatStyle);

            Assert.IsFalse(form.rock_button.Enabled);
            Assert.IsFalse(form.paper_button.Enabled);
            Assert.IsFalse(form.scissors_button.Enabled);

            Assert.IsTrue(form.mod_comboBox.Enabled);

            // Перевірка, що викликано метод Close() на serialGameClient
            serialGameClientMock.Verify(client => client.Close(), Times.Once);
        }
    }


    [TestClass]
    public class main_Form_LoadTests
    {
        [TestMethod]
        public void TestMainFormLoad()
        {
            // Arrange
            var form = new main_Form();

            // Створюємо реальні екземпляри кнопок
            var stopButton = new Button();
            var rockButton = new Button();
            var paperButton = new Button();
            var scissorsButton = new Button();

            // Присвоюємо кнопки на форму
            form.stop_button = stopButton;
            form.rock_button = rockButton;
            form.paper_button = paperButton;
            form.scissors_button = scissorsButton;

            // Act
            // Викликаємо метод Load, щоб перевірити початкові значення
            form.main_Form_Load(null, EventArgs.Empty);

            // Assert
            // Перевіряємо, чи вимкнено кнопку Stop
            Assert.IsFalse(stopButton.Enabled);
            Assert.AreEqual(FlatStyle.Standard, stopButton.FlatStyle);

            // Перевіряємо, чи вимкнено кнопки rock, paper, scissors
            Assert.IsFalse(rockButton.Enabled);
            Assert.AreEqual(FlatStyle.Standard, rockButton.FlatStyle);
            // Перевіряємо фактичний колір для BackColor
            Assert.AreEqual(Color.FromArgb(148, 148, 148), rockButton.BackColor);

            Assert.IsFalse(paperButton.Enabled);
            Assert.AreEqual(FlatStyle.Standard, paperButton.FlatStyle);
            Assert.AreEqual(Color.FromArgb(148, 148, 148), paperButton.BackColor);

            Assert.IsFalse(scissorsButton.Enabled);
            Assert.AreEqual(FlatStyle.Standard, scissorsButton.FlatStyle);
            Assert.AreEqual(Color.FromArgb(148, 148, 148), scissorsButton.BackColor);
        }





    }


    [TestClass]
    public class SendButtonTests
    {
        private Mock<ISerialGameClient> serialGameClientMock;
        private main_Form form;
        private const string JsonResponsePlayerVsAI = "{\"PlayerChoice\": \"Rock\", \"AIChoice\": \"Scissors\", \"Result\": \"Player Wins\"}";
        private const string JsonResponseAiVsAi = "{\"AI1Choice\": \"Rock\", \"AI2Choice\": \"Paper\", \"Result\": \"AI 2 Wins\"}";

        [TestInitialize]
        public void Setup()
        {
            serialGameClientMock = new Mock<ISerialGameClient>();
            form = new main_Form
            {
                serialGameClient = serialGameClientMock.Object,
                mod_comboBox = new ComboBox(),
                results_textBox = new TextBox()
            };
        }

        [TestMethod]
        public void SendButtonClick_PlayerVsAI_ShouldAppendFormattedResponse()
        {
            // Arrange
            form.mod_comboBox.Items.Add("Player vs AI");
            form.mod_comboBox.SelectedIndex = 0;
            serialGameClientMock.Setup(s => s.PlayGame("Player vs AI", "Rock")).Returns(JsonResponsePlayerVsAI);

            // Act
            form.send_button_click("Rock");

            // Assert
            string expectedOutput = "Player Choice: Rock\r\nAI Choice: Scissors\r\nResult: Player Wins\r\n";
            Assert.IsTrue(form.results_textBox.Text.Contains(expectedOutput));
        }

        [TestMethod]
        public void SendButtonClick_AiVsAi_ShouldAppendFormattedResponse()
        {
            // Arrange
            form.mod_comboBox.Items.Add("AI vs AI");
            form.mod_comboBox.SelectedIndex = 0;
            serialGameClientMock.Setup(s => s.PlayGame("AI vs AI", string.Empty)).Returns(JsonResponseAiVsAi);

            // Act
            form.send_button_click(string.Empty);

            // Симулюємо виклик методу обробки події
            form.AiVsAiTimer_Tick(null, EventArgs.Empty);

            // Assert
            string expectedOutput = "AI 1 Choice: Rock\r\nAI 2 Choice: Paper\r\nResult: AI 2 Wins\r\n";
            Assert.IsTrue(form.results_textBox.Text.Contains(expectedOutput));
        }

    }






}
