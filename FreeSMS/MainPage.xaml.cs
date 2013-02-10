using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;
using com.nuance.nmdp.speechkit;
using com.nuance.nmdp.speechkit.oem;
using com.nuance.nmdp.speechkit.util;
using com.nuance.nmdp.speechkit.util.audio;
using FreeSMS;
using System.Windows.Media.Imaging;

namespace FreeSMS
{
    public delegate void CancelSpeechKitEventHandler();

    public partial class MainPage : PhoneApplicationPage, RecognizerListener, VocalizerListener
    {

        SpeechKit _speechKit = null;
        Recognizer _recognizer = null;
        Vocalizer _vocalizer = null;
        Prompt _beep = null;
        OemConfig _oemconfig = new OemConfig();
        object _handler = null;
        string _ttsText = string.Empty;
        string _ttsVoice = string.Empty;
        Popup _popup = new Popup();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            listBoxTtsVoice.Items.Add("Tom");
            listBoxTtsVoice.Items.Add("Samantha");
            listBoxTtsVoice.SelectedIndex = 0;

            textBoxServerIp.Text = AppInfo.SpeechKitServer;
            textBoxServerPort.Text = AppInfo.SpeechKitPort.ToString();
            speechkitInitialize();

            App.CancelSpeechKit += new CancelSpeechKitEventHandler(App_CancelSpeechKit);
        }

        //~MainPage()
        //{
        //    textBoxServerPort.Visibility = Visibility.Visible;
        //    _speechKit.release();

        //    App.CancelSpeechKit -= new CancelSpeechKitEventHandler(App_CancelSpeechKit);
        //}

        void App_CancelSpeechKit()
        {
            Logger.info(this, "App_CancelSpeechKit()");
            if (_speechKit != null)
            {
                _speechKit.cancelCurrent();
                hidePopup();
            }
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.info(this, "PhoneApplicationPage_BackKeyPress()");
            if (_popup.IsOpen)
            {
                App_CancelSpeechKit();
                e.Cancel = true;
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }

        public void buttonDictate_Click(object sender, RoutedEventArgs e)
        {
            Logger.info(this, "buttonDictate_Click()");

            AppInfo.SpeechKitServer = textBoxServerIp.Text;

            if (speechkitInitialize() == false)
            {
                return;
            }

            textBoxResult.Text = string.Empty;
            dictationStart(RecognizerRecognizerType.Dictation);
        }

        private void buttonWebSearch_Click(object sender, RoutedEventArgs e)
        {
            Logger.info(this, "buttonWebsearch_Click()");

            AppInfo.SpeechKitServer = textBoxServerIp.Text;
            if (speechkitInitialize() == false)
            {
                return;
            }

            textBoxResult.Text = string.Empty;
            dictationStart(RecognizerRecognizerType.Search);
        }

        private void buttonTtstext_Click(object sender, RoutedEventArgs e)
        {
            Logger.info(this, "buttonTtstext_Click()");
            _ttsText = textBoxResult.Text;
            _ttsVoice = listBoxTtsVoice.SelectedItem as string;
            if (_ttsText.Trim().Length == 0)
            {
                return;
            }

            if (speechkitInitialize() == false)
            {
                return;
            }

            speechStart();
        }

        private bool speechkitInitialize()
        {
            try
            {
                AppInfo.SpeechKitServer = textBoxServerIp.Text;
                AppInfo.SpeechKitPort = Convert.ToInt32(textBoxServerPort.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("Port must be a number");

                return false;
            }

            try
            {
                _speechKit = SpeechKit.initialize(AppInfo.SpeechKitAppId, AppInfo.SpeechKitServer, AppInfo.SpeechKitPort, AppInfo.SpeechKitSsl, AppInfo.SpeechKitApplicationKey);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return false;
            }

            _beep = _speechKit.defineAudioPrompt("beep.wav");
            _speechKit.setDefaultRecognizerPrompts(_beep, null, null, null);
            _speechKit.connect();
            Thread.Sleep(10); // to guarantee the time to load prompt resource

            return true;
        }

        private void dictationStart(string type)
        {
            Logger.info(this, "dictationStart() type: " + type);
            Thread thread = new Thread(() =>
            {
                _recognizer = _speechKit.createRecognizer(type, RecognizerEndOfSpeechDetection.Long, _oemconfig.defaultLanguage(), this, _handler);
                _recognizer.start();
                showPopup("Please wait");
            });
            thread.Start();
        }

        void dictationStop(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content as string;

            Thread thread = new Thread(() =>
            {
                switch (content)
                {
                    case "Stop":
                        _recognizer.stopRecording();
                        showPopup("Processing Dictation");
                        break;
                    case "Cancel":
                        if (_recognizer != null)
                        {
                            _recognizer.cancel();
                        }
                        hidePopup();
                        break;
                    default:
                        break;
                }
            });
            thread.Start();
        }

        void speechStart()
        {
            Thread thread = new Thread(() =>
            {
                _vocalizer = _speechKit.createVocalizerWithLanguage(_oemconfig.defaultLanguage(), this, _handler);
                _vocalizer.setLanguage(_oemconfig.defaultLanguage());
                _vocalizer.setVoice(_ttsVoice);
                _vocalizer.speakString(_ttsText, null);
                showPopup("Processing TTS");
            });
            thread.Start();
        }

        void speechStop(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                if (_vocalizer != null)
                {
                    _vocalizer.cancel();
                }
                hidePopup();
            });
            thread.Start();
        }

        private void showPopup(string text)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // Create some content to show in the popup. Typically you would 
                // create a user control.
                double width = Application.Current.RootVisual.RenderSize.Width;
                double height = Application.Current.RootVisual.RenderSize.Height;
                enableScreen(false);
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(3);
                border.Width = width - 150;

                StackPanel panel = new StackPanel();
                panel.Background = new SolidColorBrush(Colors.DarkGray);
                TextBlock textblock = new TextBlock();
                textblock.Text = text;
                textblock.Foreground = new SolidColorBrush(Colors.White);
                textblock.Margin = new Thickness(3);
                textblock.FontSize = 20;
                textblock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                panel.Children.Add(textblock);

                SystemTray.ProgressIndicator.IsVisible = true;
                SystemTray.ProgressIndicator.IsIndeterminate = true;

                Button button = new Button();
                Button button1 = new Button();

                Grid grid = new Grid();
                grid.Background = new SolidColorBrush(Colors.DarkGray);


                switch (text)
                {
                    case "Please wait":
                    case "Processing.....":
                        button.Content = "Cancel";
                        button.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        button.Width = 160;
                        button.Click += new RoutedEventHandler(dictationStop);
                        panel.Children.Add(button);
                        break;
                    case "Processing TTS":
                    case "Speaking":
                        button.Content = "Cancel";
                        button.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        button.Width = 160;
                        button.Click += new RoutedEventHandler(speechStop);
                        panel.Children.Add(button);
                        break;
                    case "Listening":
                        button.Content = "Stop";
                        button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        button.Width = 160;
                        button.Click += new RoutedEventHandler(dictationStop);
                        button1.Content = "Cancel";
                        button1.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                        button1.Width = 160;
                        button1.Click += new RoutedEventHandler(dictationStop);
                        grid.Children.Add(button);
                        grid.Children.Add(button1);
                        panel.Children.Add(grid);
                        break;
                    default:
                        break;
                }

                border.Child = panel;

                // Set the Child property of Popup to the border 
                // which contains a stackpanel, textblock and button.
                _popup.Child = border;

                // Set where the popup will show up on the screen.
                _popup.VerticalOffset = 500;// (height - border.Height) / 2;
                _popup.HorizontalOffset = (width - border.Width) / 2;

                // Open the popup.
                _popup.IsOpen = true;
            });
        }

        private void hidePopup()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                SystemTray.ProgressIndicator.IsVisible = false;
                SystemTray.ProgressIndicator.IsIndeterminate = false;

                _popup.IsOpen = false;
                enableScreen(true);
            });
        }

        private void enableScreen(bool enable)
        {
            Color color;

            if (enable == true)
            {

                color = Colors.White;
            }
            else
            {
                color = Colors.Gray;
            }
            ApplicationTitle.Foreground = new SolidColorBrush(color);
            textBlockTtsVoice.Foreground = new SolidColorBrush(color);
            textBlockServerIp.Foreground = new SolidColorBrush(color);
            textBlockServerPort.Foreground = new SolidColorBrush(color);
            listBoxTtsVoice.Background = new SolidColorBrush(color);
            listBoxTtsVoice.BorderBrush = new SolidColorBrush(color);

            textBoxResult.IsEnabled = enable;
            textBoxServerIp.IsEnabled = enable;
            textBoxServerPort.IsEnabled = enable;
            buttonDictate.IsEnabled = enable;
            buttonWebSearch.IsEnabled = enable;
            buttonTtstext.IsEnabled = enable;
            listBoxTtsVoice.IsEnabled = enable;
        }

        public void onRecordingBegin(Recognizer recognizer)
        {
            Logger.info(this, "onRecordingBegin()");
            showPopup("Listening");
        }

        public void onRecordingDone(Recognizer recognizer)
        {
            Logger.info(this, "onRecordingDone()");
            //_recognizer.stopRecording();
            showPopup("Converting into text...");
        }

        public void onResults(Recognizer recognizer, Recognition results)
        {
            Logger.info(this, "onResults()");

            // for debugging purpose: logging the speechkit session id
            Logger.info(this, "session id: [" + _speechKit.getSessionId() + "]");

            hidePopup();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                textBoxResult.Text = results.getResult(0).getText();
                buttonDictate.Content = "Speak again ";
            });
            _recognizer.cancel();
            _recognizer = null;
        }

        public void onError(Recognizer recognizer, SpeechError error)
        {
            Logger.info(this, "onError()");

            // for debugging purpose: logging the speechkit session id
            Logger.info(this, "session id: [" + _speechKit.getSessionId() + "]");

            if (recognizer != _recognizer)
            {
                return;
            }
            hidePopup();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(error.getErrorDetail());
            });
            _recognizer.cancel();
            _recognizer = null;
        }

        public void onSpeakingBegin(Vocalizer vocalizer, string text, object context)
        {
            Logger.info(this, "onSpeakingBegin()");

            showPopup("Speaking");
        }

        public void onSpeakingDone(Vocalizer vocalizer, string text, SpeechError error, object context)
        {
            Logger.info(this, "onSpeakingDone()");

            // for debugging purpose: logging the speechkit session id
            Logger.info(this, "session id: [" + _speechKit.getSessionId() + "]");

            hidePopup();
            if (error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(error.getErrorDetail());
                });
            }
            _vocalizer.cancel();
        }

        public void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.speechtext = textBoxResult.Text;
            NavigationService.Navigate(new Uri("/SMSPage.xaml", UriKind.Relative));
            App.comingfromspeech = true;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            buttonDictate_Click(sender, e);
        }

    }
}