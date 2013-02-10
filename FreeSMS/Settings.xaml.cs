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
using FreeSMS;

namespace FreeSMS
{
    public partial class settings : PhoneApplicationPage
    {
        private AppSettings _settings = new AppSettings();
        public settings()
        {
            InitializeComponent();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool) radioButton1.IsChecked)
            {
                _settings.background = 1;
            }
            else if ((bool)radioButton2.IsChecked)
            {
                _settings.background = 2;
            }
            else if((bool)radioButton3.IsChecked)
            {
                _settings.background = 3;
            }
            NavigationService.Navigate(new Uri("/home.xaml", UriKind.Relative));
        }
    }
}