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
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class WhatWeDo : Page
    {
        public SurfaceWindow1 sw1;
        public WhatWeDo(SurfaceWindow1 window, String name)
        {
            sw1 = window;
            InitializeComponent();

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "whatwedo")
            {
                sw1.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw1.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw1.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        //Action listener for the Our Ambassadors page
        private void Ambassadors_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurAmbassadors"));
        }

        //Action Listerner for the HowWeHelp Button
        private void HowWeHelp_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "HowWeHelp"));

        }

        //Action Listener for the OurHistory button
        private void OurHistory_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurHistory"));

        }

        //Action Listener Health Professionals
        private void HealthProfessionals_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "HealthProfessionals"));

        }

        //Action Listener for the About Us Page
        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "AboutUs"));

        }

        //Action Listener for the Our Stories button
        private void OurStories_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurStories"));

        }

        //Action listener for the Our People button
        private void OurPeople_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "OurPeople"));

        }

        //Action listener for the Vacancies button
        private void Vacancies_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new WhatWeDoSubPage(sw1, "Vacancies"));

        }
        
        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw1.showPage(new HomePage(sw1));
        }

        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void WhatWeDo_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void WhatWeDo_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void WhatWeDo_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw1.showPage(new HomePage(sw1));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
