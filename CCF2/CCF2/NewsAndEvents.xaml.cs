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
            String ccfURL = "http://childcancer.org.nz";
            String newsURL = ccfURL + "/News-and-events/News.aspx";
            String eventsURL = ccfURL + "/News-and-events/Events.aspx";
            String imagesFolder = "Resources/images/";
            String newsXMLPath = "Resources/xml/news.xml";
            String newsDivXPath = "//div[@id='contentPrimary']";
            String newsItemXPath = "//div[@class='item']";
            WebClient web = new WebClient();
            int i = 0;
            try
            {
                HtmlDocument newsHTML = new HtmlWeb().Load(newsURL);
                XmlWriter writer = XmlWriter.Create(newsXMLPath);
                writer.WriteStartElement("news");
                foreach (HtmlNode n in newsHTML.DocumentNode.SelectNodes(newsDivXPath + newsItemXPath))
                {
                    String title = n.SelectSingleNode("h3/a").InnerText;
                    String articleURL = ccfURL + n.SelectSingleNode("h3/a").Attributes["href"].Value;
                    String date = n.SelectSingleNode("small").InnerText;
                    String blurb = n.SelectSingleNode("p").InnerText;

                    writer.WriteStartElement("item");
                    writer.WriteElementString("title", title);
                    writer.WriteElementString("date", date);
                    writer.WriteElementString("blurb", blurb);
                    writer.WriteStartElement("photos");

                    HtmlNode articleImage = n.SelectSingleNode("img");
                    if (articleImage != null)
                    {
                        String imgPath = imagesFolder + i.ToString() + ".jpg";
                        web.DownloadFile(new Uri(ccfURL + articleImage.Attributes["src"].Value), imgPath);
                        writer.WriteStartElement("img");
                        writer.WriteAttributeString("src", imgPath);
                        writer.WriteEndElement();
                        i++;
                    }

                    HtmlDocument articleHTML = new HtmlWeb().Load(articleURL);

                    if (articleHTML.DocumentNode.SelectSingleNode(newsDivXPath + "//img") != null)
                    {
                        foreach (HtmlNode img in articleHTML.DocumentNode.SelectNodes(newsDivXPath + "//img"))
                        {
                            String imgPath = imagesFolder + i.ToString() + ".jpg";
                            web.DownloadFile(new Uri(ccfURL + img.Attributes["src"].Value), imgPath);
                            writer.WriteStartElement("img");
                            writer.WriteAttributeString("src", imgPath);
                            writer.WriteEndElement();
                            i++;
                        }
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            catch
            {
                MessageBox.Show("Could not connect to the Child Cancer Foundation website.\n\nPreviously downloaded news and events will be shown.");
            }

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

        private String filenameFromPath(String path)
        {
            return path.Substring(path.LastIndexOf('/') + 1);
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

    }
}
