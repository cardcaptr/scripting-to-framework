using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Selenium
{
    public class Wait
    {
        private readonly WebDriverWait _wait;

        public Wait(int seconds)
        {
            _wait = new WebDriverWait(Driver.Current, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            
            _wait.IgnoreExceptionTypes(
            typeof(NoSuchElementException),
            typeof(ElementNotVisibleException),
            typeof(StaleElementReferenceException)
            );
        }
                
        public bool Until(Func<IWebDriver, bool> condition)
        {
            return _wait.Until(condition);
        }
    }
}