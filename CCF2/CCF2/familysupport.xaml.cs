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
    /// Interaction logic for FamilySupport.xaml
    /// </summary>
    public partial class FamilySupport : Page
    {
        public SurfaceWindow1 sw1;
        public FamilySupport(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            /* As the user goes through the pages, the next page slides into focus from the right.
             * The current page slides to the left and disappears. Vice versa, as the user goes
             * back, the previous page slides into focus from the left and the current page slides
             * to the right and disappears.
             */
            if (name == "familysupport")
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

        // Touching the SupportCo-ordinators button will take the user to the Support Co-ordinators page
        private void FSC_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "FSC"));
        }

        // Touching the BeadsofCourage® button will take the user to the Beads of Courage® page
        private void BeadsOfCourage_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "BeadsOfCourage"));
        }

        // Touching the SharingMagazine button will take the user to the Sharing Magazine page
        private void SharingMagazine_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "SharingMagazine"));
        }

        // Touching the FamilyPlaces button will take the user to the Family Places page
        private void FamilyPlaces_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "FamilyPlaces"));
        }

        // Touching the HolidayHomes button will take the user to the Holiday Homes page
        private void HolidayHomes_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "HolidayHomes"));
        }

        // Touching the Scholarships button will take the user to the Scholarships page
        private void Scholarships_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "Scholarships"));
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
        private void FamilySupport_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void FamilySupport_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        // TouchMove event triggers when the user's finger moves quickly across the screen
        private void FamilySupport_Touch_TouchMove(object sender, TouchEventArgs e)
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
