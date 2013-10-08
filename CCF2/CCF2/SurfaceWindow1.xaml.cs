using System;
using System.Threading.Tasks;
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
using System.Windows.Media.Animation;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : Window
    {
        public Stack<Page> pages;
        public Storyboard showP;
        public Storyboard hideP;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {

            InitializeComponent();
            pages = new Stack<Page>();
            display.Content = new HomePage(this);

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        //Code stolen from http://biederdb.googlecode.com/svn/trunk/PageTransitionsDemo/WpfPageTransitions/PageTransition.xaml.cs

        public void showPage(Page p)
        {
            pages.Push(p);
            Task.Factory.StartNew(() => newPage());
        }

        public void newPage()
        {
            Dispatcher.Invoke((Action)delegate
            {
                if (display.Content != null)
                {
                    Page oldPage = display.Content as Page;
                    if (oldPage != null)
                    {
                        oldPage.Loaded -= newPage_loaded;
                        UnloadPage(oldPage);
                    }
                }
                else
                {
                    ShowNextPage();
                }
            });
           
        }

        private void ShowNextPage()
        {
            Page newPage = pages.Pop();
            newPage.Loaded += newPage_loaded;
            display.Content = newPage;
        }

        private void UnloadPage(Page page)
        {
            hideP.Completed += hidePage_Completed;
            hideP.Begin(display);
        }

        private void newPage_loaded(object sender, RoutedEventArgs e)
        {
            showP.Begin(display);
        }

        private void hidePage_Completed(object sender, EventArgs e)
        {
            display.Content = null;
            ShowNextPage();
        }
        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
            
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }
    }
}