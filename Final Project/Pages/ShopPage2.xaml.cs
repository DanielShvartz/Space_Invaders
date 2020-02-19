﻿using Final_Project.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Final_Project.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopPage2 : Page
    {
        Data data;
        public ShopPage2()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (Data)e.Parameter;
        }

        private void Hp50Upgrade_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hp100Upgrade_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShieldUpgrade_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage));
        }
    }
}
