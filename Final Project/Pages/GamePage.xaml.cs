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
        DispatcherTimer enemy_create_bullet_timer;
        List<Bullet> enemy_bullet_control;

        static Random rnd;
        const int RANDOM_MAX_VALUE = 8;
        const int RANDOM_MIN_VALUE = 2;

        double levelSpeedY = 1; // for now this is the level speed, each level updates the speed of the enemy
        double levelSpeedX = 1;

        List<double> ShieldHp; // init array of 3 shield hp
        List<Rect> shieldRectangles; // init array of 3 rectnagle of the shields to check hits

        public GamePage()
        {
            this.InitializeComponent();
        }

        private void initEnemies(Canvas canvas)
        {
            const double startingX = 165; //defualt values which can adjust the settings of the enemies creaiton
            const double spacingX = 120;
            const double startingY = 0; //-10
            const double spacingY = -130;

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
                    enemy = new Enemy(startingX + (spacingX * counter), startingY + (spacingY * (i - 3)), rnd.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE) * levelSpeedY, rnd.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE) * levelSpeedX, this.canvas, imLocation, i); // dx and dy are not yet given
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
            rnd = new Random();

            //initalize componenets when the game starts
            player = new Player(15, this.canvas, "ms-appx:///Assets/SpaceShip/Spaceship_Default.png");

            // when the page is loaded we create a new list
            bullet_Control = new List<Bullet>(); // this lists also allows to remove hit bullets from the canvas and check for bullet collution
            enemy_bullet_control = new List<Bullet>();
            enemy_Control = new List<Enemy>();
            initEnemies(canvas);

            ShieldHp = new List<double>() { 20, 20, 20 };

            shieldRectangles = new List<Rect>() // init rectangle for collusion
            {
               new Rect(250, 638, 290, 332),  new Rect(842, 638, 290, 332), new Rect(1442, 638, 290, 332)
            };

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;//if key was press give control to windows or event

            //create a timer that moves the bullets, also if there are no bullets it wont move ( count = 0 )
            // if there are bullets it will iterate the list and move all the bullets
            game_timer_movement = new DispatcherTimer(); // this also checks for hits collusions
            game_timer_movement.Interval = TimeSpan.FromTicks(1); // CHANGE FromMilliseconds(150)
            game_timer_movement.Tick += Game_timer_movement_Tick;
            game_timer_movement.Start(); // this timer is always running and moving bullets no matter what

            //create a timer that create a bullets for the enemys. it create a bullet and the game movement timer will move it.
            // the game movement timer needs also to move on all the enemy bullets and check for collusion
            enemy_create_bullet_timer = new DispatcherTimer();
            enemy_create_bullet_timer.Interval = TimeSpan.FromSeconds(1); // you can always change how much fast enemys shoot
            enemy_create_bullet_timer.Tick += Enemy_create_bullet_timer_Tick;
            enemy_create_bullet_timer.Start();
        }

        private void Enemy_create_bullet_timer_Tick(object sender, object e) // create bullets for enemy each X times and add to list
        {
            //if the player won - we stop this timer and not check because we dont have any more enemies
            int chosenEnemy = rnd.Next(0, enemy_Control.Count()); // choose enemy to get him a new bullet
            
            //create a bullet for the enemy, in the middle of the enemy, where the enemy level can adjust its bullet type, so level one is normal two is plasma 3 is laser
            bullet = new Bullet(enemy_Control[chosenEnemy].getPlayerLocation()[0] + (enemy_Control[chosenEnemy].GetWidth() / 2), enemy_Control[chosenEnemy].getPlayerLocation()[1], canvas, (Bullets)enemy_Control[chosenEnemy].enemyLevel);
            enemy_bullet_control.Add(bullet); // add to the control list the current enemy bullet
        }

        private void Game_timer_movement_Tick(object sender, object e)
        {
            bool hitRemoved = true;
            bool shieldHit = true;

            if(bullet_Control.Count() != 0) // if we have bullets we run on both lists , the first loop runs on each bullet and then each bullet,
            {//runs in second loop that checks the other enemies. we check for collusion and then move
                for (int i = 0; i < bullet_Control.Count(); i++) //move on each bullet
                {
                    hitRemoved = true;
                    Rect enemyRectangle;
                    Rect bulletRectangle = new Rect(bullet_Control[i].getBulletInfo()[0], bullet_Control[i].getBulletInfo()[1], bullet_Control[i].getBulletInfo()[2], bullet_Control[i].getBulletInfo()[3]);
                    // we create a rectangle for the bullet and then we create a rectangle for each enemy and check if they were hit
                    for (int j = 0; j < this.enemy_Control.Count(); j++)
                    {
                        enemyRectangle = new Rect(enemy_Control[j].getPlayerLocation()[0], enemy_Control[j].getPlayerLocation()[1], enemy_Control[j].GetWidth(), enemy_Control[j].GetHeight());
                        Rect r = RectHelper.Intersect(bulletRectangle, enemyRectangle);
                        if (r.Width > 0 || r.Height > 0) // if there is collusion
                        {
                            //bullet control - i , enemy control - j
                            if(bullet_Control[i].damage > enemy_Control[j].hitPoints) // if the damage of the bullet is larger than the hitpoints
                            {
                                bullet_Control[i].damage -= enemy_Control[j].hitPoints; //remove damage as much as hp
                                canvas.Children.Remove(enemy_Control[j].GetImage()); // remove enemy from list and canvas
                                enemy_Control.Remove(enemy_Control[j]); // the bullet continues to the next enemy that he gets
                                //explode enemy animation
                                MoveBullet(bullet_Control[i]); // we move the bullet only if possible
                            }
                            else if (bullet_Control[i].damage < enemy_Control[j].hitPoints) // if the damage of the bullet is less than the enemy hp
                            {
                                enemy_Control[j].hitPoints -= bullet_Control[i].damage; // remove hp as much dmg 
                                canvas.Children.Remove(bullet_Control[i].GetBulletImage()); // remove bullet from canvas and from list
                                bullet_Control.Remove(bullet_Control[i]);
                                hitRemoved = false;
                                break;  // if the bullet is already removed from the list there is no need to check we all the enemies for to continue
                                //explode animation
                            }
                            else if(bullet_Control[i].damage == enemy_Control[j].hitPoints)
                            {
                                ////explode animation remove bullet and enemy from lists and canvas
                                canvas.Children.Remove(bullet_Control[i].GetBulletImage());
                                bullet_Control.Remove(bullet_Control[i]);
                                canvas.Children.Remove(enemy_Control[j].GetImage());
                                enemy_Control.Remove(enemy_Control[j]);
                                hitRemoved = false;
                                break; // if the bullet is already removed from the list there is no need to check we all the enemies for to continue
                            }
                        }
                    }
                    if (hitRemoved) // After moving on all the enemies, move ,if hit and removed, doesnt move. for each bullet
                    {
                        MoveBullet(bullet_Control[i]); // moves if hits and bigger dmg than hp or didnt hit at all
                    }
                }
            }
            if(enemy_Control.Count() != 0)
                for (int i = 0; i < enemy_Control.Count(); i++) // move the the enemies 
                    enemy_Control[i].Move();

            for (int i = 0; i < enemy_bullet_control.Count(); i++) // move on each bullet of the enemy
            {
                shieldHit = true;
                Rect enemybullet = new Rect(enemy_bullet_control[i].getBulletInfo()[0], enemy_bullet_control[i].getBulletInfo()[1], enemy_bullet_control[i].getBulletInfo()[2], enemy_bullet_control[i].getBulletInfo()[3]);
                for(int j = 0; j < shieldRectangles.Count(); j++) // create a rect for each bullet and move on the shield that were already made
                {
                    Rect r = RectHelper.Intersect(enemybullet, shieldRectangles[j]);
                    if (r.Width > 20 || r.Height > 20) // if they were hit
                    {
                        canvas.Children.Remove(enemy_bullet_control[i].GetBulletImage()); // remove from canavas first
                        ShieldHp[j] -= enemy_bullet_control[i].damage; // then reduce hp from shield on the array
                        enemy_bullet_control.Remove(enemy_bullet_control[i]); // at the end remove bullet
                        shieldHit = false; // mark they were hit and bullet doesnt need to move

                        Debug.WriteLine("Shield-" + (j + 1) + " hp-" + ShieldHp[j]);
                    }
                }
                if(shieldHit)
                    MoveBullet(enemy_bullet_control[i]);
            }
        }

        private void MoveBullet(Bullet bullet)
        {
            double[] bulletInfo = bullet.getBulletInfo();
            double bulletSpeed = bullet.GetBulletSpeed();

            //todo: command tihs, explain why removes from the right list, check bullet movement and see what happends

            if (bulletInfo[1] + bulletSpeed < 0 || bulletInfo[1] + bulletSpeed > canvas.ActualHeight)
            { // if the bullet reaches the upper section of the screen(0 for the player) or the bullet reachers the lower section of the screen(canvas.ActualHeight - for the enemy bullet)

                canvas.Children.Remove(bullet.GetBulletImage()); // remove the image
                if(bullet.isPlayerBullet()) // check if the bullet is player or enemies
                {
                    bullet_Control.Remove(bullet); // we just remove it from the list and from the canvas, no need to stop timer
                }
                else
                {
                    enemy_bullet_control.Remove(bullet);
                }
            }
            else
            {
                bullet.Move(); // move the bullet and update image
                Canvas.SetTop(bullet.GetBulletImage(), bullet.getBulletInfo()[1]); 
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
                if (counterPress >= 9 && counterPress <= 20) // if he got to the 10 - we check it before the keys to block key pressing
                {
                    counterPress++; //  we keep uploading and block him 10 times from checking the other keys so he blocks the keys from pressing
                    return;
                }
                else // if he is not on cooldown
                {
                    //first place the bullet on the canvas places in the middle of the ship
                    bullet = new Bullet(player.getPlayerLocation()[0] + (player.GetWidth() / 2), player.getPlayerLocation()[1], canvas, Bullets.Sniper_Shell);
                    bullet_Control.Add(bullet); // add to the control list the current bullet
                    counterPress++; // each press we count how much time it was pressed
                }
            }     
        }
    }
}

