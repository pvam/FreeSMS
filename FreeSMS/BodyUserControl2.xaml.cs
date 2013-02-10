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

namespace FreeSMS
{
    public partial class BodyUserControl2 : UserControl
    {
        public BodyUserControl2()
        {
            InitializeComponent();
        }

        private void textBlock1_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = App.resp_msg;
        }
    }
}
