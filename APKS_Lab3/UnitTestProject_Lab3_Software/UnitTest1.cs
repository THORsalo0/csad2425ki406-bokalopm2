using APKS_Lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace UnitTestProject_Lab3_Software
{
    [TestClass]
    public class ButtonExistenceTests
    {
        // Тест для кнопки "Scissors"
        [TestMethod]
        public void TestScissorsButtonExists()
        {
            // Arrange
            var gameForm = new main_Form();

            // Act
            var scissorsButton = typeof(main_Form)
                .GetField("scissors_button", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(gameForm);

            // Assert
            Assert.IsNotNull(scissorsButton, "Scissors button does not exist in the form class.");
        }

        // Тест для кнопки "Paper"
        [TestMethod]
        public void TestPaperButtonExists()
        {
            // Arrange
            var gameForm = new main_Form();

            // Act
            var paperButton = gameForm.Controls["paper_button"]; // Перевіряємо наявність кнопки по її імені

            // Assert
            Assert.IsNotNull(paperButton, "Paper button does not exist on the form.");
        }

        // Тест для кнопки "Rock"
        [TestMethod]
        public void TestRockButtonExists()
        {
            // Arrange
            var gameForm = new main_Form();

            // Act
            var rockButton = gameForm.Controls["rock_button"]; // Перевіряємо наявність кнопки по її імені

            // Assert
            Assert.IsNotNull(rockButton, "Rock button does not exist on the form.");
        }
    }
}
