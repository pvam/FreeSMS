using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace FreeSMS
{
    public partial class home : PhoneApplicationPage
    {
        private AppSettings settings = new AppSettings();
        public home()
        {
            InitializeComponent();
            App.status = settings.status;
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                bool temp = settings.adpromo;
               // Debug.WriteLine(temp);
                if (!temp)
                    NavigationService.Navigate(new Uri("/Promo.xaml", UriKind.Relative));
            }
            catch (Exception n) {
                Debug.WriteLine(n.ToString());
            };
            base.OnNavigatedTo(e);
        }
        private void hubTile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 1;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 2;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
        private void hubTile3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 3;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
        private void hubTile4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 4;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 5;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 6;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 7;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.sms_service = 8;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/selectservice.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/helppage.xaml", UriKind.Relative));
        }
        void rateapp()
        {
            MessageBox.Show("You are Awesome! Thanks for reviewing :-) ");
            MarketplaceReviewTask m = new MarketplaceReviewTask();
            m.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            rateapp();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/aboutpage.xaml", UriKind.Relative));
        }

        private void dnd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/dnd.xaml", UriKind.Relative));
        }

        private void hubTile2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 2;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 1;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }


        private void hubTile1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 3;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 4;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile5_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 5;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile6_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 6;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile7_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 7;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile8_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.sms_service = 8;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void hubTile9_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/dnd.xaml", UriKind.Relative));
        }

        //private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        //{
        //    NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        //}
        //protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        //{
        //    ImageBrush img = new ImageBrush();
        //    switch (settings.background)
        //    {
        //        case 1:
        //            img.ImageSource = new BitmapImage(new Uri("/Images/Exercisebg.png", UriKind.Relative));
        //            img.Stretch = Stretch.UniformToFill;
        //            panorama.Background = img;
        //            break;
        //        case 2:
        //            img.ImageSource = new BitmapImage(new Uri("/Images/Homebg.png", UriKind.Relative));
        //            img.Stretch = Stretch.UniformToFill;
        //            panorama.Background = img;
        //            break;
        //        case 3:
        //            panorama.Background = new SolidColorBrush(Colors.Black);
        //            break;
        //    }
        //}
    }
}