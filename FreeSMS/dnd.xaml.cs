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

namespace FreeSMS
{
    public partial class dnd : PhoneApplicationPage
    {
        string temp;
        public dnd()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            temp = textBox1.Text;
            progressOverlay.Show();
            progressOverlay.Focus();
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://cbsserver.zni.in/dnd/?phone=" +temp+"&apikey="+App.apikey);
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
                    String resp_msg = "";
                    try
                    {
                        if (responseString.Equals("0"))
                            resp_msg = temp + " Is not registered for DND";
                        else if (responseString.Equals("1"))
                            resp_msg = temp + " Is registered for Do not Distrub service";
                        else if (responseString.Equals("-1"))
                            resp_msg = "There were some issues with your internet connection!";
                        else if (responseString.Equals("404"))
                            resp_msg = "Sorry for the inconvenience,Something went wrong! We will fix this bug in next version";
                        else if (responseString.Equals("400"))
                            resp_msg = "We have Overwhelming requests at this time,Please try later.Sorry for the inconvenience";
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            progressOverlay.Hide();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            PhoneNumberChooserTask task = new PhoneNumberChooserTask();
            task.Completed += (s, evt) =>
            {
                if (evt.Error == null && evt.TaskResult == TaskResult.OK)
                {
                    temp = evt.PhoneNumber;
                    if (temp != null && temp.Contains("+91"))
                        temp = temp.Substring(3);
                    else if (temp != null && temp.Substring(0, 2).Equals("91") && temp.Length > 10)
                        temp = temp.Substring(2);
                    textBox1.Text = temp;
                }
            };
            task.Show(); 
        }
    }
}