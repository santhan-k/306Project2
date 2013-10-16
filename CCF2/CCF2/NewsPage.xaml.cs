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
    /// Interaction logic for NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        public SurfaceWindow1 sw1;
        public NewsPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            //Loading content from XML file
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/NewsInfo.xml");
            XmlNode imageNode = xml.SelectSingleNode("//pages/" + name + "/img");
            if (imageNode != null)
            {
                //Loading image from the image URI Found in the XML file
                mainImage.Source = new BitmapImage(new Uri("/CCF2;component/" + imageNode.Attributes["src"].Value, UriKind.Relative));
                mainImage.Visibility = System.Windows.Visibility.Visible;
                //bodyText.Width = 740;
            }

            titleText1.Content = xml.SelectSingleNode("//pages/" + name + "/heading/text()").Value;


            MainBodyText.Text = xml.SelectSingleNode("//pages/" + name + "/content").InnerText.Trim();
            
        }

        private void SurfaceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Action listener for The back click button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new testNE(sw1, "back"));
        }

        //Action listener for the event that is caused when the logo is clicked
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
    }
}
