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

        /* As the user goes through the pages, the next page slides into focus from the right.
         * The current page slides to the left and disappears. 
         */ 
        public HomePage(SurfaceWindow1 window)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();

        }

        /* As the user goes back, the previous page slides into focus from the left and
         * the current page slides to the right and disappears.
         */
        public HomePage(SurfaceWindow1 window, string name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

        }

        // Touching the WhatWeDo icon will take the user to the WhatWeDo page
        private void WhatWeDo_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDo(sw1,"whatwedo"));
        }

        // Touching the HowYouCanHelp icon will take the user to the HowYouCanHelp page
        private void HowYouCanHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelp(sw1, "howyoucanhelp"));  
        }

        // Touching the NewsAndEvents icon will take the user to the NewsAndEvents page
        private void NewsAndEvents_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsAndEvents(sw1, "newsandevents"));
        }

        // Touching the FamilySupport icon will take the user to the FamilySupport page
        private void FamilySupport_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupport(sw1, "familysupport"));
        }

        // Touching the Volunteer icon will take the user to the Volunteer page
        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new VolunteerPage(sw1, "volunteer"));
        }

        // Touching the CCF logo will take the user to the welcome splash screen
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WelcomeScreen(sw1));
        }

        // Touching the ContactUs icon will take the user to the ContactUs page
        private void ContactUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new ContactUsPage(sw1, "testNE"));
        }

        /* The user can go back to the previous page by swiping their finger to the right.
         * The swipe gesture is made up of 3 phases:
         * Initial phase: TouchDown, the moment when the user touches the screen.
         * Middle phase: TouchMove, the speed and direction of the user as they move their finger across the screen.
         * Final phase: TouchUp, the moment when the user's finger leaves the screen.
         */
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        // TouchDown event triggers the moment when the user touches the screen and captures the (x,y) position of the touch
        private void Home_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void Home_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }
        
        // TouchMove event triggers when the user's finger moves quickly across the screen
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
