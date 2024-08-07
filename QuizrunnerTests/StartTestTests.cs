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
    public class StartTestTests
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
        public void doTestButton_ClickTest()
        {
            //из-за того что в бд нет тестов, сюда не получается перейти
        }
    }
}