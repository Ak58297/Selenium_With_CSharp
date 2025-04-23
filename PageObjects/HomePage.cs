using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWithCsharp.PageObjects
{
    class HomePage
    {

        private IWebDriver driver;
        private Actions actions;
        private IWebElement[] GroupCompanies;
        WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            actions = new Actions(driver);
        }
        //Page object factory

        [FindsBy(How = How.XPath, Using = "//picture[@title='Flipkart']/img[@title='Flipkart']")]
        private IWebElement HomebuttonLogo;// encapsulation: hiding complex implementation details

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Login')]")]
        private readonly IWebElement LoginButton;

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Cart')]")]
        private readonly IWebElement CartButton;

        [FindsBy(How = How.XPath, Using = "(//*[@class='_3I5N7v']) [2]")]
        private readonly IWebElement GroupCompaniesText;

        [FindsBy(How = How.XPath, Using = "((//*[@class='_3I5N7v']) [2])/following::a[text()='Myntra']")]
        private readonly IWebElement MyntraHyperlink;
        
        [FindsBy(How = How.XPath, Using = "((//*[@class='_3I5N7v']) [2])/following::a[text()='Cleartrip']")]
        private readonly IWebElement CleartripHyperlink;
        
        [FindsBy(How = How.XPath, Using = "((//*[@class='_3I5N7v']) [2])/following::a[text()='Shopsy']")]
        private readonly IWebElement ShopsyHyperLink;

        [FindsBy(How = How.XPath,Using = "//input[@class='Pke_EE']")]
        private IWebElement SearchBox;
        
        [FindsBy(How = How.XPath, Using = "//*[@class='KzDlHZ']")]
        private IList<IWebElement> mobilephone;
        
        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Available offers')]")]
        private IWebElement Buy_Without_Exchange;
        public IWebElement getHomebutton()
        {
            return HomebuttonLogo;
        }

        public IWebElement getLoginbutton()
        {
            return LoginButton;
        }

        public IWebElement getCartButton()
        {
            return CartButton;
        }

        public void ValidateHomePage()
        {
            Assert.Multiple(() =>
            {
                Assert.That(getHomebutton().Displayed, Is.True);
                Assert.That(getLoginbutton().Displayed, Is.True);
                Assert.That(getCartButton().Displayed, Is.True);

            });
        }

        public void ValidateFooterGroupCompanies()
        {

            Assert.Multiple(() =>
            {
                Assert.That(CleartripHyperlink.Displayed, Is.True);
                Assert.That(MyntraHyperlink.Displayed, Is.True);
                Assert.That(ShopsyHyperLink.Displayed, Is.True);
            });

        }


        public void CheckGroupCompaniesInSeparateTab()
        {
           GroupCompanies = new IWebElement[] { CleartripHyperlink, MyntraHyperlink , ShopsyHyperLink };
            foreach (IWebElement el in GroupCompanies)
            {
                actions.KeyDown(Keys.LeftControl).Click(el).KeyUp(Keys.LeftControl).Build().Perform();
            }
            Thread.Sleep(5000);
        }

        public void switchtoAllGroupCompaniesTab()
        {
            string ParentWindow = driver.CurrentWindowHandle;
            IList<string> windowHandles =driver.WindowHandles;
            int SizeOfWindows = windowHandles.Count;
            foreach(string window in windowHandles)
            {
                driver.SwitchTo().Window(window);
                string title=driver.Title;
                Console.WriteLine("<-------------->" + title);
            }
            driver.SwitchTo().Window(ParentWindow);
            Console.WriteLine(driver.Title);
        }

        public void SelectSearchedMobiles(String PhoneName,string phoneVarient)
        {
            SearchBox.SendKeys(PhoneName);
            SearchBox.SendKeys(Keys.Enter);
            //actions.KeyDown(Keys.Enter).KeyUp(Keys.Enter);
            foreach (IWebElement phone in mobilephone)
            {
                if (phone.Text.Contains(phoneVarient))
                {
                    phone.Click();
                    break;
                }
            }
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Navigate().Refresh();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(driver => Buy_Without_Exchange.Displayed);
            Assert.That(Buy_Without_Exchange.Displayed, Is.True);
        }
    }
}