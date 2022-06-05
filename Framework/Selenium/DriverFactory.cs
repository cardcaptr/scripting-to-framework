using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Framework.Selenium
{
    public static class DriverFactory
    {
        public static IWebDriver Build(string type, string browser)
        {
            if (type == "local")
            {
                switch (browser)
                {
                    case "chrome":
                        return BuildChromeDriver();

                    default:
                        throw new ArgumentException($"{browser} is not supported locally.");
                }
            }

            else if (type == "remote")
            {
                return BuildRemoteDriver(browser);
            }

            else
            {
                throw new ArgumentException($"{type} is invalid. Choose 'local' or 'remote'.");
            }
        }


        private static RemoteWebDriver BuildRemoteDriver(string browser)
        {
            var DOCKER_GRID_HUB_URI = new Uri("http://localhost:4444/wd/hub");

            RemoteWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions
                    {
                        BrowserVersion = "",
                        PlatformName = "LINUX",
                    };

                    chromeOptions.AddArgument("--start-maximized");

                    driver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, chromeOptions.ToCapabilities());
                    break;

                default:
                    throw new ArgumentException($"{browser} is not supported remotely.");
            }

            return driver;
        }

        private static ChromeDriver BuildChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            return new ChromeDriver(FW.WORKSPACE_DIRECTORY + PlatformDriver, options);
        }
        private static string PlatformDriver
        {
            get
            {
                return Environment.OSVersion.Platform.ToString().Contains("Win")
                    ? "_drivers/windows"
                    : "_drivers/mac";
            }
        }
    }
}
