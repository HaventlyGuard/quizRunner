using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using System.Threading;

namespace Quizrunner.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        private Application _application;
        private AutomationBase _automation;

        [TestInitialize]
        public void TestInitialize()
        {
            var appPath = Path.Combine(Directory.GetCurrentDirectory(), "Quizrunner.exe");
            _application = Application.Launch(appPath);
            _automation = new UIA3Automation();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _automation.Dispose();
            _application.Close();
        }
        public Window goToMainWindow()
        {
            var window = _application.GetMainWindow(_automation);
            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("User_Name")).AsTextBox();
            var enterButton = window.FindFirstDescendant(cf => cf.ByAutomationId("EnterButton")).AsButton(); textBox.Enter("NewUser16");
            textBox.Enter("NewUser1");
            enterButton.Click();
            Thread.Sleep(1000);
            var mainWindow = _application.GetAllTopLevelWindows(_automation)
                                         .FirstOrDefault(w => w.Name == "MainWindow");
            return mainWindow;
        }
        [TestMethod()]
        public void Start_button_Click_1Test()
        {
            var mainWindow = goToMainWindow();
            Assert.IsNotNull(mainWindow, "MainWindow not found.");

            var startButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Start_button")).AsButton();
            Assert.IsNotNull(startButton, "Start_button not found.");

            startButton.Click();

            var createTestWindow = _application.GetAllTopLevelWindows(_automation)
                                               .FirstOrDefault(w => w.Name == "CreateTest");
            Assert.IsNotNull(createTestWindow, "CreateTest window should be open.");
            Assert.IsTrue(createTestWindow.Properties.IsOffscreen.Value == false, "CreateTest window should be visible.");
            createTestWindow.Close();
        }

        [TestMethod()]
        public void Acheivments_window_click()
        {
            var mainWindow = goToMainWindow();
            Assert.IsNotNull(mainWindow, "MainWindow not found.");

            var achievmentButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Achievements")).AsButton();
            Assert.IsNotNull(achievmentButton, "Start_button not found.");

            achievmentButton.Click();
            Thread.Sleep(1000);
            var achivmentsWindow = _application.GetAllTopLevelWindows(_automation)
                                               .FirstOrDefault(w => w.Name == "Achievements");
            Assert.IsNotNull(achivmentsWindow, "Achievements window should be open.");
            Assert.IsTrue(achivmentsWindow.Properties.IsOffscreen.Value == false, "Achievements window should be visible.");
            achivmentsWindow.Close();
        }

        [TestMethod()]
        public void Start_button_Click()
        {
            var mainWindow = goToMainWindow();
            Assert.IsNotNull(mainWindow, "MainWindow not found.");

            var startTestButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Create_new_game")).AsButton();
            Assert.IsNotNull(startTestButton, "StartTestButton not found.");

            startTestButton.Click();
            Thread.Sleep(1000); //Доделать проверку на месседжбокс 
            /*
            // Проверка появления окна "Start_game"
            var achievementsWindow = _application.GetAllTopLevelWindows(_automation)
                                                 .FirstOrDefault(w => w.Name == "Start_game");
            Assert.IsNotNull(achievementsWindow, "Start_game window should be open.");
            Assert.IsFalse(achievementsWindow.Properties.IsOffscreen.Value, "Start_game window should be visible.");

            // Закрытие окна "Start_game"
            achievementsWindow.Close();

            // Проверка появления MessageBox с текстом "Нет тестов"
            var messageBox = _application.GetAllTopLevelWindows(_automation)
                                         .FirstOrDefault(w => w.ClassName == "#32770"); // ClassName для стандартного MessageBox
            Assert.IsNotNull(messageBox, "MessageBox should be open.");

            var messageText = messageBox.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text)).AsLabel();
            Assert.AreEqual("Нет тестов", messageText.Text, "MessageBox text is incorrect.");*/

           
        }
    }
}
