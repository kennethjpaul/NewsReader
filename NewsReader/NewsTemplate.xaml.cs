using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NewsReader
{
    public sealed partial class NewsTemplate : UserControl
    {
        public NewsTemplate()
        {
            this.InitializeComponent();
        }



        public string Textfield
        {
            get { return (string)GetValue(textfieldProperty); }
            set { SetValue(textfieldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textfield.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textfieldProperty =
            DependencyProperty.Register("Textfield", typeof(string), typeof(NewsTemplate), null);



        public string UrlField
        {
            get { return (string)GetValue(UrlFieldProperty); }
            set { SetValue(UrlFieldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UrlField.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlFieldProperty =
            DependencyProperty.Register("UrlField", typeof(string), typeof(NewsTemplate), null);



        public ImageSource Imagefield
        {
            get { return (ImageSource)GetValue(ImagefieldProperty); }
            set { SetValue(ImagefieldProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Imagefield.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagefieldProperty =
            DependencyProperty.Register("Imagefield", typeof(ImageSource), typeof(NewsTemplate),null);











        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
            templateGrid.Background = new SolidColorBrush(Color.FromArgb(100, 207, 207, 207));
            
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
            templateGrid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        
    }
}
