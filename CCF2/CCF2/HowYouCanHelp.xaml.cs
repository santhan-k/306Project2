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
    /// Interaction logic for HowYouCanHelp.xaml
    /// </summary>
    public partial class HowYouCanHelp : Page
    {
        
        public SurfaceWindow1 sw1;
        public HowYouCanHelp(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            /* As the user goes through the pages, the next page slides into focus from the right.
             * The current page slides to the left and disappears. Vice versa, as the user goes
             * back, the previous page slides into focus from the left and the current page slides
             * to the right and disappears.
             */
            if (name == "howyoucanhelp")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        // Touching the back button will take the user to the homepage
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        // Touching the BeadsofCourage® button will take the user to the Beads of Courage® page
        private void BeadsOfCourage_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "BeadsOfCourage"));
        }

        // Touching the Sponsors button will take the user to the Sponsors page
        private void Sponsors_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Sponsors"));
        }

        // Touching the Donate button will take the user to the Donate page
        private void Donate_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Donate"));
        }

        // Touching the Fundraise button will take the user to the Fundraise page
        private void Fundraise_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Fundraise"));
        }

        // Touching the MakeaGift button will take the user to the Make a Gift page 
        private void MakeAGift_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "MakeAGift"));
        }

        // Touching the OurCampaigns button will take the user to the Our Campaigns page
        private void OurCampaigns_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "OurCampaigns"));
        }

        // Touching the Volunteer button will take the user to the Volunteer page
        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Volunteer"));
        }

        // Touching the Schools button will take the user to the Schools page
        private void Schools_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Schools"));
        }

        // Touching the CCF logo will take the user to the homepage
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        /* The user can go back to the previous page by swiping their finger to the right.
         * The swipe gesture is made up of 3 phases:
         * Initial phase: TouchDown, the moment when the user touches the screen.
         * Middle phase: TouchMove, the speed and direction of the user as they move their finger across the screen.
         * Final phase: TouchUp, the moment when the user's finger leaves the screen.
         */
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        // TouchDown event triggers the moment when the user touches the screen and captures the (x,y) position of the touch
        private void HowYouCanHelp_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void HowYouCanHelp_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        // TouchMove event triggers when the user's finger moves quickly across the screen
        private void HowYouCanHelp_Touch_TouchMove(object sender, TouchEventArgs e)
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
