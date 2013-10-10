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

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDoSubPage.xaml
    /// </summary>
    public partial class WhatWeDoSubPage : Page
    {
        public SurfaceWindow1 sw1;
        public WhatWeDoSubPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            //Removing the old page and showing the new one 
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();

            //Loading content from XML file
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/WhatWeDoInfo.xml");

            headingLabel.Content = xml.SelectSingleNode("//pages/" + name + "/heading/text()").Value;
            XmlNode imageNode = xml.SelectSingleNode("//pages/" + name + "/img");
            if (imageNode != null)
            {
                //Loading image from the image URI Found in the XML file
                bodyImage.Source = new BitmapImage(new Uri("/CCF2;component/" + imageNode.Attributes["src"].Value, UriKind.Relative));
                bodyImage.Visibility = System.Windows.Visibility.Visible;
                //bodyText.Width = 740;
            }
            bodyText.Text = xml.SelectSingleNode("//pages/" + name + "/content").InnerText.Trim();

        }

        //Action listener for The back click button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDo(sw1,"back"));
        }

        //Action listener for the event that is caused when the logo is clicked
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
    }
}
