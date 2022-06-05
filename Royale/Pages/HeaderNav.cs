using System;
using OpenQA.Selenium;
using Framework.Selenium;

namespace Royale.Pages
{
    public class HeaderNav 
    {
        public readonly HeaderNavMap Map;

        public HeaderNav()
        {
            Map = new HeaderNavMap();
        }

        public void GoToCardsPage()
        {
            Map.CardsTabLink.Click();
        }

    }

    public class HeaderNavMap
    {
        public IWebElement CardsTabLink =>  Driver.FindElement(By.CssSelector("a[href='/cards']"));

        public IWebElement DeckBuilderLink => Driver.FindElement(By.CssSelector("a[href='/deckbuilder']"));
    }
        
}