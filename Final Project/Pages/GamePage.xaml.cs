using Final_Project.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class GamePage : Page
    { 
        List<Bullet> bullet_Control; // we will have a list of bullets to control the bullets movement
        Player player;
        Bullet bullet;
        DispatcherTimer bullet_timer_movement;


        public GamePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //initalize componenets when the game starts
            player = new Player(10, this.canvas, "ms-appx:///Assets/SpaceShip/Spaceship_Default.png");

            // when the page is loaded we create a new list
            bullet_Control = new List<Bullet>(); // this list also allows to remove hit bullets from the canvas and check for bullet collution

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; ; //if key was press give control to windows or event

            //create a timer that moves the bullets, also if there are no bullets it wont move ( count = 0 )
            // if there are bullets it will iterate the list and move all the bullets
            bullet_timer_movement = new DispatcherTimer();
            bullet_timer_movement.Interval = TimeSpan.FromMilliseconds(150);
            bullet_timer_movement.Tick += Bullet_timer_movement_Tick; ;
            bullet_timer_movement.Start(); // this timer is always running and moving bullets no matter what
        }

        private void Bullet_timer_movement_Tick(object sender, object e)
        {
            if (bullet_Control.Count == 0) // if there are no bullets
                return;
            else
            {
                foreach(Bullet bullet in bullet_Control)
                {

                    double[] bulletInfo = bullet.getBulletInfo();
                    double bulletSpeed = bullet.GetBulletSpeed();

                    if (bulletInfo[1] + bulletSpeed < 0) // if he reaches the border  - (bulletInfo[2])
                    { // maybe remove from list and stop timer from here and not from outside the function
                      //Debug.WriteLine("y = " + bulletInfo[1] + " bulletSpeed = " + bulletSpeed + " canvas.ActualHeight = " + canvas.ActualHeight +  " bullet height - " + bulletInfo[2]);
                        canvas.Children.Remove(bullet.GetBulletImage()); // remove the image
                        return false; // false means he needs to be removed from the list

                        bullet_Control.Remove(bullet);
                        bullet.bullet_timer_movement.Stop(); // stop timer, remove from canvas and list

                    }
                    else
                    {
                        bullet.Move();
                        Canvas.SetTop(bullet.GetBulletImage(), bullet.getBulletInfo()[1]);
                        return true; // true means he moves
                    }
                }
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == VirtualKey.Left) // if its left it moves left
                player.Move("Left");
            if (args.VirtualKey == VirtualKey.Right) // if its right we move right
                player.Move("Right");
            if (args.VirtualKey == VirtualKey.Space)
            {
                //first place the bullet on the canvas places in the middle of the ship
                bullet = new Bullet(player.getPlayerLocation()[0] + (player.GetWidth() / 2), player.getPlayerLocation()[1], canvas, Bullets.Light_Shell_Default);
                bullet_Control.Add(bullet); // add to the control list the current bullet
            }
        }  
    }
}
