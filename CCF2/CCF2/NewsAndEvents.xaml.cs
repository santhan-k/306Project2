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
using HtmlAgilityPack;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Xml;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Controls;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class NewsAndEvents : Page
    {
        public SurfaceWindow1 sw3;

        public NewsAndEvents(SurfaceWindow1 window, String name)
        {
            sw3 = window;
            InitializeComponent();

            showNewsOrEvents("News", newsPanel);
            showNewsOrEvents("Events", eventsPanel);           

            /* As the user goes through the pages, the next page slides into focus from the right.
             * The current page slides to the left and disappears. Vice versa, as the user goes
             * back, the previous page slides into focus from the left and the current page slides
             * to the right and disappears.
             */
            if (name == "newsandevents")
            {
                sw3.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw3.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw3.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw3.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        //Reads the relevant xml file and loads panel with an image and caption for each item
        //The CCF logo is used as a placeholder if necessary.
        private void showNewsOrEvents(String content, StackPanel panel)
        {
            //read in news or events information
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/" + content + ".xml");

            foreach (XmlNode node in xml.SelectNodes("//" + content + "/item"))
            {
                String id = node.SelectSingleNode("id/text()").Value;
                String title = node.SelectSingleNode("title/text()").Value;
                XmlNode imageNode = node.SelectSingleNode("photos/img");
                Uri imgUri = new Uri("pack://application:,,,/Resources/images/Common/CCF-logo2_200.png", UriKind.Absolute);

                //use the image if there is one. Otherwise use the CCF logo.
                if (imageNode != null)
                {
                    imgUri = new Uri(Directory.GetCurrentDirectory() + "/" + imageNode.Attributes["src"].Value, UriKind.Absolute);
                }

                //put the image and caption in a button (using a canvas)
                BitmapImage bi = new BitmapImage(imgUri);
                SurfaceButton s = new SurfaceButton();
                Canvas c = new Canvas();

                //canvas height and width must be set :(
                c.Height = panel.Height - 10;
                c.Width = c.Height / bi.PixelHeight * bi.PixelWidth;
                c.Children.Add(new Image() { Source = bi, Height=c.Height });

                //label has white text on a semi-transparent dark background
                Label l = new Label() { Content = title, Width = c.Width };
                l.Foreground = new SolidColorBrush(Colors.White);
                l.Background = new SolidColorBrush(Colors.Black) { Opacity=0.5 };
                Canvas.SetBottom(l, 0);
                c.Children.Add(l);

                s.Name = id;
                s.Padding = new Thickness(0);
                s.Margin = new Thickness(4);
                s.Content = c;

                if (content == "News")
                {
                    s.Click += News_Click;
                }
                else if (content == "Events")
                {
                    s.Click += Events_Click;
                }
                panel.Children.Add(s);
            }
        }

        //Action listener for the back button
        // Touching the back button will take the user to the homepage
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, (sender as SurfaceButton).Name));
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new EventsPage(sw3, (sender as SurfaceButton).Name));
        }

        //Action listener for the logo
        // Touching the CCF logo will take the user to the homepage
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }

        private void SurfaceScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        /* The user can go back to the previous page by swiping their finger to the right.
         * The swipe gesture is made up of 3 phases:
         * Initial phase: TouchDown, the moment when the user touches the screen.
         * Middle phase: TouchMove, the speed and direction of the user as they move their finger across the screen.
         * Final phase: TouchUp, the moment when the user's finger leaves the screen.
         */
        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        // TouchDown event triggers the moment when the user touches the screen and captures the (x,y) position of the touch
        private void NewsAndEvents_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void NewsAndEvents_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        // TouchMove event triggers when the user's finger moves quickly across the screen
        private void NewsAndEvents_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw3.showPage(new HomePage(sw3));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}

