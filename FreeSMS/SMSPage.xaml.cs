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
using System.Windows.Controls.Primitives;
using Coding4Fun.Phone.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using Inneractive.Nokia.Ad;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FreeSMS
{
     public class SMS
    {
        public string text { get; set; }
    }
    public partial class SMSPage : PhoneApplicationPage
    {
        IsolatedStorageSettings storage = IsolatedStorageSettings.ApplicationSettings;
        ObservableCollection<SMS> sentsms = new ObservableCollection<SMS>();
        int count = 3;
        string temp;
        private const string _key = "no";
        private AppSettings settings = new AppSettings();
        public SMSPage()
        {
            InitializeComponent();
            //grid1.Visibility = Visibility.Visible;
            grid2.Visibility = Visibility.Visible;
            grid3.Visibility = Visibility.Visible;
            grid4.Visibility = Visibility.Visible;
            //InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid1, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid2, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid3, 120);
            InneractiveAd.DisplayAd("MSP_FreeSMS_WP7", InneractiveAd.IaAdType.IaAdType_Banner, grid4, 120);
            textBox1.Text = App.mobile_nos;
            load_sentsms();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 10) { MessageBox.Show("Seems you have forgot to enter mobile no!"); return; }
            if (textBox2.Text == string.Empty) { MessageBox.Show("Blank messages does not convey anything ,right ? please type something ! :-)"); return; }
            else
            {
                //grid1.Visibility = Visibility.Collapsed;
                grid2.Visibility = Visibility.Collapsed;
                progressOverlay.Show();
                progressOverlay.Focus();
                //adcontrol3.Visibility = Visibility.Visible;
                //adcontrol4.Visibility = Visibility.Visible;
                temp = textBox1.Text;
                App.service_name = App.services[App.sms_service - 1];
                switch (App.sms_service)
                {
                    case 1:
                        if ((bool)checkBox1.IsChecked)
                        {
                            DateTime d = (DateTime)datePicker.Value;
                            
                            string ddmmyy = d.Day.ToString() + "0" + d.Month.ToString() + d.Year.ToString();
                            DateTime t = (DateTime)timePicker.Value;
                            if (timePicker.Value.ToString().Contains("PM"))
                                t.AddHours(12);
                            string time = t.Hour.ToString() + t.Minute.ToString();
                            send(settings.way2smsuid, settings.way2smspwd, textBox2.Text, textBox1.Text, ddmmyy, time);
                        }
                        else
                            send(settings.way2smsuid, settings.way2smspwd, textBox2.Text, textBox1.Text);
                        break;
                    case 2:
                        if ((bool)checkBox1.IsChecked)
                        {
                            DateTime d = (DateTime)datePicker.Value;
                            string ddmmyy = d.Day.ToString() + "0" + d.Month.ToString() + d.Year.ToString();
                            DateTime t = (DateTime)timePicker.Value;
                            if (timePicker.Value.ToString().Contains("PM"))
                                t.AddHours(12);
                            string time = t.Hour.ToString() + t.Minute.ToString();
                            send(settings.uid160by2, settings.pwd160by2, textBox2.Text, textBox1.Text, ddmmyy, time);
                        }
                        send(settings.uid160by2, settings.pwd160by2, textBox2.Text, textBox1.Text);
                        break;
                    case 3:
                        send(settings.uidfullonsms, settings.pwdfullonsms, textBox2.Text, textBox1.Text);
                        break;
                    case 4:
                        send(settings.uidsite2sms, settings.pwdsite2sms, textBox2.Text, textBox1.Text);
                        break;
                    case 5:
                        send(settings.uidsmsfi, settings.pwdsmsfi, textBox2.Text, textBox1.Text);
                        break;
                    case 6:
                        send(settings.uidfreesms8, settings.pwdfreesms8, textBox2.Text, textBox1.Text);
                        break;
                    case 7:
                        send(settings.uidsms440, settings.pwdsms440, textBox2.Text, textBox1.Text);
                        break;
                    case 8:
                        send(settings.uidbhokali, settings.pwdbhokali, textBox2.Text, textBox1.Text);
                        break;
                }
                //     else
                //{
                //    MessageBox.Show("Right now, we cant connect to internet ,make sure your data connection is turned on");
                //}
            }

        }

        private void send(string uid, string password, string message, string no, string ddmmyy, string time)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + uid + "&pwd=" + password + "&msg=" + message + "&phone=" + no + "&provider=" + App.service_name + "&future=1" + "&date=" + ddmmyy + "&time=" + time);
            httpWebRequest.BeginGetResponse(Response_Completed, httpWebRequest);
        }

        public void send(string uid, string password, string message, string no)
        {

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
                    //grid1.Visibility = Visibility.Visible;
                    grid2.Visibility = Visibility.Visible;
                    String resp_msg = "";
                    try
                    {
                        if (responseString.Equals("1"))
                        {
                            resp_msg = "SMS succesfully sent to " + temp;
                            if ((bool)checkBox1.IsChecked)
                                resp_msg = "SMS succesfully SCHEDULED to " + temp;
                            load_sentsms();
                            Save_SentSMS();
                        }
                        else if (responseString.Equals("-1"))
                        {
                            if (count > 0)
                            {
                                textBox2.Text += " ";
                                send(settings.way2smsuid, settings.way2smspwd, textBox2.Text, textBox1.Text);
                                --count;
                                return;
                            }
                            
                            resp_msg = "We are having a temporary problem with servers ,Please try again :(";
                        }
                        else if (responseString.Equals("-2"))
                            resp_msg = "This Mobile no is not registered in " + App.service_name + " ,Please register first!";
                        else if (responseString.Equals("-3"))
                            resp_msg = "Sorry,There seems to be a problem with your message text,Message Undelivered.";
                        else if (responseString.Equals("-4"))
                            resp_msg = "Incorrect login details ,please recheck!";
                    }
                    catch (NullReferenceException e)
                    {

                    }
                    App.resp_msg = resp_msg;
                    MessagePrompt messagePrompt = new MessagePrompt();
                    messagePrompt.Body = new BodyUserControl();
                    messagePrompt.Show();


                });
            }
            catch (Exception e)
            {
                //UIThread.Invoke(() => MessageBox.Show("Right now, we cant connect to internet ,make sure your data connection is turned on"));
                //UIThread.Invoke(() => NavigationService.Navigate(new Uri("/home.xaml", UriKind.Relative)));
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

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            sentbox.SelectedIndex = -1;
            try
            {
                if (App.comingfromspeech)
                {
                    textBox2.Text = App.text + " " + App.speechtext;
                    App.comingfromspeech = false;
                    NavigationService.RemoveBackEntry();
                }
                if (NavigationContext.QueryString.ContainsKey(_key))
                {
                    string no1 = NavigationContext.QueryString[_key];

                    if (no1.Length >= 10)
                    {
                        textBox1.Text = no1;
                    }
                    App.sms_service = settings.smsservice;

                    // Remove the no from the query string (important!)
                    NavigationContext.QueryString.Remove(_key);
                }                
            }
            catch (Exception eee) { };

        }

        //retreiving phone number
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PhoneNumberChooserTask task = new PhoneNumberChooserTask();
                task.Completed += (s, evt) =>
                {
                    if (evt.Error == null && evt.TaskResult == TaskResult.OK)
                    {
                        temp = evt.PhoneNumber;
                        if (temp != null && temp[0] == '0' && temp.Length > 10)
                            temp = temp.Substring(1);
                        else if (temp != null && temp.Contains("+91"))
                            temp = temp.Substring(3);
                        else if (temp != null && temp.Substring(0, 2).Equals("91") && temp.Length > 10)
                            temp = temp.Substring(2);
                        temp = temp.Replace("-", "");
                        temp = temp.Replace("(", "");
                        temp = temp.Replace(")", "");
                        temp = temp.Replace(" ", "");
                        if (textBox1.Text == string.Empty)
                            textBox1.Text = temp;
                        else
                            textBox1.Text += "," + temp;
                    }
                };
                task.Show();
            }
            catch (Exception exce)
            {
            }
        }

        //focusing textbox1
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            progressOverlay.Hide();
            if (App.sms_service == 1 || App.sms_service == 2)
                checkBox1.Visibility = Visibility.Visible;
            if (App.sms_onetime)
            {
                textBox1.Focus();
                App.sms_onetime = false;
            }
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Collapsed;
            textBlock2.Visibility = Visibility.Visible;
            textBlock3.Visibility = Visibility.Visible;
            datePicker.Visibility = Visibility.Visible;
            timePicker.Visibility = Visibility.Visible;
            datePicker.Value = DateTime.Now;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;
            textBlock2.Visibility = Visibility.Collapsed;
            textBlock3.Visibility = Visibility.Collapsed;
            datePicker.Visibility = Visibility.Collapsed;
            timePicker.Visibility = Visibility.Collapsed;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            App.mobile_nos = textBox1.Text;
            App.text = textBox2.Text;
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        private void ApplicationBarMenuItem_Click3(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
            marketplaceDetailTask.ContentIdentifier = "79e16d86-a739-44fd-942e-c7fdf26cdc22";
            marketplaceDetailTask.Show();
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            // If a location is currently shown and a secondary tile hasn't already
            // been created for it, create one
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("no=" + textBox1.Text));
                if (tile == null)
                {
                    settings.smsservice = App.sms_service;
                    StandardTileData data = new StandardTileData
                    {
                        BackgroundImage = new Uri("Images/Background.png", UriKind.Relative),
                        Title = textBox1.Text
                    };
                    string temp = "/SMSPage.xaml?" + _key + "=" + textBox1.Text;
                    ShellTile.Create(new Uri(temp, UriKind.Relative), data);
                }
            }

        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            switch (pivot.SelectedIndex)
            {
                case 0:
                    {
                        ApplicationBar = new ApplicationBar();
                        ApplicationBar.BackgroundColor = (Color)Resources["PhoneAccentColor"];
                        ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                        button1.IconUri = new Uri("/Images/appbar.send.text.rest.png", UriKind.Relative);
                        button1.Text = "Send";
                        ApplicationBar.Buttons.Add(button1);
                        button1.Click += new EventHandler(ApplicationBarIconButton_Click);
                        ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                        button2.IconUri = new Uri("/Images/Voice_Search.png", UriKind.Relative);
                        button2.Text = "Speak";
                        ApplicationBar.Buttons.Add(button2);
                        button2.Click += new EventHandler(ApplicationBarIconButton_Click_1);
                        ApplicationBarIconButton button3 = new ApplicationBarIconButton();
                        button3.IconUri = new Uri("/Images/Board_Pin.png", UriKind.Relative);
                        button3.Text = "Pin to start";
                        ApplicationBar.Buttons.Add(button3);
                        button3.Click += new EventHandler(ApplicationBarIconButton_Click_2);
                        ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
                        menuItem1.Text = "Clear mobile no";
                        ApplicationBar.MenuItems.Add(menuItem1);
                        menuItem1.Click += new EventHandler(ApplicationBarMenuItem_Click);
                        ApplicationBarMenuItem menuItem2 = new ApplicationBarMenuItem();
                        menuItem2.Text = "Clear message";
                        ApplicationBar.MenuItems.Add(menuItem2);
                        menuItem2.Click += new EventHandler(ApplicationBarMenuItem_Click3);
                        ApplicationBarMenuItem menuItem3 = new ApplicationBarMenuItem();
                        menuItem3.Text = "Get ad-free version";
                        ApplicationBar.MenuItems.Add(menuItem3);
                        menuItem3.Click += new EventHandler(ApplicationBarMenuItem_Click_1);
                        break;
                    }
                case 1:
                    {
                        ApplicationBar = new ApplicationBar();
                        ApplicationBar.BackgroundColor = (Color)Resources["PhoneAccentColor"];
                        ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                        button1.IconUri = new Uri("/Images/Forward.png", UriKind.Relative);
                        button1.Text = "Forward";
                        ApplicationBar.Buttons.Add(button1);
                        button1.Click += new EventHandler(SentSMS_Forward);
                        ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                        button2.IconUri = new Uri("/Images/Delete.png", UriKind.Relative);
                        button2.Text = "Delete";
                        ApplicationBar.Buttons.Add(button2);
                        button2.Click += new EventHandler(Delete_SentSMS);
                        ApplicationBarMenuItem menuItem1 = new ApplicationBarMenuItem();
                        menuItem1.Text = "Delete all sent SMS";
                        ApplicationBar.MenuItems.Add(menuItem1);
                        menuItem1.Click += new EventHandler(Deleteall_SentSMS);
                    }
                    load_sentsms(); break;
               
            }
        }

        private void load_sentsms()
        {            
                ObservableCollection<SMS> templist;
                if (storage.TryGetValue<ObservableCollection<SMS>>("SentHistory", out templist))
                { 
                    sentsms = templist; 
                }
                else
                {
                    storage.Add("SentHistory", sentsms);
                }
                sentbox.ItemsSource = sentsms;
        }
        private void Save_SentSMS()
        {
            string push = "• " + textBox2.Text ;
            sentsms.Insert(0, new SMS { text = push });
            sentbox.ItemsSource = sentsms;
            storage["SentHistory"] = sentsms;
        }
        private void SentSMS_Forward(object sender, EventArgs e)
        {
            if (sentbox.SelectedIndex != -1)
            {
                SMS n = sentbox.SelectedItem as SMS;
                textBox2.Text = n.text.Substring(2);
                pivot.SelectedIndex = 0;
            }
        }
        private void Delete_SentSMS(object sender, EventArgs e)
        {
            try
            {
                sentsms.RemoveAt(sentbox.SelectedIndex);
            }
            catch (Exception eop) { }
        }
        private void Deleteall_SentSMS(object sender, EventArgs e)
        {
            sentsms.Clear();
        }

        //private void ApplicationBarMenuItem_Click_2(object sender, EventArgs e)
        //{
        //    input.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(input_Completed);
        //    Background = new SolidColorBrush(Colors.Green);
        //    input.Title = "Add Signature";
        //    input.Message = "this will be added to end of your text";
        //    input.Show();
        //}
        //void input_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        //{
        //    settings.signature = input.InputBox.Text;
        //}

    }
}