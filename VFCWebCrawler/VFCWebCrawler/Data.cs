namespace VFCWebCrawler
{
    internal class Data
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Followers { get; set; }

        public string ToString()
        {
            return this.Link + " " + this.Name + " " + this.Email + " " + this.Followers;
        }
    }
}
