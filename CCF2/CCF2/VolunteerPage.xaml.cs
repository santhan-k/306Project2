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
using CCF2.Models;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for VolunteerPage.xaml
    /// </summary>
    public partial class VolunteerPage : Page
    {
        public SurfaceWindow1 sw1;
        public VolunteerPage(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "volunteer")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }
            this.DataContext = this; 
        }

        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
    }
}
