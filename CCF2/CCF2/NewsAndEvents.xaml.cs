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
using HtmlAgilityPack;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Xml;
using Microsoft.Surface.Presentation.Input;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class NewsAndEvents : Page
    {
        public SurfaceWindow1 sw3;
        public NewsAndEvents(SurfaceWindow1 window, String name)
        {
            sw3 = window;
            InitializeComponent();

            showNewsOrEvents("News", newsPanel);
            showNewsOrEvents("Events", eventsPanel);
            
            //if (!Directory.Exists("Resources/images/NewsAndEvents"))
            //{
            //    Directory.CreateDirectory("Resources/images/NewsAndEvents");
            //}

            //try
            //{
            //    getNewsOrEvents("News");
            //    getNewsOrEvents("Events");
            //}
            //catch
            //{
            //    MessageBox.Show("Could not connect to the Child Cancer Foundation website.\n\nPreviously downloaded news and events will be shown.");
            //}

            //If new page is initiated from the home page, it comes in the form the right
            if (name == "newsandevents")
            {
                sw3.hideP = (window.Resources["SlidePageLeftExit"] as Storyboard).Clone();
                sw3.showP = (window.Resources["SlidePageLeftEntry"] as Storyboard).Clone();
            }
            else  // else it comes in the from the left
            {
                sw3.hideP = (window.Resources["SlidePageRightExit"] as Storyboard).Clone();
                sw3.showP = (window.Resources["SlidePageRightEntry"] as Storyboard).Clone();
            }

        }

        private void showNewsOrEvents(String content, StackPanel panel)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/" + content + ".xml");

            foreach (XmlNode node in xml.SelectNodes("//" + content + "/item"))
            {
                XmlNode imageNode = node.SelectSingleNode("photos/img");
                Uri imgUri = new Uri("Resources/images/Common/CCF-logo2_200.png", UriKind.Relative);

                if (imageNode != null)
                {
                    imgUri = new Uri(Directory.GetCurrentDirectory() + "/" + imageNode.Attributes["src"].Value, UriKind.Absolute);
                }
                panel.Children.Add(new Image() { Source = new BitmapImage(imgUri), Margin=new Thickness(10) });
            }
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
            XmlWriter writer = XmlWriter.Create(xmlPath,settings);
            writer.WriteStartElement(content);

            foreach (HtmlNode n in newsHTML.DocumentNode.SelectNodes(contentXPath + itemXPath))
            {
                String title = n.SelectSingleNode("h3/a").InnerText;
                String itemURL = ccfURL + n.SelectSingleNode("h3/a").Attributes["href"].Value;
                String details = n.SelectSingleNode("small").InnerText;
                String blurb = n.SelectSingleNode("p").InnerText;

                writer.WriteStartElement("item");
                writer.WriteElementString("id", itemID.ToString());
                writer.WriteElementString("title", title);
                writer.WriteElementString("details", details.Replace("<br>","\n"));
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

        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }

        //Action listener for the CharityHomeForCCE button
        private void CharityHomeForCCE_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new EventsPage(sw3, "CharityHomeForCCE"));
        }

        //Action Listerner for the CraftyKnitwitsKnitathonGrandAuction Button
        private void CraftyKnitwitsKnitathonGrandAuction_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new EventsPage(sw3, "CraftyKnitwitsKnitathonGrandAuction"));

        }

        //Action Listener for the Child Cancer Legends Luncheon button
        private void ChildCancerLegendsLuncheon_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new EventsPage(sw3, "ChildCancerLegendsLuncheon"));

        }

        //Action Listener Assurity Consulting support One Day button
        private void AssurityConsultingsupportOneDays_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, "AssurityConsultingsupportOneDay"));

        }

        //Action Listener for the Charitybeginsattheoffice Page
        private void Charitybeginsattheoffice_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, "Charitybeginsattheoffice"));

        }

        //Action Listener for the Governor-GeneralDinnerinHamiltongreatnightforall button
        private void GovernorGeneralDinnerinHamiltongreatnightforall_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, "Governor-GeneralDinnerinHamiltongreatnightforall"));

        }

        //Action listener for the CRCSpeedshowauctionpaintingsforcharity button
        private void CRCSpeedshowauctionpaintingsforcharity_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, "CRCSpeedshowauctionpaintingsforcharity"));

        }

        //Action listener for the logo
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }

        private void SurfaceScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private Dictionary<TouchDevice, Point> currentTouchDevices = new Dictionary<TouchDevice, Point>();

        private void NewsAndEvents_Touch_TouchDown(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Add(e.TouchDevice, e.TouchDevice.GetPosition(this));
        }

        private void NewsAndEvents_Touch_TouchUp(object sender, TouchEventArgs e)
        {
            currentTouchDevices.Remove(e.TouchDevice);
        }

        private void NewsAndEvents_Touch_TouchMove(object sender, TouchEventArgs e)
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
                    sw3.showPage(new HomePage(sw3));
                    currentTouchDevices.Clear();
                    return;
                }
            }
        }
    }
}
