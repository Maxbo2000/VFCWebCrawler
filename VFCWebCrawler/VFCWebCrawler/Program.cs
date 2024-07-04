using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            var test = driver.FindElements(By.ClassName("yuRUbf"));
            var testElements = new List<string>();
            //Iterate over the Array of found Search Results and grab the link Element
            foreach (var x in test) 
            {
                testElements.Add(x.FindElement(By.TagName("a")).GetAttribute("href"));
            }

            var testNames = new List<IWebElement>();
            //Iterate over the Links and grab the actual text from the link and print it out to the console
            foreach (var x in testElements)
            {
                if(x == "www.instagram.com" || x == "instagram.com" || x == "https://www.instagram.com/" || x == "https://instagram.com/")
                {
                    continue;
                }
                driver.Navigate().GoToUrl(x);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
                testNames.Add(driver.FindElement(By.XPath("//span[starts-with(@class, 'x1lliihq x1plvlek')]")));
            }
            testNames.ForEach(x => Console.WriteLine(x.Text));
        }
    }
}
