using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Web;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NewsReader;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Threading;
using System.Threading.Tasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsReader.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class News : Page
    {
        public News()
        {
            this.InitializeComponent();
        }
        public delegate string Del(string message);

        public static event Del gotLink;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            gotLink += DelegateMethod;
          //  LoadingControl.IsLoading = true;
            string tmp = e.Parameter.ToString();
            LoadNewsItems(tmp);
          //  Task t = Task.Factory.StartNew(()=>LoadNewsItems(tmp));
            
        }

      

        public string DelegateMethod(string message)
        {
            Debug.WriteLine(message);
            LoadingControl.IsLoading = false;
            return "";
        }

        private string LoadNewsItems(string pageID)
        {
            
            var url= "https://www.facebook.com/pages_reaction_units/more/?page_id=" + pageID + "&cursor=%7B%22card_id%22%3A%22page_posts_divider%22%2C%22has_next_page%22%3Atrue%7D&surface=www_pages_home&unit_count=8&dpr=1&__user=0&__a=1";
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string encoded = client.DownloadString(url);

            string[] splitString = encoded.Split("\"jsmods\"");
            string shortString = splitString[0];

            string decoded = DecodeEncodedNonAsciiCharacters(shortString);
            string decoded_1 = Regex.Replace(decoded, @"\\""", "\"");
            string decoded_2 = Regex.Replace(decoded_1, @"\\/", "\\");
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(decoded_2);
            var node = htmlDoc.DocumentNode.SelectNodes("//div[@class='_6ks']/a");

            if (node != null)
            {
                foreach (var a in node)
                {


                    var link_unclean = a.Attributes["href"].Value;
                    var link_clean = GetCleanLink(link_unclean);

                    var img_unclean = a.FirstChild.FirstChild.FirstChild.Attributes["src"].Value;
                    var img_clean = GetCleanImg(img_unclean);

                    var title_unclean = a.FirstChild.FirstChild;
                    string title_clean = GetCleanTitle(title_unclean.InnerHtml);

                   // Debug.WriteLine(link_clean);
                   // Debug.WriteLine("");

                    var tile = new NewsTemplate
                    {
                        Textfield = title_clean,
                        Imagefield = new BitmapImage(new Uri(img_clean, UriKind.Absolute)),
                        UrlField = link_clean

                    };
                    tile.PointerReleased += NavigateToWebPage;

                    scrollTest2.Children.Add(tile);

                }
            }
            return gotLink("Done");
        }

        private void NavigateToWebPage(object sender, PointerRoutedEventArgs e)
        {

            var tile = sender as NewsTemplate;
            var url = tile.UrlField;
            var url_uri = new System.Uri(url);
            Debug.WriteLine(url);
            WebBrowser.Navigate(url_uri);
            WebBrowser.Visibility = Visibility.Visible;
            hideBrowser.Visibility = Visibility.Visible;
        }

        string GetCleanLink(string link)
        {

            var linkPattern = new Regex(@"(?<=u=)(.*)(?=&amp)");
            var link_clean = linkPattern.Match(link).Value;
            string cleanTitle = HttpUtility.UrlDecode(link_clean);

            return cleanTitle;
        }

        string GetCleanTitle(string title)
        {
            var pattern = new Regex(@"(?<=""hover"">)(.*?)(?=<\\a><\\div><div class)");
            string title_clean = pattern.Match(title).Value;
            title_clean = WebUtility.HtmlDecode(title_clean);
            return title_clean;
        }

        string GetCleanImg(string image)
        {
            var img_pattern = new Regex(@"(?<=url=)(.*?)(?=&amp)");
            var img_clean = img_pattern.Match(image).Value;
            img_clean = HttpUtility.UrlDecode(img_clean);

            return img_clean;
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        private void HideBrowser_Click(object sender, RoutedEventArgs e)
        {
            WebBrowser.Visibility = Visibility.Collapsed;
            hideBrowser.Visibility = Visibility.Collapsed;
        }
    }
}
