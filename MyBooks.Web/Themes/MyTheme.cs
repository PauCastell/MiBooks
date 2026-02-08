using MudBlazor;

namespace MyBooks.Web.Themes
{
    public class MyTheme
    {
        public static MudTheme GetTheme()
        {
            return new MudTheme()
            {
                Typography = new Typography()
                {
                    Default = new DefaultTypography()
                    {
                        FontFamily = new[] { "Segoe UI","Calibri","Arial","sans-serif" },
                        FontSize = "14px",
                        LineHeight = "1.5",
                    },
                    H6 = new H6Typography()
                    {
                        FontSize = "16px",
                        FontWeight = "500",
                    },
                    Body2 = new Body2Typography()
                    {
                        FontSize = "13px",
                        LineHeight = "1.4",
                    }
                }
            };
        }
    }
}
