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
using System.Windows.Threading;
using Microsoft.Phone.Tasks;
using Inneractive.Nokia.Ad;

namespace FreeSMS
{
    public partial class Login : PhoneApplicationPage
    {
        string message = "Hi,i am using your app. Device :WP7.5 ,Version 2.0 ,Service : " + App.services[App.sms_service - 1];
        int count = 3;
        private AppSettings settings = new AppSettings();
        public Login()
        {
            InitializeComponent();
            grid1.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
            grid3.Visibility = Visibility.Visible;
            grid4.Visibility = Visibility.Visible;
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid1, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid2, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid3, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid4, 120);
            textBox1.Focus();
            App.status = settings.status;
        }

        //loading
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            progressOverlay.Hide();
            switch (App.sms_service)
            {
                case 1:
                    ApplicationTitle.Text = "Way2SMS";
                    if (App.status[0])
                    {
                        textBox1.Text = settings.way2smsuid;
                        passwordBox1.Password = settings.way2smspwd;
                    }
                    break;
                case 2:
                    ApplicationTitle.Text = "160By2";
                    if (App.status[1])
                    {
                        textBox1.Text = settings.uid160by2;
                        passwordBox1.Password = settings.pwd160by2;
                    }
                    break;
                case 3:
                    ApplicationTitle.Text = "fullonsms";
                    if (App.status[2])
                    {
                        textBox1.Text = settings.uidfullonsms;
                        passwordBox1.Password = settings.pwdfullonsms;
                    }
                    break;
                case 4:
                    ApplicationTitle.Text = "site 2 sms";
                    if (App.status[3])
                    {
                        textBox1.Text = settings.uidsite2sms;
                        passwordBox1.Password = settings.pwdsite2sms;
                    }
                    break;
                case 5:
                    ApplicationTitle.Text = "smsfi.com";
                    if (App.status[4])
                    {
                        textBox1.Text = settings.uidsmsfi;
                        passwordBox1.Password = settings.pwdsmsfi;
                    }
                    break;
                case 6:
                    ApplicationTitle.Text = "free SMS8";
                    if (App.status[5])
                    {
                        textBox1.Text = settings.uidfreesms8;
                        passwordBox1.Password = settings.pwdfreesms8;
                    }
                    break;
                case 7:
                    ApplicationTitle.Text = "SMS440";
                    if (App.status[6])
                    {
                        textBox1.Text = settings.uidsms440;
                        passwordBox1.Password = settings.pwdsms440;
                    }
                    break;
                case 8:
                    ApplicationTitle.Text = "Bhokali SMS";
                    if (App.status[7])
                    {
                        textBox1.Text = settings.uidbhokali;
                        passwordBox1.Password = settings.pwdbhokali;
                    }
                    break;
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            App.loginno = textBox1.Text;
            App.loginpwd = passwordBox1.Password;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                textBox1.Text = App.loginno;
                passwordBox1.Password = App.loginpwd;
            }
            catch (Exception n) { };
        }

        public void send(string uid, string password, string message, string no)
        {
            grid1.Visibility = Visibility.Collapsed;
            grid2.Visibility = Visibility.Collapsed;
            progressOverlay.Show();
            progressOverlay.Focus();

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + uid + "&pwd=" + password + "&msg=" + message + "&phone=" + no + "&provider=" + App.service_name);
            httpWebRequest.BeginGetResponse(Response_Completed, httpWebRequest);
        }

        void Response_Completed(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(response.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                response.Close();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    progressOverlay.Hide();
                    try
                    {
                        if (responseString.Equals("1"))
                        {
                            MessageBox.Show("Congratulations Login successful and auto-login is enabled ! From next time you wont need to login into " + App.services[App.sms_service - 1]);
                            App.status[App.sms_service - 1] = true;
                            settings.status = App.status;
                            switch (App.sms_service)
                            {
                                case 1:
                                    settings.way2smsuid = textBox1.Text;
                                    settings.way2smspwd = passwordBox1.Password;
                                    break;
                                case 2:
                                    settings.uid160by2 = textBox1.Text;
                                    settings.pwd160by2 = passwordBox1.Password;
                                    break;
                                case 3:
                                    settings.uidfullonsms = textBox1.Text;
                                    settings.pwdfullonsms = passwordBox1.Password;
                                    break;
                                case 4:
                                    settings.uidsite2sms = textBox1.Text;
                                    settings.pwdsite2sms = passwordBox1.Password;
                                    break;
                                case 5:
                                    settings.uidsmsfi = textBox1.Text;
                                    settings.pwdsmsfi = passwordBox1.Password;
                                    break;
                                case 6:
                                    settings.uidfreesms8 = textBox1.Text;
                                    settings.pwdfreesms8 = passwordBox1.Password;
                                    break;
                                case 7:
                                    settings.uidsms440 = textBox1.Text;
                                    settings.pwdsms440 = passwordBox1.Password;
                                    break;
                                case 8:
                                    settings.uidbhokali = textBox1.Text;
                                    settings.pwdbhokali = passwordBox1.Password;
                                    break;
                            }
                            NavigationService.Navigate(new Uri("/SMSPage.xaml", UriKind.Relative));
                        }
                        else if (responseString.Equals("-1"))
                        {
                            if (count > 0)
                            {
                                message+= " ";
                                send(settings.way2smsuid, settings.way2smspwd,message, textBox1.Text);
                                --count;
                                return;
                            }
                            MessageBox.Show("We are having a temporary problem with servers ,Please try again :(");
                        }
                        else if (responseString.Equals("-2"))
                            MessageBox.Show("Mobile no is not registered for this web service ,Please register first and then try ");
                        else if (responseString.Equals("-3"))
                            MessageBox.Show("Sorry,There seems to be a problem with your message text ,Message Undelivered.");
                        else if (responseString.Equals("-4"))
                            MessageBox.Show("Incorrect login details ,please recheck!");
                        settings.status = App.status;
                    }
                    catch (NullReferenceException e)
                    {

                    }
                });
            }
            catch (Exception e)
            {
                Exception exception = e;
                base.Dispatcher.BeginInvoke(() =>
                {
                    if (exception.Message == "The remote server returned an error: NotFound.")
                    {
                        MessageBox.Show("No internet connectivity ,Make sure your data connection is turned on");
                    }
                });
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == string.Empty || passwordBox1.Password == string.Empty)
                return;
            App.service_name = App.services[App.sms_service - 1];
            switch (App.sms_service)
            {
                case 1:
                    if (settings.way2smsuid == textBox1.Text && settings.way2smspwd == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 2:
                    if (settings.uid160by2 == textBox1.Text && settings.pwd160by2 == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 3:
                    if (settings.uidfullonsms == textBox1.Text && settings.pwdfullonsms == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 4:
                    if (settings.uidsite2sms == textBox1.Text && settings.pwdsite2sms == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 5:
                    if (settings.uidsmsfi == textBox1.Text && settings.pwdsmsfi == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 6:
                    if (settings.uidfreesms8 == textBox1.Text && settings.pwdfreesms8 == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 7:
                    if (settings.uidsms440 == textBox1.Text && settings.pwdsms440 == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
                case 8:
                    if (settings.uidbhokali == textBox1.Text && settings.pwdbhokali == passwordBox1.Password)
                        naviagte();
                    else
                        send(textBox1.Text, passwordBox1.Password, message, "7893610972");
                    break;
            }
        }

        private void naviagte()
        {
            NavigationService.Navigate(new Uri("/SMSPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text =string.Empty;
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            passwordBox1.Password = string.Empty;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string uri=string.Empty;
            switch (App.sms_service)
            {
                case 1: uri = "http://site5.way2sms.com/jsp/UserRegistration.jsp?id=5WYiObjLlkjMZi3Fmn+vI+cOfYzRWu0K"; break;
                case 2: uri = "http://www.160by2.com/UserReg"; break;
                case 3: uri = "http://fullonsms.com/Register.php"; break;
                case 4: uri = "http://www.site2sms.com/userregistration.asp"; break;
                case 5: uri = "http://www.smsfi.com/free-sms/"; break;
                case 6: uri = "http://www.freesms8.in/Free-SMS-India-Mobile-Registration.aspx"; break;
                case 7: uri = "http://sms440.com/indexReg.aspx"; break;
                case 8: uri = "http://www.bhokalisms.com/Default.aspx"; break;
            }
            WebBrowserTask webBrowserTask = new WebBrowserTask();

            webBrowserTask.Uri = new Uri(uri, UriKind.Absolute);

            webBrowserTask.Show();

        }


    }

}