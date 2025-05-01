using SeleniumWithCsharp.PageObjects;
using SeleniumWithCsharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWithCsharp.Tests
{
    class FashionPageTest: BaseClass
    {
        private FashionPage fp;
        private ReusableMethods rm;
        public override void setUp()
        {
            fp = new FashionPage(getDriver());
            rm = new ReusableMethods();
        }


        [Test, Category("Regression")]
        public void CheckThedropDownOfFashionPage()
        {
            string text=fp.NavigateToCasualShoeSection();
            Console.WriteLine(rm.ReverseString(text));
        }
    }
}
