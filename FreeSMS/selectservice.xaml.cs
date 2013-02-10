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

namespace FreeSMS
{
    public partial class selectservice : PhoneApplicationPage
    {
        
        public selectservice()
        {
           
            InitializeComponent();
            this.defaultPicker.Items.Clear();
            this.defaultPicker.ItemsSource = new List<string>() { "Way2SMS", "160By2", "FullOnSMS", "Site2SMS", "SmsFI", "FreeSMS8", "SMS440", "BhokaliSMS" };
            defaultPicker.SelectedIndex = 0;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.service_name = defaultPicker.SelectedItem.ToString();
            App.sms_service =defaultPicker.SelectedIndex +1;
            if (App.status[App.sms_service-1] != true)
            {
                MessageBox.Show("You have not yet provided the login details for " + App.service_name +
                    "\nPress ok to enter login details.");
                NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
            App.sms_onetime = true;
            NavigationService.Navigate(new Uri("/SMSpage.xaml?speech=0", UriKind.Relative));
        }
    }
}