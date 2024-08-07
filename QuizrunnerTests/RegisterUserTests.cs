using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace Quizrunner.Tests
{
    [TestClass]
    public class RegisterUserTests
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

        [TestMethod]
        public void Test_EnterButton_DoesNotNavigate_WhenNameIsEmpty()
        {
            var window = _application.GetMainWindow(_automation);
            Assert.IsNotNull(window, "Main window is not loaded.");

            var enterButton = window.FindFirstDescendant(cf => cf.ByAutomationId("EnterButton")).AsButton();
            Assert.IsNotNull(enterButton, "Enter Button is not found.");
            enterButton.Click();

            Assert.IsFalse(window.Properties.IsOffscreen.Value, "RegisterUser window should still be visible.");
        }

        [TestMethod]
        public void Test_RegisterButton_DoesNotNavigate_WhenNameIsEmpty()
        {
            var window = _application.GetMainWindow(_automation);
            Assert.IsNotNull(window, "Main window is not loaded.");

            var registerButton = window.FindFirstDescendant(cf => cf.ByAutomationId("RegisterButton")).AsButton();
            Assert.IsNotNull(registerButton, "Register Button is not found.");
            registerButton.Click();

            Assert.IsFalse(window.Properties.IsOffscreen.Value, "RegisterUser window should still be visible.");
        }

        [TestMethod]
        public void Test_EnterButton_DoesNotNavigate_WhenUserDoesNotExist()
        {
            var window = _application.GetMainWindow(_automation);
            Assert.IsNotNull(window, "Main window is not loaded.");

            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("User_Name")).AsTextBox();
            var enterButton = window.FindFirstDescendant(cf => cf.ByAutomationId("EnterButton")).AsButton();

            textBox.Enter("NonExistingUser");
            enterButton.Click();

            Assert.IsFalse(window.Properties.IsOffscreen.Value, "RegisterUser window should still be visible.");
        }

        [TestMethod]
        public void Test_RegisterButton_RegistersNewUserAndNavigates()
        {
            var window = _application.GetMainWindow(_automation);
            Assert.IsNotNull(window, "Main window is not loaded.");

            var textBox = window.FindFirstDescendant(cf => cf.ByAutomationId("User_Name")).AsTextBox();
            var registerButton = window.FindFirstDescendant(cf => cf.ByAutomationId("RegisterButton")).AsButton();

            textBox.Enter("NewUser");
            registerButton.Click();

            // Check if the main window is now visible
            var mainWindow = _application.GetAllTopLevelWindows(_automation).FirstOrDefault(w => w.Title == "MainWindow");
            Assert.IsFalse(window.Properties.IsOffscreen.Value, "RegisterUser window should not be visible.");
        }
    }
}
