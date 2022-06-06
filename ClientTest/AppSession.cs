using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace ClientTest
{
    public class AppSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = @"D:\Git\IMDbApiApp\imdbClientButWorking\bin\Debug\net6.0-windows\imdbClientButWorking.exe";

        protected static WindowsDriver<WindowsElement> session;

        public static void Setup()
        {
            // Launch Calculator application if it is not yet launched
            if (session == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                // Note: Multiple calculator windows (instances) share the same process Id
                
                AppiumOptions opt = new AppiumOptions();
                opt.AddAdditionalCapability("app", AppId);
                opt.AddAdditionalCapability("deviceName", "WindowsPC");

                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), opt);
                //Assert.IsNotNull(session);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5.0);
                
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }

    }
}
