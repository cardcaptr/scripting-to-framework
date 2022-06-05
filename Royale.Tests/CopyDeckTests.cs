using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using Royale.Pages;
using Framework.Services;
using Framework.Selenium;
using System.Collections.Generic;
using Framework.Models;
using OpenQA.Selenium.Support.UI;
// using Royale.Tests.Base;

namespace Tests;

public class CopyDeskTests
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

    [Test]
    public void User_can_copy_the_deck()
    {
        // 2. go to Deck Builder page & Click "add cards manually"
        Pages.DeckBuilder.Goto().AddCardsManually();

        // 3. Click Copy Deck icon
        Pages.DeckBuilder.CopySuggestedDeck();

        // 4. Click Yes
        Pages.CopyDeck.Yes();
        
        // 5. Assert the "if click Yes..." message is displayed
        Assert.That(Pages.CopyDeck.Map.CopiedMessage.Displayed);
    } 

    [Test]
    public void User_opens_app_store()
    {
        Pages.DeckBuilder.Goto().AddCardsManually();
        Pages.DeckBuilder.CopySuggestedDeck();
        Pages.CopyDeck.No().OpenAppStore();
        Assert.That(Driver.Title, Is.EqualTo("Clash Royale on the App Store"));
    }

    [Test]
    public void User_opens_google_play()
    {
        Pages.DeckBuilder.Goto().AddCardsManually();
        Pages.DeckBuilder.CopySuggestedDeck();
        Pages.CopyDeck.No().OpenGooglePlay();
        Assert.That(Driver.Title, Is.EqualTo("Clash Royale on the App Store"));       
    }


  
}