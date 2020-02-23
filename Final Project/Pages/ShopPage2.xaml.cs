using Final_Project.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ShopPage2 : Page
    {
        enum ItemType
        {
            HealthUpgrade50, HealthUpgrade100, ShieldUpgradeNew, ShieldUpgradeRefresh
        };
        Data data;
        public ShopPage2()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (Data)e.Parameter;
            Debug.WriteLine(data.ToString());
            if(data == null)
            {
                Hp50Upgrade_Button.IsEnabled = false;
                ShieldUpgrade_Button.IsEnabled = false;
                Hp100Upgrade_Button.IsEnabled = false;
                ShieldHeal_Button.IsEnabled = false;
            }
            if (data.ShieldHp.Count() == 3)
                ShieldUpgrade_Button.IsEnabled = false;

            data.coins += 1500;
            Coin_Text.Text = "Coins:" + data.coins;
        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage), data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coinsNeeded"> The amount of coins needed to buy the item</param>
        /// <param name="itemType"> To know if the item is a spaceship or bullet</param>
        /// <param name="buttonChosen"> To disable the button</param>
        void BuyItem(int coinsNeeded, ItemType itemType, Button buttonChosen)
        {
            if (data.coins >= coinsNeeded) // check if the player can buy this item
            {
                data.coins -= coinsNeeded; // if so we decrease his coins and set the item - to set in the game page
                Coin_Text.Text = "Coins:" + data.coins;
                if (itemType == ItemType.HealthUpgrade50) // we set the wanted item by its id
                    data.player_HitPoints += 50;
                else if (itemType == ItemType.HealthUpgrade100)
                    data.player_HitPoints += 100;
                else if (itemType == ItemType.ShieldUpgradeNew)
                {
                    //create new shield
                }
                else if (itemType == ItemType.ShieldUpgradeRefresh)
                {
                    //refresh shields
                }
                buttonChosen.IsEnabled = false; // we disable the button
            }
            else
            {
                PreviewErrorMSGAsync(); // if he doesnt have enough coins for the item we preview a message
            }
        }

        private void Hp50Upgrade_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(50, ItemType.HealthUpgrade50, Hp50Upgrade_Button);
        }

        private void Hp100Upgrade_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(100, ItemType.HealthUpgrade100, Hp100Upgrade_Button);
        }

        private void ShieldUpgrade_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(100, ItemType.ShieldUpgradeNew, ShieldUpgrade_Button);
        }

        private void ShieldHeal_Button_Click(object sender, RoutedEventArgs e)
        {
            BuyItem(50, ItemType.ShieldUpgradeRefresh, ShieldHeal_Button);
        }

        async System.Threading.Tasks.Task PreviewErrorMSGAsync()
        {
            var dialog = new MessageDialog("You dont have enough coins to buy this item.");
            dialog.Title = "Not enough coins!";

            dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
            var ans = await dialog.ShowAsync();
        }
    }
}
