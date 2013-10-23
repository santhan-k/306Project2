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
using System.Xml;
using Microsoft.Surface.Presentation.Input;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDoSubPage.xaml
    /// </summary>
    public partial class FamilySupportSubPage : Page
    {
        public SurfaceWindow1 sw1;
        public FamilySupportSubPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/FamilySupportInfo.xml");
            
            headingLabel.Content = xml.SelectSingleNode("//pages/"+name+"/heading/text()").Value;
            XmlNode imageNode = xml.SelectSingleNode("//pages/" + name + "/img");
            if (imageNode != null)
            {
                bodyImage.Source = new BitmapImage(new Uri("/CCF2;component/" + imageNode.Attributes["src"].Value, UriKind.Relative));
                bodyImage.Visibility = System.Windows.Visibility.Visible;
                bodyText.Width = 740;
            }
            bodyText.Text = xml.SelectSingleNode("//pages/" + name + "/content").InnerText.Trim();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupport(sw1, "back"));
        }

        private void bodyImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // insert code here
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void FamilySupportSubPage_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void FamilySupportSubPage_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void FamilySupportSubPage_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw1.showPage(new FamilySupport(sw1, "back"));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
