using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VFCWebCrawler
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            var driver = new ChromeDriver();
            driver.Url = "https://www.google.com";
            driver.FindElement(By.TagName("textarea")).SendKeys("site:\"www.instagram.com\"" + Keys.Return);
            //Get google search result links
            var test = driver.FindElements(By.ClassName("yuRUbf"));
            var testElements = new List<string>();
            //Iterate over the Array of found Search Results and grab the link Element
            foreach (var x in test)
            {
                testElements.Add(x.FindElement(By.TagName("a")).GetAttribute("href"));
            }

            var Data = new List<Data>();
            WebDigester digester = new WebDigester(driver);
            //Iterate over the Links and grab the actual text from the link and print it out to the console
            foreach (var x in testElements)
            {
                if(x == "www.instagram.com" || x == "instagram.com" || x == "https://www.instagram.com/" || x == "https://instagram.com/")
                {
                    continue;
                }
                driver.Navigate().GoToUrl(x);
                Thread.Sleep(3000);
                Data.Add(digester.Digest());
            }
            driver.Quit();
        }
    }
}
