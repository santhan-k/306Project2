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
    /// Interaction logic for testNE.xaml
    /// </summary>
    public partial class testNE : Page
    {
        public SurfaceWindow1 sw1;
        public testNE(SurfaceWindow1 window, String name)
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

        }
        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
        //Action listener for the Our Ambassadors page
        private void Ambassadors_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsPage(sw1, "AssurityConsultingsupportOneDay"));
        }

        //Action Listerner for the HowWeHelp Button
        private void HowWeHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsPage(sw1, "Charitybeginsattheoffice"));

        }

        //Action Listener for the OurHistory button
        private void OurHistory_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsPage(sw1, "Governor-GeneralDinnerinHamiltongreatnightforall"));

        }

        //Action Listener Health Professionals
        private void HealthProfessionals_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsPage(sw1, "CRCSpeedshowauctionpaintingsforcharity"));

        }

        //Action Listener for the About Us Page
        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new EventsPage(sw1, "CharityHomeForCCE"));

        }

        //Action Listener for the Our Stories button
        private void OurStories_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new EventsPage(sw1, "CraftyKnitwitsKnitathonGrandAuction"));

        }

        //Action listener for the Our People button
        private void OurPeople_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new EventsPage(sw1, "ChildCancerLegendsLuncheon"));

        }

       

        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
    }
}
