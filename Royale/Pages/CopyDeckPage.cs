using System;
using OpenQA.Selenium;
using Framework.Selenium;

namespace Royale.Pages
{

    public class CopyDeckPage : PageBase
    {
        public readonly CopyDeckPageMap Map;

        public CopyDeckPage()
        {
            Map = new CopyDeckPageMap();
        }

        public void Yes()
        {
            Map.YesButton.Click();
            Driver.Wait.Until(drvr => Map.CopiedMessage.Displayed);

        }

    }

    public class CopyDeckPageMap
    {
        public IWebElement YesButton =>  Driver.FindElement(By.Id("button-open"));
        
        public IWebElement CopiedMessage => Driver.FindElement(By.CssSelector(".notes.active"));
    }
        
}