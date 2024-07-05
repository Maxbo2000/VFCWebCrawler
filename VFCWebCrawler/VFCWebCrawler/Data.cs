namespace VFCWebCrawler
{
    internal class Data
    {
        public string Link { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Followers { get; set; }

        public override string ToString()
        {
            return Link + " " + Name + " " + Email + " " + Followers;
        }
    }
}
