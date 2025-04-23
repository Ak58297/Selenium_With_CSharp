using OpenQA.Selenium;
using SeleniumWithCsharp.PageObjects;
using SeleniumWithCsharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWithCsharp.Tests
{
    [Parallelizable(ParallelScope.Children)] // if you will give .self in all class then all class methods will run parallely
    class HomePageTest: BaseClass
    {
       private HomePage hp;

        public override void  setUp()
        {
            hp = new HomePage(getDriver());
        }


        [Test]
        public void HomepageWithoutLogin()
        {
            hp.ValidateHomePage();
            hp.ValidateFooterGroupCompanies();
            hp.CheckGroupCompaniesInSeparateTab();
            hp.switchtoAllGroupCompaniesTab();
        }


        [Test, Category("Regression")]
      //  [Parallelizable(ParallelScope.All)]
        [TestCase("iphone 16 pro max", "Pro Max (Black Titanium, 512 GB)")]
        [TestCase("samsung s24 ultra", "Ultra 5G (Titanium Gray, 256 GB)")]
        public void SeachMobilePhones(string phoneName, string selectedVariant)
        {
            hp.SelectSearchedMobiles(phoneName, selectedVariant);
        }



       
    }
}
