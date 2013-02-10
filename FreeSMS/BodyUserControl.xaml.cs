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
using Microsoft.Phone.Tasks;
using Inneractive.Nokia.Ad;

namespace FreeSMS
{
    public partial class BodyUserControl : UserControl
    {
        public BodyUserControl()
        {
            InitializeComponent();
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid1, 120);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = App.resp_msg;
        }

        private void image1_Tap(object sender, GestureEventArgs e)
        {
            MarketplaceReviewTask m = new MarketplaceReviewTask();
            m.Show();
        }
    }
}
