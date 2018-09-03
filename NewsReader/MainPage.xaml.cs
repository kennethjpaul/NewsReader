using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using System.Diagnostics;
using Windows.ApplicationModel.Core;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NewsReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Main_List_Navigation();
            //DataAccess.deleteDatabase();
        }


        //////////////////////// NAVIGATION PANEL LOAD FUNCTIONS ///////////////////////////

        private void Main_List_Navigation()
        {

            /// CLEARING AND ADDING CLICK FUNCTIONS
            navigationPanel.MenuItems.Clear();
            navigationPanel.ItemInvoked += Home_click;
            navigationPanel.ItemInvoked -= News_click;
            navigationPanel.ItemInvoked -= Memes_click;
            navigationPanel.ItemInvoked -= Edit_click;


            // BACK BUTTON

            navigationPanel.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;


            // LOAD DEFAULT


            // ADDING MENU ITEMS

            navigationPanel.MenuItems.Add(new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Globe),
                Tag = "News",
                Content = new TextBlock
                {
                    Text = "News",
                    Tag = "Nav_News"
                }
            });
            navigationPanel.MenuItems.Add(new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Emoji2),
                Tag = "Memes",
                Content = new TextBlock
                {
                    Text = "Memes",
                    Tag = "Nav_Memes"
                }
            });
            navigationPanel.MenuItems.Add(new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Edit),
                Tag = "EditPage",
                Content = new TextBlock
                {
                    Text = "Edit Page",
                    Tag = "Nav_EditPage"
                }
            });
        }

        private void News_List_Navigation()
        {

            /// CLEARING AND ADDING CLICK FUNCTIONS
            navigationPanel.MenuItems.Clear();
            navigationPanel.ItemInvoked -= Home_click;
            navigationPanel.ItemInvoked += News_click;
            navigationPanel.ItemInvoked -= Memes_click;
            navigationPanel.ItemInvoked -= Edit_click;

            // BACK BUTTON

            navigationPanel.IsBackButtonVisible = NavigationViewBackButtonVisible.Visible;

            // LOAD DEFAULT


            // ADDING MENU ITEMS

            List<String> News_List = DataAccess.GetData_News();

            foreach (var member in News_List)
            {
                navigationPanel.MenuItems.Add(new NavigationViewItem
                {
                    Content = new TextBlock
                    {
                        Text = member,
                        Tag = member 
                    },
                    Icon = new SymbolIcon(Symbol.World),
                });
            }

        }

        private void Memes_List_Navigation()
        {

        }

        private void Edit_List_Navigation()
        {
            /// CLEARING AND ADDING CLICK FUNCTIONS
            
            navigationPanel.MenuItems.Clear();
            navigationPanel.ItemInvoked -= Home_click;
            navigationPanel.ItemInvoked -= News_click;
            navigationPanel.ItemInvoked -= Memes_click;
            navigationPanel.ItemInvoked += Edit_click;

            // BACK BUTTON

            navigationPanel.IsBackButtonVisible = NavigationViewBackButtonVisible.Visible;

            // LOAD DEFAULT


            // ADDING MENU ITEMS

            navigationPanel.MenuItems.Add(new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Globe),
                Tag = "Add Page",
                Content = new TextBlock
                {
                    Text = "Add Page",
                    Tag = "Nav_Add_Page"
                }
            });
            navigationPanel.MenuItems.Add(new NavigationViewItem
            {
                Icon = new SymbolIcon(Symbol.Emoji2),
                Tag = "Delete Page",
                Content = new TextBlock
                {
                    Text = "Delete Page",
                    Tag = "Nav_Delete_Page"
                }
            });
        }






        //////////////////////// NAVIGATION ITEMS CLICK FUNCTIONS ///////////////////////////

        private void Home_click(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            Debug.WriteLine(ItemContent.Tag);
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_News":
                        //MasterFrame.Navigate(typeof(Views.NewsItems));
                        News_List_Navigation();
                        break;
                    case "Nav_Memes":
                        Memes_List_Navigation();
                        break;
                    case "Nav_EditPage":
                        Edit_List_Navigation();
                        break;
                }
            }
        }

        private void News_click(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
            {
                string pageID = DataAccess.GetID_News(ItemContent.Tag.ToString());
                contentFrame.Navigate(typeof(Views.News),pageID);
            }
        }

        private void Memes_click(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

        }
        private void Edit_click(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;

            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_Add_Page":
                        contentFrame.Navigate(typeof(Views.AddPage));
                        break;
                    case "Nav_Delete_Page":
                        contentFrame.Navigate(typeof(Views.DeletePage));
                        break;
                }
            }

        }


        private void BackButton_Clicked(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            Main_List_Navigation();

            
        }


    }
}
