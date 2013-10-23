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
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class OurStories : Page
    {
        public SurfaceWindow1 sw1;
        public OurStories(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "ourstories")
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

        //Action listener for the DequarnHarrison Button
        private void DequarnHarrison_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new OurStoriesSubPage(sw1, "DequarnHarrison"));
        }

        //Action Listerner for the ClaudiaLittle Button
        private void ClaudiaLittle_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new OurStoriesSubPage(sw1, "ClaudiaLittle"));

        }

        //Action Listener for the JoyceSingh button
        private void JoyceSingh_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new OurStoriesSubPage(sw1, "JoyceSingh"));

        }
        
        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void OurStories_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void OurStories_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void OurStories_Touch_TouchMove(object sender, TouchEventArgs e)
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