using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLibrary;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsReader.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPage : Page
    {
        List<string> comboList = new List<string>();
        public AddPage()
        {
            this.InitializeComponent();
            comboList.Add("News");
            comboList.Add("Memes");

        }

        private async void AddData_Click(object sender, RoutedEventArgs e)
        {
            string pageNameData = PageName.Text;
            string pageUrlData = UrlName.Text;

            pageNameData = pageNameData.Trim();
            pageUrlData = pageUrlData.Trim();


            if(comboBoxList.SelectedIndex==-1)
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Category Empty",
                    Content = "Please Select a Category ",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }

            else if(string.IsNullOrEmpty(pageNameData) || string.IsNullOrEmpty(pageUrlData))
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Empty Fields",
                    Content = "Please Fill all the fields ",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
            else if (!pageUrlData.Contains("https://www.facebook.com/"))
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "URL Format is incorrect",
                    Content = "Please provide the url in the following format \n https://www.facebook.com/[page_name]/ ",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
            else
            {
                if(comboBoxList.SelectedIndex==0)
                {
                    Add_Data_News(pageNameData, pageUrlData);
                }
                else if(comboBoxList.SelectedIndex==1)
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Not Implemented",
                        Content = "Memes category not implemented ",
                        CloseButtonText = "Ok"
                    };

                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
            }

        }

        private async void Add_Data_News(string pageName, string pageUrl)
        {
            var html = @pageUrl;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            string pageID;

            var anchors = htmlDoc.DocumentNode.SelectNodes("//meta[@property='al:android:url']");

            if(anchors!=null)
            {
                string temp = anchors.First().Attributes["content"].Value;
                var pattern = new Regex(@"(?<=page\/)(.*)(?=\?refe)");
                pageID = pattern.Match(temp).Value;

                bool checkExist = DataAccess.CheckID_News(pageID);

                if(checkExist)
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Entry Already Exist ",
                        CloseButtonText = "Ok"
                    };

                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                }
                else
                {
                    DataAccess.AddData_News(pageName, pageUrl, pageID);
                    PageName.Text = string.Empty;
                    UrlName.Text = string.Empty;
                }
                
            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Error getting the page, please recheck the url ",
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }
        }
    }
}
