﻿using System;
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
    public partial class SubMenuPage : Page
    {
        public SurfaceWindow1 sw1;
        public SubMenuPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }


        private void HowYouCanHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewsAndEvents_Click(object sender, RoutedEventArgs e)
        {

        }


        private void FamilySupport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Volunteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContactUs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
