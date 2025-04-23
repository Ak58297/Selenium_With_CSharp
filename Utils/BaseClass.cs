using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumWithCsharp.Utils
{

    class BaseClass
    {
        ExtentReports extent_reports;
        ExtentTest test;
        //private IWebDriver driver;
         ThreadLocal<IWebDriver> driver = new();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            
            Console.WriteLine("Path: " + reportPath);
            var htmlreporter = new ExtentHtmlReporter(reportPath);
            extent_reports = new ExtentReports();
            extent_reports.AttachReporter(htmlreporter);
            extent_reports.AddSystemInfo("UserName", "abhishek");
            extent_reports.AddSystemInfo("hostname", "localhost");

            extent_reports.AddSystemInfo("env", "test");





        }

        [SetUp]
        public void StartBrowser()
        {

          test=  extent_reports.CreateTest(TestContext.CurrentContext.Test.Name);
         //   String selecte    dBrowser = ConfigurationManager.AppSettings["browser"];
         // Console.WriteLine("<----->" + selectedBrowser);
            inItBrowser("Chrome");
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = getData("Url");
            setUp();
        }
        
        public void inItBrowser(string browserToOpen)
        {
            switch(browserToOpen)
            {
                case "Chrome":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        driver.Value = new ChromeDriver();
                        break;
                    }

                case "Firefox":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                        driver.Value = new FirefoxDriver();
                        break;
                    }

                case "Edge":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                        driver.Value = new EdgeDriver();
                        break;
                    }
            }            
        }

        public virtual void setUp()
        { }


        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public static string getData(string KeyName)
        {
            return new Json_Reader().ExtractData(KeyName);
        }


        public MediaEntityModelProvider CaptureScreenshot(IWebDriver driver,String ScreenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, ScreenShotName).Build();

        }



        [TearDown]
        public void AfterTest()
        {
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";


            if(status==TestStatus.Failed)
            {

                test.Fail("Test Failed", CaptureScreenshot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with a log trace:" + stacktrace);
            }
            else if(status==TestStatus.Passed)
                {

            }
//            extent_reports.Flush();

             driver.Value.Quit();
               // driver.Value.Dispose();
            

        }

    }
}
