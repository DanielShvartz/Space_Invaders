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
using Windows.UI.Xaml.Media.Imaging;
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
            if(data == null) //if he has no data 
            {
                Hp50Upgrade_Button.IsEnabled = false; // he cannot press on any buttons
                ShieldUpgrade_Button.IsEnabled = false;
                Hp100Upgrade_Button.IsEnabled = false;
                ShieldHeal_Button.IsEnabled = false;
                return;
            }

            int counterExist = 0; // check if he has 3 shields
            for (int i = 0; i < data.Shields_Images.Count(); i++)
                if (data.Shields_Images[i] != null) // if shield is not null - an image is exiting - we counter
                    counterExist++;
            if (counterExist == 3) // if he has 3 shields - he cannot buy more
                ShieldUpgrade_Button.IsEnabled = false; // but if he has 2 or 1 or 0 shields he can buy.

            //data.coins += 1500;
            Coin_Text.Text = "Coins:" + data.coins;
        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage), data);
        }

        //doesnt add value just replace
        void createNewShield(int itemNewIndex) //index of new item to insert
        {
            Image shield_image = new Image(); // create new shield image
            shield_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Shield.png"));
            //no need to insert just pin point by index
            data.Shields_Images[itemNewIndex] = shield_image; // push by index
            
            Image shield_image_hp = new Image(); // create new shield hp image
            shield_image_hp.Source = new BitmapImage(new Uri("ms-appx:///Assets/HealthPoints/hp_8.png"));
            shield_image_hp.Tag = itemNewIndex; // to know which tag is he having
            data.Shields_hp_Images[itemNewIndex] =  shield_image_hp;

            data.ShieldHp[itemNewIndex] =  24; // insert new hp

            //no need to create shield rectangle because it stays the same
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
                if (itemType == ItemType.HealthUpgrade50)// 50 hp buy  // we set the wanted item by its id
                    data.player_HitPoints += 50;
                else if (itemType == ItemType.HealthUpgrade100) // 50 hp buy
                    data.player_HitPoints += 100;
                else if (itemType == ItemType.ShieldUpgradeNew) // new shield buy
                {
                    //if the player has 3 shields - doesnt buy - own respo
                    for (int i = 0; i < data.Shields_Images.Count(); i++)
                    {
                        if (data.Shields_Images[i] == null) // found missing shield
                        {
                            createNewShield(i); // send index and not tag
                            return;
                            /*
                             * If we break here - it will disable the button and then we can only buy 1 shield
                             * if we return we can buy again because it doesnt disable the button
                             * now we can buy few times but if he has 3 shields the button will get disabled because it
                             * wont enter the loop
                             */
                        }

                    }
                }
                else if (itemType == ItemType.ShieldUpgradeRefresh)
                {
                    for (int i = 0; i < data.Shields_Images.Count(); i++)
                    {
                        if(data.Shields_Images[i] != null) // if its null we cannot heal it - it doesnt exist
                        {
                            data.ShieldHp[i] = 24; // if its existing - load full hp in the array
                            data.Shields_hp_Images[i].Source = new BitmapImage(new Uri("ms-appx:///Assets/HealthPoints/hp_8.png"));
                            //set full hp image
                        }
                    }
                }
                buttonChosen.IsEnabled = false; // we disable the button so he cant buy again
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

        private void GameReturnPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage), data);
        }
    }
}
