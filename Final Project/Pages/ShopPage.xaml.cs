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
    public sealed partial class ShopPage : Page
    {
        public ShopPage()
        {
            this.InitializeComponent();
        }

        private void ContinuePageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage2));
        }
        
        //spaceships
        private void Space_Ship1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Space_Ship2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Space_Ship3_Click(object sender, RoutedEventArgs e)
        {

        }

        //bullets
        private void Medium_Shell_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sniper_Bullet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Heavy_Bullet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Granade_Bullet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
