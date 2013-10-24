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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
namespace CCF2
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Page
    {
        public SurfaceWindow1 sw1;
        public WelcomeScreen(SurfaceWindow1 window)
        {
            sw1 = window;
            InitializeComponent();

            /* As the user goes through the pages, the next page slides into focus from the right.
             * The current page slides to the left and disappears. Vice versa, as the user goes
             * back, the previous page slides into focus from the left and the current page slides
             * to the right and disappears.
             */
            sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
            sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            //mainVideo.Source = new Uri("Resources/CCFVideo.mp4" + UriKind.RelativeOrAbsolute);
            //mainVideo.Play();
        }

        // Touching anywhere on the screen will take the user to the homepage
        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1, "s"));
        }

        // Restarts the video when it ends
        private void mainVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            mainVideo.Position = TimeSpan.Zero;
            
        }

        private void mainVideo_MediaOpened(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
