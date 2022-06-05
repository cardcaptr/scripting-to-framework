using System;
using OpenQA.Selenium;
using Framework.Models;
using Framework.Selenium;

namespace Royale.Pages
{
    public class CardDetailsPage : PageBase
    {
        public readonly CardDetailsPageMap Map;
        public CardDetailsPage()
        {
            Map = new CardDetailsPageMap();
        }
        
        public (string Type, int Arena) GetCardCategory()
        {
            var categories = Map.CardCategory.Text.Split(", ");
            return (categories[0].Trim().ToLower(),Int32.Parse(categories[1].Trim().Substring(6)));
        }    

        public Card GetBaseCard()
        {
            var (type, arena) = GetCardCategory();
            string rarity="";

            //Check if IWebElement exists
            if (Map.CardRarity().Count!=0)
            {                
                rarity=Map.CardRarity()[0].Text.Replace("Rarity","").Substring(2);
            }
         
            return new Card
            {
                Name = Map.CardName.Text,
                Rarity = rarity,
                Type = type,
                Arena = arena
            };
        } 

    }

    public class CardDetailsPageMap
    {
        //methods used in getters
        public IWebElement CardName => Driver.FindElement(By.CssSelector("[class*='cardName']"));
        public IWebElement CardCategory => Driver.FindElement(By.CssSelector(".card__rarity"));
        
        public IList <IWebElement> CardRarity()
        { 
            //FindElements used as doesn't throw an exception when no element is found
            return Driver.FindElements(By.CssSelector("[class*='rarityCaption']"));
        }

    }
    
     
}