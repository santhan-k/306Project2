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
    /// Interaction logic for FamilySupport.xaml
    /// </summary>
    public partial class FamilySupport : Page
    {
        public SurfaceWindow1 sw1;
        public FamilySupport(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            if (name == "familysupport")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        //Back button listener
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
           
        }


        private void FSC_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "FSC"));
        }

        private void BeadsOfCourage_Click(object sender, RoutedEventArgs e)
        {
            // BeadsOfCourage.Background = new RadialGradientBrush(Colors.Green, Colors.Black);
            sw1.showPage(new FamilySupportSubPage(sw1, "BeadsOfCourage"));
        }

        private void SharingMagazine_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "SharingMagazine"));
        }

        private void FamilyPlaces_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "FamilyPlaces"));
        }

        private void HolidayHomes_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "HolidayHomes"));
        }

        private void Scholarships_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new FamilySupportSubPage(sw1, "Scholarships"));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }
    }
}
