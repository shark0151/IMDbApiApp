using IMDbApiLib.Models;
using imdbClientButWorking;
using imdbClientButWorking.Models;
using OpenQA.Selenium.Appium.Windows;

namespace ClientTest
{
    [TestClass]
    public class UITest : AppSession
    {
        [TestMethod]
        public async Task ClickLogin()
        {
            Setup();
            WindowsElement loginbtn = session.FindElementByName("login_Btn");
            loginbtn.Click();
            WindowsElement error = session.FindElementByName("OK");
            //Thread.Sleep(1000);
            bool success = error != null;
            error.Click();
            
            Assert.IsNotNull(error);
            TearDown();
        }

        [TestMethod]
        public async Task Login()
        {
            Setup();
            session.FindElementByName("username_field").SendKeys("test");
            session.FindElementByName("password_field").SendKeys("test123");
            WindowsElement loginbtn = session.FindElementByName("login_Btn");
            loginbtn.Click();

            
            session.SwitchTo().Window(session.WindowHandles[0]);
            var main = session.FindElementByName("main_window");


            bool success = main != null;
            
            Assert.IsNotNull(main);
            session.Quit();
            //session.SwitchTo().Window(session.WindowHandles[0]);
            TearDown();
        }

    }
}