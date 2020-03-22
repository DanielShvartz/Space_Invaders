using System;
using System.Collections.Generic;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        async System.Threading.Tasks.Task LoadPlayerAsync()
        {
            //we create a proxy to access the db
            Final_Project.Player_ServiceRef.SavePlayer_ServiceClient proxy = new Final_Project.Player_ServiceRef.SavePlayer_ServiceClient();

            var resultUsername = await proxy.IsUsernameExistsAsync(UsernameText.Text); // we check first if the username exists
            if(resultUsername) // if he exists we check for the password
            {
                var result = await proxy.IsPlayerExistsAsync(UsernameText.Text, PasswordText.Text); // we check if the player exists (with the password
                if (result)  // if he does exist
                {
                    //we dont know which hearts images to load to so we check
                    var playerResult = await proxy.LoadPlayerAsync(UsernameText.Text, PasswordText.Text); // we load him

                    List<double> ShieldHp = new List<double>() { playerResult.Shield1_HP, playerResult.Shield2_HP, playerResult.Shield3_HP }; // init array of 3 shield hp

                    Image shield_Image_1 = new Image(); // we load the images of the shields
                                                        //if any things doesnt work create 3 objects
                    shield_Image_1.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Shield.png")); // we load shield images
                    List<Image> Shields_Images = new List<Image>() { shield_Image_1, shield_Image_1, shield_Image_1 }; // to pass

                    for (int i = 0; i < 3; i++) // check for missing shield
                        if (ShieldHp[i] <= 0) // if shield hp is less then 0 or 0 
                            Shields_Images[i] = null; // we delete its image

                    List<Image> Shields_hp_Images; // we load the hp images of the shields
                    Shields_hp_Images = LoadShieldImages(playerResult.Shield1_Image, playerResult.Shield2_Image, playerResult.Shield3_Image); // we dont know which images to load so we send the number of the image of each shield to load

                    Classes.Data player_data = new Classes.Data(playerResult.Current_Level, playerResult.Coins, playerResult.HP,
                        playerResult.SpaceShip_Level, (Final_Project.Classes.Bullets)playerResult.Bullet_Level, Shields_Images, 
                        Shields_hp_Images, ShieldHp, UsernameText.Text, PasswordText.Text, true);

                    Frame.Navigate(typeof(GamePage), player_data); // pass to page after got all data
                }
                else // if he doesnt exist - we let him enter new password or create new player
                {
                    var dialog = new MessageDialog("You have entered a wrong password!\nPlease enter a correct password or create a new player");
                    dialog.Title = "Couldn't Login!";
                    dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
                    var ans = await dialog.ShowAsync();
                }
            }
            else // if the username doesnt exist there is no need to check the password - we just start new game with the given info
            {
                Classes.Data player_data = new Classes.Data();
                player_data.username = UsernameText.Text; // we only pass username and password 
                player_data.password = PasswordText.Text;
                player_data.isExitingInDB = false; // but he doesnt exist in the db so we will need to add him
                Frame.Navigate(typeof(GamePage), player_data); // we send that he has no data and the data will be create thoughout the game
                
            }

        }
        /// <summary>
        /// The function gets the numbers of images for each shield
        /// </summary>
        /// <returns>
        /// The function returns a list with a the images of for the shield depands of given number
        /// </returns>
        List<Image> LoadShieldImages(int Shield1_image_Num, int Shield2_image_Num, int Shield3_image_Num)
        {
            Image ShieldImage1 = null, ShieldImage2 = null, ShieldImage3 = null; // set images
            List<Image> ShieldImageList = new List<Image>() { ShieldImage1, ShieldImage2, ShieldImage3 }; // the image for shield
            List<int> ShieldImageNumber = new List<int>() { Shield1_image_Num, Shield2_image_Num, Shield3_image_Num }; // the number of the image

            for (int i = 0; i < 3; i++) // we move on the numbers
            {
                if (ShieldImageNumber[i] == 0) // if we have null the shield doesnt exist
                    ShieldImageList[i] = null; // so we he wont have image
                else // if a shield exist
                {
                    string imgLocation = string.Format("ms-appx:///Assets/HealthPoints/hp_{0}.png", ShieldImageNumber[i]); // we take the number of the image to load
                    ShieldImageList[i] = new Image(); // because it was set as null we create new image, without this, no image will be created
                    ShieldImageList[i].Source = new BitmapImage(new Uri(imgLocation)); // change the image accord
                }
            }
            return ShieldImageList; // return the list
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e) // when the player sends the data he sends username and password
        {
            LoadPlayerAsync();
        }
    }
}
