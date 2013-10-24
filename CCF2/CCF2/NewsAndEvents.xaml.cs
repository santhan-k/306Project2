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
using Microsoft.Surface.Presentation.Controls;

namespace CCF2
{
    /// <summary>
    /// Interaction logic for WhatWeDo.xaml
    /// </summary>
    public partial class NewsAndEvents : Page
    {
        public SurfaceWindow1 sw3;
        public ICommand NewsCommand { get; set; }


        public NewsAndEvents(SurfaceWindow1 window, String name)
        {
            sw3 = window;
            InitializeComponent();

            showNewsOrEvents("News", newsPanel);
            showNewsOrEvents("Events", eventsPanel);
            this.NewsCommand = new TestCommand(ExecuteCommand1, CanExecuteCommand1);
            

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

        public bool CanExecuteCommand1(object parameter)
        {
            return true;
        }

        public void ExecuteCommand1(object parameter)
        {
            MessageBox.Show("Executing command 1");
        }

        public bool CanExecuteCommand2(object parameter)
        {
            return true;
        }

        public void ExecuteCommand2(object parameter)
        {
            MessageBox.Show("Executing command 2");
        }


        private void showNewsOrEvents(String content, StackPanel panel)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("Resources/xml/" + content + ".xml");

            foreach (XmlNode node in xml.SelectNodes("//" + content + "/item"))
            {
                String id = node.SelectSingleNode("id/text()").Value;
                XmlNode imageNode = node.SelectSingleNode("photos/img");
                Uri imgUri = new Uri("Resources/images/Common/CCF-logo2_200.png", UriKind.Relative);

                if (imageNode != null)
                {
                    imgUri = new Uri(Directory.GetCurrentDirectory() + "/" + imageNode.Attributes["src"].Value, UriKind.Absolute);
                }
                SurfaceButton s = new SurfaceButton();
                s.Name = content + id;
                s.Padding = new Thickness(0);
                s.Content = new Image() { Source = new BitmapImage(imgUri) };
                
                if (content == "News")
                {
                    s.Click += News_Click;
                }
                else if(content == "Events")
                {
                    s.Click += Events_Click;
                }
                panel.Children.Add(s);
            }
        }

        

        //Action listener for the back button
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new NewsPage(sw3, (sender as SurfaceButton).Name));
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new EventsPage(sw3, (sender as SurfaceButton).Name));
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

        private void NewsPanel_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            MessageBox.Show(sender.ToString());
            MessageBox.Show(e.Source.ToString());
            MessageBox.Show(e.OriginalSource.ToString());
        }

        private void NewsButton_Click(object sender, RoutedEventArgs e)
        {
            sw3.showPage(new HomePage(sw3));
        }
    }
    public class TestCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);
        public delegate bool ICommandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public TestCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        #endregion
    }

}

