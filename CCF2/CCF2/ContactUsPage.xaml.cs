using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CCF2.Models;
using Microsoft.Surface.Presentation.Input;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for ContactUsPage.xaml
    /// </summary>
    public partial class ContactUsPage : Page
    {
        public ObservableCollection<TweetModel> Tweets { get; set; } 
        public UserTweetsViewModel UserTweetsWidget { get; set; } 
        public SurfaceWindow1 sw1;
        public ContactUsPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "testNE")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

            XDocument doc = XDocument.Load("Resources/xml/Tweets.xml");
            var tweetmes = doc.Element("Tweets").Elements("Tweet");
            
            ObservableCollection<TweetModel> result = new ObservableCollection<TweetModel>();
            foreach (XElement message in tweetmes)
            {
                TweetModel tweet = new TweetModel()
                {
                    Text = message.Attribute("Text").Value,
                    
                    ScreenName = message.Attribute("ScreenName").Value,
                    UserName = "@" + message.Attribute("UserName").Value,
                    PublicationDate = message.Attribute("PublicationDate").Value,
                    Image = message.Attribute("Image").Value
                    


                };
                
                result.Add(tweet);
            }
            
            this.Tweets = result;
            this.tweetsListView.DataContext = this;
            this.DataContext = this;
            
            //UserTweetsWidget = new UserTweetsViewModel("ChildCancerNZ", 20);
            //this.DataContext = this; 

        }
        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
        
        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void ContactUs_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void ContactUs_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void ContactUs_Touch_TouchMove(object sender, TouchEventArgs e)
        {
            if (currentTouchDevices.Count == 1)
            {
                int isRight = 0;

                foreach (KeyValuePair<TouchDevice, Point> td in currentTouchDevices)
                {
                    if (td.Key != null && e.TouchDevice.GetPosition(this).X - td.Value.X > 100)
                        isRight++;
                    else
                        return;
                }

                if (isRight == 1)
                {
                    sw1.showPage(new HomePage(sw1));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
