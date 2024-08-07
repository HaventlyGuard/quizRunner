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
using FlaUI.Core.Conditions;





namespace Quizrunner.Tests
{
    [TestClass()]
    public class CreateTestTests
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
        public void TestButtonClick()
        {
            var startTestWindow = goToStartTestWindow();
            Assert.IsNotNull(startTestWindow, "StartTest window not found.");

            var doTestButton = startTestWindow.FindFirstDescendant(cf => cf.ByAutomationId("doTestButton")).AsButton();
            Assert.IsNotNull(doTestButton, "Do Test button not found.");

            doTestButton.Click();
            Thread.Sleep(1000); // Подождите некоторое время, чтобы приложение успело обработать действие

            // Проверяем, что нажатие кнопки вызывает нужное действие в приложении
            // Например, проверяем, что после нажатия кнопки окно "CreateTest" открывается
            var createTestWindow = _application.GetAllTopLevelWindows(_automation)
                                               .FirstOrDefault(w => w.Name == "CreateTest");
            Assert.IsNotNull(createTestWindow, "CreateTest window not opened after clicking Do Test button.");
        }

        [TestMethod]
        public void TestComboBoxSelection()
        {
            var startTestWindow = goToStartTestWindow();
            Assert.IsNotNull(startTestWindow, "StartTest window not found.");

            var comboBox = startTestWindow.FindFirstDescendant(cf => cf.ByAutomationId("Combobox_Test_Select")).AsComboBox();
            Assert.IsNotNull(comboBox, "ComboBox not found.");

            // Выбираем элемент в комбо-боксе (выполняем действие, которое обычно выполняется пользователем)
            comboBox.Select(0); // Выбор первого элемента, например

            // Проверяем, что после выбора элемента в комбо-боксе происходит нужное действие в приложении
            // Например, проверяем, что какие-то элементы становятся видимыми или активными
            var numberQLabel = startTestWindow.FindFirstDescendant(cf => cf.ByAutomationId("number_q")).AsLabel();
            var selectNumberQuestionsComboBox = startTestWindow.FindFirstDescendant(cf => cf.ByAutomationId("selectNumberQuestions")).AsComboBox();
            Assert.IsNotNull(numberQLabel, "NumberQ label not found.");
            Assert.IsNotNull(selectNumberQuestionsComboBox, "SelectNumberQuestions ComboBox not found.");
        
        }

        private Window goToStartTestWindow()
        {
            var mainWindow = goToMainWindow();
            Assert.IsNotNull(mainWindow, "Main window not found.");

            var startTestButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("StartTestButton")).AsButton();
            Assert.IsNotNull(startTestButton, "StartTestButton not found.");

            startTestButton.Click();
            Thread.Sleep(1000); // Подождите некоторое время, чтобы приложение успело обработать действие

            var startTestWindow = _application.GetAllTopLevelWindows(_automation)
                                              .FirstOrDefault(w => w.Name == "StartTest");
            return startTestWindow;
        }
    }
}
