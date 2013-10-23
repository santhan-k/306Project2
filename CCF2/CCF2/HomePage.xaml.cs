using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Surface.Presentation.Input;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    /// 
    public partial class HomePage : Page
    {
        public SurfaceWindow1 sw1;
        public HomePage(SurfaceWindow1 window)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();

        }

        public HomePage(SurfaceWindow1 window, string name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

        }

        //Action listener for the event that is caused when the What We Do menu button is clicked
        private void WhatWeDo_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDo(sw1,"whatwedo"));
        }

        //Action listener for the event that is caused when the How You Can Help menu button is clicked
        private void HowYouCanHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelp(sw1, "howyoucanhelp"));  
        }

        //Action listener for the event that is caused when the News And Events menu button is clicked
        private void NewsAndEvents_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsAndEvents(sw1, "newsandevents"));
        }

        //Action listener for the event that is caused when the Family Support menu button is clicked
        private void FamilySupport_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupport(sw1, "familysupport"));
        }

        //Action listener for the event that is caused when the Volunteer menu button is clicked
        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new VolunteerPage(sw1, "volunteer"));
        }
        //Action listener for the event that is caused when the logo is clicked
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WelcomeScreen(sw1));
        }

        //Action listener for the event that is caused when the Contact Us menu button is clicked
        private void ContactUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new ContactUsPage(sw1, "testNE"));
        }

        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void Home_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void Home_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void Home_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw1.showPage(new WelcomeScreen(sw1));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
