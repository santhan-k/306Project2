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
    /// Interaction logic for OurStoriesSubPage.xaml
    /// </summary>
    public partial class OurStoriesSubPage : Page
    {
        public SurfaceWindow1 sw1;

        /* As the user goes through the pages, the next page slides into focus from the right.
         * The current page slides to the left and disappears. Vice versa, as the user goes
         * back, the previous page slides into focus from the left and the current page slides
         * to the right and disappears.
         */
        public OurStoriesSubPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

            //read the xml file
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/OurStoriesInfo.xml");

            headingLabel.Content = xml.SelectSingleNode("//pages/" + name + "/heading/text()").Value;
            XmlNode imageNode = xml.SelectSingleNode("//pages/" + name + "/img");

            //show the image if there is one, and reduce the width of the TextBlock to accomodate it
            if (imageNode != null)
            {
                bodyImage.Source = new BitmapImage(new Uri("/CCF2;component/" + imageNode.Attributes["src"].Value, UriKind.Relative));
                bodyImage.Visibility = System.Windows.Visibility.Visible;
                bodyText.Width = 740;
            }
            bodyText.Text = xml.SelectSingleNode("//pages/" + name + "/content").InnerText.Trim();
        }

        // Touching the back button will take the user to the Our Stories page
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new OurStories(sw1, "back"));
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
        private void OurStoriesSub_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        // TouchUp event triggers the moment when the user's finger leaves the screen
        private void OurStoriesSub_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        // TouchMove event triggers when the user's finger moves quickly across the screen
        private void OurStoriesSub_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw1.showPage(new OurStories(sw1, "back"));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
