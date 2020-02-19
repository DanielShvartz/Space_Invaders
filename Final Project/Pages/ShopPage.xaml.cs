using Final_Project.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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

        protected override void OnNavigatedTo(NavigationEventArgs e) // load any button/data about player
        {
            data = (Data)e.Parameter;
                Debug.Write(data);
            if (data == null) // if he doesnt have any info on him - block
            {
                Space_Ship1.IsEnabled = false;
                Space_Ship2.IsEnabled = false;
                Space_Ship3.IsEnabled = false;
                Medium_Shell.IsEnabled = false;
                Heavy_Bullet.IsEnabled = false;
                Sniper_Bullet.IsEnabled = false;
                Granade_Bullet.IsEnabled = false;
                return;
            }

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

            Coin_Text.Text = "Coins:" + data.coins;

        }

        public ShopPage()
        {
            this.InitializeComponent();
        }

        private void ContinuePageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage2), data);
        }
        
        async System.Threading.Tasks.Task PreviewErrorMSGAsync()
        {
            var dialog = new MessageDialog("You dont have enough coins to buy this item.");
            dialog.Title = "Not enough coins!";

            dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
            var ans = await dialog.ShowAsync();
        }
        //spaceships
        private void Space_Ship1_Click(object sender, RoutedEventArgs e)
        {
            if (data.coins >= 500) // check if the player can buy this item
            {
                data.coins -= 500; // if so we decrease his coins and set the spaceship - to set in the game page
                data.player_SpaceShip_Level = 1;
                Space_Ship1.IsEnabled = false;
            }
            else
            {
                PreviewErrorMSGAsync();
            }

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
