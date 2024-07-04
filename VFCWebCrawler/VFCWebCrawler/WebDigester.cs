using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.Text;

namespace VFCWebCrawler
{
    internal partial class WebDigester(ChromeDriver driver)
    {
        private readonly ChromeDriver driver = driver;

        public Data Digest()
        {
            //Grab entire HTML site
            var htmlBody = (string)driver.ExecuteScript("return document.documentElement.outerHTML;");
            //Tidy up the data
            htmlBody = htmlBody.Replace(">", ">\n");
            var splitted = htmlBody.Split(new string[] { ">" }, StringSplitOptions.RemoveEmptyEntries);

            //Fill Data Object and return it
            var data = new Data
            {
                Followers = GetFollowersFromHTML(splitted),
                Name = GetNameFromHTML(splitted),
                Link = driver.Url,
                Email = ExtractEmails(htmlBody) ?? ""
            };
            return data;
        }

        public string? ExtractEmails(string data)
        {
            //Regex to get emails
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(data);

            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                sb.AppendLine(emailMatch.Value);
            }
            //store to file
            return sb.ToString();
        }

        public int GetFollowersFromHTML(string[] splitted)
        {
            string result = "";
            for (int i = 0; i < splitted.Length; i++)
            {
                //Looks for specific span html component with this code and title as attribute and then grabs the Followers
                if (splitted[i].Contains("_ac2a") && splitted[i].Contains("title="))
                {
                    result = splitted[i];
                }
            }
            //Cleanup
            result = result.Replace("<span class=\"_ac2a\" title=\"", "");
            result = result.Remove(result.IndexOf("\n"), 1);
            result = result.Remove(result.Length - 1, 1);
            result = result.Replace(".", "");

            return Int32.Parse(result);
        }

        public string GetNameFromHTML(string[] splitted)
        {
            string result = "";
            for (int i = 0; i < splitted.Length; i++)
            {
                //Looks for specific span html component with this code and then grabs the name
                if (splitted[i].Contains("x1lliihq x1plvlek xryxfnj x1n2onr6 x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs x1s928wv xhkezso x1gmr53x x1cpjm7i x1fgarty x1943h6x x1i0vuye xvs91rp x1s688f x5n08af x10wh9bi x1wdrske x8viiok x18hxmgj"))
                {
                    result = splitted[i + 1];
                }
            }
            //Cleanup
            result = result.Replace("</span", "");
            result = result.Remove(result.IndexOf("\n"), 1);

            return result;
        }

    }

    
}
