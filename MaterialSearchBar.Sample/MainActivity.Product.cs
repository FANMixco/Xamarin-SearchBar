namespace MaterialSearchBar.Sample
{
    public partial class MainActivity
    {
        public class Product
        {
            public string Title { get; set; }
            public string SubTitle { get; set; }

            public Product(string title, string subTitle)
            {
                Title = title;
                SubTitle = subTitle;
            }
        }
    }
}