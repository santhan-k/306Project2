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

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "howyoucanhelp")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        //Action listener for the Our BeadsofCourage page
        private void BeadsOfCourage_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "BeadsOfCourage"));
        }

        //Action listener for the Our Sponsors page
        private void Sponsors_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Sponsors"));
        }
        //Action listener for the Our Donate page
        private void Donate_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Donate"));
        }
        //Action listener for the Our Fundraise page
        private void Fundraise_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Fundraise"));
        }

        //Action listener for the Our MakeAGift page
        private void MakeAGift_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "MakeAGift"));
        }

        //Action listener for the Our Our Campaigns page
        private void OurCampaigns_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "OurCampaigns"));
        }
        //Action listener for the Our Volunteer page
        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Volunteer"));
        }
        //Action listener for the Our Schools page
        private void Schools_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelpSubPage(sw1, "Schools"));
        }

        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

    }
}
