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

        private void WhatWeDo_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDo(sw1,"whatwedo"));
        }

        private void HowYouCanHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HowYouCanHelp(sw1, "howyoucanhelp"));  
        }

        private void NewsAndEvents_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsAndEvents(sw1, "newsandevents"));

        }

        private void FamilySupport_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupport(sw1, "familysupport"));
        }

        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContactUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new testNE(sw1, "testNE"));
        }
    }
}
