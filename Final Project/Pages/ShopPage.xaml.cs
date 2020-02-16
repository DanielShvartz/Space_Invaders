using Final_Project.Classes;
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
        Data data;
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (Data)e.Parameter;
            if (data == null)
                return;

            if (data.player_Bullet >= Bullets.Heavy_Shell) // depands on past chooses
                Medium_Shell.IsEnabled = false;
            if (data.player_Bullet >= Bullets.Granade_Shell) // if the player has a specific bullet or spaceship
                Heavy_Bullet.IsEnabled = false;
            if (data.player_Bullet >= Bullets.Sniper_Shell) // we prevent him for buying weaker bullet
                Granade_Bullet.IsEnabled = false;

            if (data.player_SpaceShip_Level >= (int)SpaceShips.Level2)
                Space_Ship1.IsEnabled = false;
            if (data.player_SpaceShip_Level >= (int)SpaceShips.Level3)
                Space_Ship2.IsEnabled = false;

        }

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
