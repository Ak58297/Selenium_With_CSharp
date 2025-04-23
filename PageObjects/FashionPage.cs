using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumWithCsharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWithCsharp.PageObjects
{
    class FashionPage
    {
        private Actions action;
        private IWebDriver driver;
        public FashionPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);
            action = new Actions(driver);
        }

        [FindsBy(How = How.XPath, Using = "//*[text()='Fashion']")]
        private IWebElement Fashiondropdown;
        [FindsBy(How = How.XPath, Using = "//a[text()='Men Footwear']")]
        private IWebElement MenFootwearn;
        [FindsBy(How = How.XPath, Using = "//*[text()=\"Men's Casual Shoes\"]")]
        private IWebElement MensCasualShoe;
        [FindsBy(How = How.XPath, Using = "//*[text()='Popularity']")]
        private IWebElement PopularityText;


        public string NavigateToCasualShoeSection()
        {
            action.MoveToElement(Fashiondropdown).Build().Perform();
            action.MoveToElement(MenFootwearn).Build().Perform();
            action.MoveToElement(MensCasualShoe).Click().Build().Perform();
            Assert.That(PopularityText.Displayed, Is.True);
            return PopularityText.Text;
        }

    }
}
