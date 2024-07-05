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

            var driver = new ChromeDriver
            {
                Url = "https://www.google.com"
            };
            //Enter google search
            driver.FindElement(By.TagName("textarea")).SendKeys("site:\"www.instagram.com\"" + Keys.Return);
            //Get google search result links
            var searchResults = driver.FindElements(By.ClassName("yuRUbf"));
            var searchLinks = new List<string>();
            //Iterate over the Array of found Search Results and grab the link Element
            foreach (var x in searchResults)
            {
                searchLinks.Add(x.FindElement(By.TagName("a")).GetAttribute("href"));
            }

            var Data = new List<Data>();
            var digester = new WebDigester(driver);
            //Iterate over the Links and grab the actual text from the link and print it out to the console
            foreach (var x in searchLinks)
            {
                if(x == "www.instagram.com" || x == "instagram.com" || x == "https://www.instagram.com/" || x == "https://instagram.com/")
                {
                    continue;
                }
                driver.Navigate().GoToUrl(x);
                Thread.Sleep(3000);
                Data.Add(digester.Digest());
            }
            Data.ForEach(x => Console.WriteLine(x.ToString()));
            driver.Quit();
        }
    }
}
