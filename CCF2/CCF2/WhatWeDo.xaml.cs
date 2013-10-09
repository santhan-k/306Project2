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

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class WhatWeDo : Page
    {
        public SurfaceWindow1 sw1;
        public WhatWeDo(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();
            if (name == "whatwedo")
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        private void Ambassadors_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurAmbassadors"));
        }

        private void HowWeHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "HowWeHelp"));

        }

        private void OurHistory_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurHistory"));

        }
        private void HealthProfessionals_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "HealthProfessionals"));

        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "AboutUs"));

        }

        private void OurStories_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurStories"));

        }

        private void OurPeople_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurPeople"));

        }

        private void Vacancies_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "Vacancies"));

        }
    }
}
