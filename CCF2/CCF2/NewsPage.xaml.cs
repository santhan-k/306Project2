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
using System.Xml;
using Microsoft.Surface.Presentation.Input;
using System.IO;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        public SurfaceWindow1 sw1;
        public NewsPage(SurfaceWindow1 window, String itemID)
        {
            sw1 = window;
            InitializeComponent();

            /* As the user goes through the pages, the next page slides into focus from the right.
             * The current page slides to the left and disappears. Vice versa, as the user goes
             * back, the previous page slides into focus from the left and the current page slides
             * to the right and disappears.
             */
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

            //read the xml file
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/News.xml");

            //find the news article
            //display the title, date (details), blurb and photos
            foreach (XmlNode node in xml.SelectNodes("//News/item"))
            {
                String id = node.SelectSingleNode("id/text()").Value;
                if (id == itemID)
                {
                    XmlNode imageNode = node.SelectSingleNode("photos/img");
                    headingLabel.Content = node.SelectSingleNode("title/text()").Value;
                    detailsText.Text = node.SelectSingleNode("details/text()").Value;
                    bodyText.Text = node.SelectSingleNode("blurb/text()").InnerText.Trim();

                    if (imageNode != null)
                    {
                        //ignore the first image because it is from the news page, and often an identical one exists in the article page.
                        //thus this prevents double-ups
                        foreach (XmlNode n in imageNode.SelectNodes("following-sibling::img"))
                        {
                            Uri imgUri = new Uri(Directory.GetCurrentDirectory() + "/" + n.Attributes["src"].Value, UriKind.Absolute);
                            imagesPanel.Children.Add(new Image() { Source = new BitmapImage(imgUri), Margin=new Thickness(4), Height=250, Stretch=Stretch.UniformToFill });
                        }
                    }
                }
            }
        }

        // Touching the back button will take the user to the News And Events page
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new NewsAndEvents(sw1, "back"));
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
        private void News_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void News_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        // TouchMove event triggers when the user's finger moves quickly across the screen
        private void News_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw1.showPage(new NewsAndEvents(sw1, "back"));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
