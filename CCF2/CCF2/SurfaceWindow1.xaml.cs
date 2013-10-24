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
using HtmlAgilityPack;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Xml;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using CCF2.Models;
namespace CCF2
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : Window
    {
        public Stack<Page> pages;

        //Storyboards used to create animations between pages
        public Storyboard showP;
        public Storyboard hideP;
        public UserTweetsViewModel UserTweetsWidget { get; set; } 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            //Initialising the window
            InitializeComponent();

            //Creating a page stack
            pages = new Stack<Page>();

            //Retrieving tweets 
            UserTweetsWidget = new UserTweetsViewModel("ChildCancerNZ", 20);

            //Creating a drirectory for the News And events images if it does not exist
            if (!Directory.Exists("Resources/images/NewsAndEvents"))
            {
                Directory.CreateDirectory("Resources/images/NewsAndEvents");
            }
            //Retrieving News And Events content from the CCF website
            try
            {
                getNewsOrEvents("News");
                getNewsOrEvents("Events");
            }
                //Showing an error message if there is no internet connection
            catch
            {
                MessageBox.Show("Could not connect to the Child Cancer Foundation website.\n\nPreviously downloaded news and events will be shown.");
            }

            //Displaying the Welcome screen
            display.Content = new WelcomeScreen(this);

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }


        private void getNewsOrEvents(String content)
        {
            WebClient web = new WebClient();
            String ccfURL = "http://childcancer.org.nz";
            String url = ccfURL + "/News-and-events/" + content + ".aspx";
            String contentXPath = "//div[@id='contentPrimary']";
            String itemXPath = "//div[@class='item']";

            String imagesFolder = "Resources/images/NewsAndEvents/";
            String xmlPath = "Resources/xml/" + content + ".xml";
            int imageID = 0;
            int itemID = 0;

            HtmlDocument newsHTML = new HtmlWeb().Load(url);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(xmlPath, settings);
            writer.WriteStartElement(content);

            foreach (HtmlNode n in newsHTML.DocumentNode.SelectNodes(contentXPath + itemXPath))
            {
                String title = n.SelectSingleNode("h3/a").InnerText;
                String itemURL = ccfURL + n.SelectSingleNode("h3/a").Attributes["href"].Value;
                String details = n.SelectSingleNode("small").InnerText;
                String blurb = n.SelectSingleNode("p").InnerText;

                writer.WriteStartElement("item");
                writer.WriteElementString("id", content + itemID.ToString());
                writer.WriteElementString("title", title);
                writer.WriteElementString("details", details.Replace("<br>", "\n"));
                writer.WriteElementString("blurb", blurb);
                writer.WriteStartElement("photos");

                HtmlNode itemImage = n.SelectSingleNode("img");
                if (itemImage != null)
                {
                    String imgPath = imagesFolder + content + imageID.ToString() + ".jpg";
                    web.DownloadFile(new Uri(ccfURL + itemImage.Attributes["src"].Value), imgPath);
                    writer.WriteStartElement("img");
                    writer.WriteAttributeString("src", imgPath);
                    writer.WriteEndElement();
                    imageID++;
                }

                HtmlDocument itemHTML = new HtmlWeb().Load(itemURL);

                if (itemHTML.DocumentNode.SelectSingleNode(contentXPath + "//img") != null)
                {
                    foreach (HtmlNode img in itemHTML.DocumentNode.SelectNodes(contentXPath + "//img"))
                    {
                        String imgPath = imagesFolder + content + imageID.ToString() + ".jpg";
                        web.DownloadFile(new Uri(ccfURL + img.Attributes["src"].Value), imgPath);
                        writer.WriteStartElement("img");
                        writer.WriteAttributeString("src", imgPath);
                        writer.WriteEndElement();
                        imageID++;
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                itemID++;
            }
            writer.WriteEndElement();
            writer.Flush();
        }

        //This function is called by the other pages to indicate what page needs to be displayed
        public void showPage(Page p)
        {
            pages.Push(p);

            //Start a new task.   Doing it this way has performance benefits
            Task.Factory.StartNew(() => newPage());
        }

        //Functoin's responsibility is to replace the old page with the one that needs to be loaded
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

        //Finally loads the next page
        private void ShowNextPage()
        {
            Page newPage = pages.Pop();
            newPage.Loaded += newPage_loaded;
            display.Content = newPage;
        }

        //Unloads the page that needs to be removed
        private void UnloadPage(Page page)
        {
            hideP.Completed += hidePage_Completed;
            hideP.Begin(display);
        }

        //Display the new page
        private void newPage_loaded(object sender, RoutedEventArgs e)
        {
            showP.Begin(display);
        }

        //Hide page that has just been pulled out of the window
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