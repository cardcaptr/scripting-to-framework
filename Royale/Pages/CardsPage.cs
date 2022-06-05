using System;
using Framework.Selenium;
using OpenQA.Selenium;

namespace Royale.Pages
{
    public class CardsPage : PageBase
    {
        public readonly CardsPageMap Map;
        public CardsPage()
        {
            Map = new CardsPageMap();
        }

        public CardsPage GoTo()
        {
            HeaderNav.GoToCardsPage();
            return this;
        }
        public IWebElement GetCardByName ( string cardName)
        {
            if (cardName.Contains(" "))
            {
                cardName = cardName.Replace(" ", "+");
            }

            return Map.Card(cardName);
        }

    }

    public class CardsPageMap
    {
        public IWebElement Card(string name) => Driver.FindElement(By.CssSelector($"a[href*='{name}']"));

    }
    
    
}