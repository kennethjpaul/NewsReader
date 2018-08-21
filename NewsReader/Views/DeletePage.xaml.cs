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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NewsReader.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeletePage : Page
    {
        List<string> deletePagesList = new List<string>();

        public DeletePage()
        {
            this.InitializeComponent();
            deletePagesList.Clear();
            deletePagesList = DataAccess.GetData_News();

         
        }

        private async void DeleteList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(deleteList.SelectedItem!=null)
            {
                string pageName = deleteList.SelectedItem.ToString();
                Debug.WriteLine(deleteList.SelectedItem.ToString
                    ());

                ContentDialog deleteFileDialog = new ContentDialog
                {
                    Title = "Delete "+pageName +" permanently?",
                    Content = "If you delete this entry, you won't be able to access the content in that page. Do you want to delete?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };

                ContentDialogResult result = await deleteFileDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    DataAccess.Delete_News_Entry(pageName);
                    deletePagesList.Clear();
                    deletePagesList = DataAccess.GetData_News();
                    deleteList.ItemsSource = deletePagesList; 
                }
            }
        }
    }
}
