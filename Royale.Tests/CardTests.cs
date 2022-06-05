using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using Royale.Pages;
using Framework.Services;
using Framework.Selenium;
using System.Collections.Generic;
using Framework.Models;
// using Royale.Tests.Base;

namespace Tests;

public class Tests
{

    

    [SetUp]
    public void BeforeEach()
    {
        Driver.Init();
        Pages.Init();
        // need to maximize window on Windows so the proper elements appear
        Driver.Current.Manage().Window.Maximize();

        // 1. go to statsroyale.com
        Driver.GoTo("https://statsroyale.com/");
        
    }
   
    [TearDown]
    public void AfterEach()
    {
        Driver.Quit();
    }

    static IList<Card> apiCards = new ApiCardService().GetAllCardsAsync().Result;


    [Test]
    // [Parallelizable(ParallelScope.Children)]
    // [TestCaseSource("apiCards")]

    public void Ice_Spirit_is_on_Cards_Page()
    {
       IceSpiritCard card = new IceSpiritCard();
        // // 2. click cards link in header nav
        // Driver.Current.FindElement(By.CssSelector("a[href='/cards']")).Click();
        // // Assert ice spirit is displayed
        // var iceSpirit = Driver.Current.FindElement(By.CssSelector("a[href*='Ice+Spirit']"));
        // Assert.That(iceSpirit.Displayed);  

        IWebElement cardToTest = Pages.Cards.GoTo().GetCardByName(card.Name);
        Assert.That(cardToTest.Displayed);
        
    }

    [Test]
    public void Ice_Spirit_Headers_are_correct_on_Cards_Detail_Page()
    {
        // // 1. go to statsroyale.com
        // Driver.Current.Url = "https://statsroyale.com/";
        // // 2. click cards link in header nav
        // Driver.Current.FindElement(By.CssSelector("a[href='/cards']")).Click();
        // // 3. Click ice spirit card
        // Driver.Current.FindElement(By.CssSelector("a[href*='Ice+Spirit']")).Click();
        // // 4. assert basic information and stats in headers
        // var cardName = Driver.Current.FindElement(By.CssSelector("[class*='cardName']")).Text;
        // var cardCategories = Driver.Current.FindElement(By.CssSelector(".card__rarity")).Text.Split(", ");
        // var cardType = cardCategories[0];
        // var cardArena = cardCategories[1];
        // var cardRarity = Driver.Current.FindElement(By.CssSelector("[class*='card__common']")).Text;

        //1. go to card page
        new CardsPage().GoTo().GetCardByName("Giant").Click();
        var cardDetailsPage = new CardDetailsPage();
        var (type, arena) = cardDetailsPage.GetCardCategory();
        var cardName = cardDetailsPage.Map.CardName.Text;
        var cardRarity = cardDetailsPage.Map.CardRarity()[0].Text.Replace("Rarity","").Substring(2);

        Assert.AreEqual("Ice Spirit", cardName);
        Assert.AreEqual("Troop", type);
        Assert.AreEqual("Arena 8",arena);
        Assert.AreEqual("Common",cardRarity);
    }
    
    [Test, Category("cardss")]
    [Parallelizable(ParallelScope.Children)]
    [TestCaseSource("apiCards")]
    public void Card_Headers_are_correct_on_Cards_Detail_Page(Card card)
    {
        //1. go to card page
        Pages.Cards.GoTo().GetCardByName(card.Name).Click();
        
        var cardOnPage = Pages.CardDetails.GetBaseCard();

        if (cardOnPage.Type == "troop")
                cardOnPage.Type = "character";
        Assert.AreEqual(card.Name, cardOnPage.Name);
        Assert.That(card.Type.Contains(cardOnPage.Type));
        Assert.AreEqual(card.Arena, cardOnPage.Arena);
        // Assert.AreEqual(card.Rarity,cardOnPage.Rarity);
    }

}