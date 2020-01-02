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
        Enemy enemy;
        List<Enemy> enemy_Control;
        DispatcherTimer game_timer_movement;
        int counterPress = 0;

        public GamePage()
        {
            this.InitializeComponent();
        }

        private void initEnemies(Canvas canvas)
        {
            const double startingX = 155; //defualt values which can adjust the settings of the enemies creaiton
            const double spacingX = 120;
            const double startingY = -16;
            const double spacingY = -150;

            string imLocation = "";
            int counter = 1; // after each 12 moves  - changes the enemys pictures and moves down
            for (int i = 3; i >= 1; ) // this loops runs 3 times and create 3 rows
            {
                if (counter <= 12) // each 12 runs create 12 aliens
                {
                    switch(i)
                    {
                        case 3:
                            imLocation = "ms-appx:///Assets/Enemys/Ship_Level3.png"; // each run we have different ship image
                            break;
                        case 2:
                            imLocation = "ms-appx:///Assets/Enemys/Ship_Level2.png";
                            break;
                        case 1:
                            imLocation = "ms-appx:///Assets/Enemys/Ship_Level1.png";
                            break;
                    }

                    // the x and y is also the space between them
                    enemy = new Enemy(startingX + (spacingX * counter), startingY + (spacingY * (i-3)), 10, 10, this.canvas, imLocation, i); // dx and dy are not yet given
                    enemy_Control.Add(enemy); // add to list
                    counter++; // add the counter
                }
                else // if we have 12 in a row - we move 1 down, and start from the left
                {
                    i--;
                    counter = 1; // we reset the counter to have more 12
                }
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            counterPress = 0;

            //initalize componenets when the game starts
            player = new Player(10, this.canvas, "ms-appx:///Assets/SpaceShip/Spaceship_Default.png");

            // when the page is loaded we create a new list
            bullet_Control = new List<Bullet>(); // this lists also allows to remove hit bullets from the canvas and check for bullet collution
            enemy_Control = new List<Enemy>();
            initEnemies(canvas);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; ; //if key was press give control to windows or event

            //create a timer that moves the bullets, also if there are no bullets it wont move ( count = 0 )
            // if there are bullets it will iterate the list and move all the bullets
            game_timer_movement = new DispatcherTimer(); // this also checks for hits collusions
            game_timer_movement.Interval = TimeSpan.FromTicks(1); // CHANGE FromMilliseconds(150)
            game_timer_movement.Tick += Game_timer_movement_Tick;
            game_timer_movement.Start(); // this timer is always running and moving bullets no matter what

        }

        private void Game_timer_movement_Tick(object sender, object e)
        {
            if (bullet_Control.Count() == 0) // if there are no bullets
                return;
            else // we run on both lists, the first loop runs on each bullet and then each bullet, runs in second loop that checks the other enemies.
            {// we check for collusion and then move
                for (int i = 0; i < bullet_Control.Count(); i++) //move on each bullet
                {
                    Rect enemyRectangle;
                    Rect bulletRectangle = new Rect(bullet_Control[i].getBulletInfo()[0], bullet_Control[i].getBulletInfo()[1], bullet_Control[i].getBulletInfo()[2], bullet_Control[i].getBulletInfo()[3]);
                    // we create a rectangle for the bullet and then we create a rectangle for each enemy and check if they were hit
                    for (int j = 0; j < this.enemy_Control.Count(); j++)
                    {
                        enemyRectangle = new Rect(enemy_Control[j].getPlayerLocation()[0], enemy_Control[j].getPlayerLocation()[1], enemy_Control[j].GetWidth(), enemy_Control[j].GetHeight());
                        Rect r = RectHelper.Intersect(bulletRectangle, enemyRectangle);
                        if (r.Width > 0 || r.Height > 0) // if there is collusion
                        {
                            Debug.WriteLine("Hit!"); //TODO: make the hit system and hp system move on hp system and check for changes
                        }
                    }
                    MoveBullet(bullet_Control[i]); // move them
                }
            }
        }


        private void MoveBullet(Bullet bullet)
        {
            double[] bulletInfo = bullet.getBulletInfo();
            double bulletSpeed = bullet.GetBulletSpeed();

            if (bulletInfo[1] + bulletSpeed < 0)  // if he reaches the border
            { 
                canvas.Children.Remove(bullet.GetBulletImage()); // remove the image
                bullet_Control.Remove(bullet); // we just remove it from the list and from the canvas, no need to stop timer

            }
            else
            {
                bullet.Move();
                Canvas.SetTop(bullet.GetBulletImage(), bullet.getBulletInfo()[1]); // move
            }
        }


        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            
            if (counterPress == 21) // after that we unblock agai
                counterPress = 0;
            if (args.VirtualKey == VirtualKey.Left) // if its left it moves left
                player.Move("Left");
            if (args.VirtualKey == VirtualKey.Right) // if its right we move right
                player.Move("Right");
            if (args.VirtualKey == VirtualKey.Space) // if he presses space it creates a new bullet for the player
            {
                ////cooldown
                if (counterPress >= 10 && counterPress <= 20) // if he got to the 10 - we check it before the keys to block key pressing
                {
                    counterPress++; //  we keep uploading and block him 10 times from checking the other keys so he blocks the keys from pressing
                    return;
                }
                else // if he is not on cooldown
                {
                    //first place the bullet on the canvas places in the middle of the ship
                    bullet = new Bullet(player.getPlayerLocation()[0] + (player.GetWidth() / 2), player.getPlayerLocation()[1], canvas, Bullets.Light_Shell_Default);
                    bullet_Control.Add(bullet); // add to the control list the current bullet
                    counterPress++; // each press we count how much time it was pressed
                }
            }     
        }




    }
}
