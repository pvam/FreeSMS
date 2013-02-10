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
using Microsoft.Phone.Tasks;

namespace FreeSMS
{
    public partial class aboutpage : PhoneApplicationPage
    {
        public aboutpage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask m = new MarketplaceReviewTask();
            m.Show();
            MessageBox.Show("You are Awesome! Thanks for reviewing :-) ");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmailComposeTask mail = new EmailComposeTask();
            mail.To = "FreeSMSsupport@live.in";
            mail.Subject = "FreeSMS WP Version 2.0";
            mail.Show();

        }
    }
}