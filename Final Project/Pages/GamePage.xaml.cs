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
    public sealed partial class GamePage : Page
    { 
        Player player;
        Bullet bullet;
        Enemy enemy;
        static Random rnd;

        DispatcherTimer game_timer_movement;
        DispatcherTimer enemy_create_bullet_timer;

        double Player_HitPoints = 100;
        int counterPress = 0;
        const int RANDOM_MAX_VALUE = 8;
        const int RANDOM_MIN_VALUE = 2;
        const double SHIELD_HP_NUM = 24;
        const double SHIELD_REDUCEMENT = SHIELD_HP_NUM / 8;
        double levelSpeedY = 1; // for now this is the level speed, each level updates the speed of the enemy
        double levelSpeedX = 1;
        int coins = 0;

        List<Bullet> bullet_Control; // we will have a list of bullets to control the bullets movement
        List<Bullet> enemy_bullet_control;
        List<Enemy> enemy_Control;
        List<Image> Shields_Images;
        List<Image> Shields_hp_Images;
        List<double> ShieldHp; // init array of 3 shield hp
        List<Rect> ShieldRectangles; // init array of 3 rectnagle of the shields to check hits
        

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

        async System.Threading.Tasks.Task MessageStartAsync()
        {
            var dialog = new MessageDialog("Press Ok To start!");
            dialog.Title = "NewGame";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            await dialog.ShowAsync();
            
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

            Shields_Images = new List<Image> { shield_1, shield_2, shield_3}; // init list of shields to be able to remove them easly and thier hp
            Shields_hp_Images = new List<Image> { shield_hp_1, shield_hp_2, shield_hp_3 };
            ShieldHp = new List<double>() { SHIELD_HP_NUM, SHIELD_HP_NUM, SHIELD_HP_NUM }; // shields hp
            ShieldRectangles = new List<Rect>() // init rectangle for collusion
            {
               new Rect(250, 638, 290, 332),  new Rect(842, 638, 290, 332), new Rect(1442, 638, 290, 332)
            };

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;//if key was press give control to windows or event

            //create a timer that moves the bullets, also if there are no bullets it wont move ( count = 0 )
            // if there are bullets it will iterate the list and move all the bullets
            game_timer_movement = new DispatcherTimer(); // this also checks for hits collusions
            game_timer_movement.Interval = TimeSpan.FromTicks((long)0.5); // CHANGE FromMilliseconds(150)
            game_timer_movement.Tick += Game_timer_movement_Tick;
            game_timer_movement.Start(); // this timer is always running and moving bullets no matter what

            //create a timer that create a bullets for the enemys. it create a bullet and the game movement timer will move it.
            // the game movement timer needs also to move on all the enemy bullets and check for collusion
            enemy_create_bullet_timer = new DispatcherTimer();
            enemy_create_bullet_timer.Interval = TimeSpan.FromSeconds(0.5); // you can always change how much fast enemys shoot
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
            bool bulletGotHit = true;
            double remainHP;

            if (bullet_Control.Count() != 0) // bullets from player - check for hit with enemys and remove them:
                //if we have bullets we run on both lists , the first loop runs on each bullet and then each bullet,
            {//runs in second loop that checks the other enemies. we check for collusion and then move
                for (int player_bullet = 0; player_bullet < bullet_Control.Count(); player_bullet++) //move on each bullet
                {
                    hitRemoved = true;
                    Rect enemyRectangle;
                    Rect bulletRectangle = new Rect(bullet_Control[player_bullet].getBulletInfo()[0], bullet_Control[player_bullet].getBulletInfo()[1], bullet_Control[player_bullet].getBulletInfo()[2], bullet_Control[player_bullet].getBulletInfo()[3]);
                    // we create a rectangle for the bullet and then we create a rectangle for each enemy and check if they were hit
                    for (int enemy = 0; enemy < this.enemy_Control.Count(); enemy++)
                    {
                        enemyRectangle = new Rect(enemy_Control[enemy].getPlayerLocation()[0], enemy_Control[enemy].getPlayerLocation()[1], enemy_Control[enemy].GetWidth(), enemy_Control[enemy].GetHeight());
                        Rect r = RectHelper.Intersect(bulletRectangle, enemyRectangle);
                        if (r.Width > 0 || r.Height > 0) // if there is collusion
                        {
                            //bullet control - i , enemy control - j
                            if (bullet_Control[player_bullet].damage > enemy_Control[enemy].hitPoints)  // if the damage of the bullet is larger than the hitpoints - enemy dies here - getting coins
                            {
                                bullet_Control[player_bullet].damage -= enemy_Control[enemy].hitPoints; //remove damage as much as hp
                                canvas.Children.Remove(enemy_Control[enemy].GetImage()); // remove enemy from list and canvas

                                coins += enemy_Control[enemy].enemyLevel; // add coins and then remove enemy
                                Coins_Text.Text = coins.ToString();

                                enemy_Control.Remove(enemy_Control[enemy]); // the bullet continues to the next enemy that he gets
                                //explode enemy animation
                                MoveBullet(bullet_Control[player_bullet]); // we move the bullet only if possible

                                
                            }
                            else if (bullet_Control[player_bullet].damage < enemy_Control[enemy].hitPoints) // if the damage of the bullet is less than the enemy hp
                            {
                                enemy_Control[enemy].hitPoints -= bullet_Control[player_bullet].damage; // remove hp as much dmg 
                                canvas.Children.Remove(bullet_Control[player_bullet].GetBulletImage()); // remove bullet from canvas and from list
                                bullet_Control.Remove(bullet_Control[player_bullet]);
                                hitRemoved = false;
                                break;  // if the bullet is already removed from the list there is no need to check we all the enemies for to continue
                                //explode animation
                            }
                            else if (bullet_Control[player_bullet].damage == enemy_Control[enemy].hitPoints) // enemy dies here - getting coins
                            {
                                ////explode animation remove bullet and enemy from lists and canvas
                                canvas.Children.Remove(bullet_Control[player_bullet].GetBulletImage());
                                bullet_Control.Remove(bullet_Control[player_bullet]);
                                canvas.Children.Remove(enemy_Control[enemy].GetImage());

                                coins += enemy_Control[enemy].enemyLevel; // add coins and then remove enemy
                                Coins_Text.Text = coins.ToString();

                                enemy_Control.Remove(enemy_Control[enemy]);
                                hitRemoved = false;
                                break; // if the bullet is already removed from the list there is no need to check we all the enemies for to continue
                            }
                        }
                    }
                    if (hitRemoved) // After moving on all the enemies, move ,if hit and removed, doesnt move. for each bullet
                        MoveBullet(bullet_Control[player_bullet]); // moves if hits and bigger dmg than hp or didnt hit at all
                }
            }
            if(enemy_Control.Count() != 0) // move enemys
                for (int i = 0; i < enemy_Control.Count(); i++) // move the the enemies 
                    enemy_Control[i].Move();

            for (int bullet = 0; bullet < enemy_bullet_control.Count(); bullet++) //  - for each bullet i - per bullet - move on each bullet of the enemy
            {
                bulletGotHit = false;
                Rect r;
                Rect enemybulletRect = new Rect(enemy_bullet_control[bullet].getBulletInfo()[0], enemy_bullet_control[bullet].getBulletInfo()[1], enemy_bullet_control[bullet].getBulletInfo()[2], enemy_bullet_control[bullet].getBulletInfo()[3]);
                //check hit with shield
                for (int shield = 0; shield < ShieldRectangles.Count(); shield++) // j - per shield - create a rect for each bullet and move on the shield that were already made
                {
                    r = RectHelper.Intersect(enemybulletRect, ShieldRectangles[shield]);
                    if (r.Width > 20 || r.Height > 20) //if they were hit
                    {
                        canvas.Children.Remove(enemy_bullet_control[bullet].GetBulletImage()); // remove from the bullet from the canavas first

                        remainHP = ShieldHp[shield] - enemy_bullet_control[bullet].damage; // take how much hp remained to calculate which image to preview

                        //if the hp updates - we update on the canvas, the lists of images shield and list of images hp
                        UpdateShieldHpImage(remainHP, shield, bullet);

                        enemy_bullet_control.Remove(enemy_bullet_control[bullet]); // at the end remove bullet
                        bulletGotHit = true; // mark they were hit and bullet doesnt need to move

                        //Debug.WriteLine("Shield-" + (j + 1) + " hp-" + ShieldHp[j]);
                    }
                }
                // check hit with player
                //create rect for player - player moves so rect is changing
                Rect playerRect = new Rect(player.getPlayerLocation()[0], player.getPlayerLocation()[1], player.GetWidth(), player.GetHeight());
                r = RectHelper.Intersect(enemybulletRect, playerRect); // check for hit between bullet of enemy and player
                if (r.Width > 20 || r.Height > 20) // if they were hit
                {
                    canvas.Children.Remove(enemy_bullet_control[bullet].GetBulletImage()); // remove from the bullet from the canavas first
                    Player_HitPoints -= enemy_bullet_control[bullet].damage; // reduce hp
                    Health_Text.Text = "Health: " + Player_HitPoints.ToString() + '%'; // update text box
                    //here check for 0
                    enemy_bullet_control.Remove(enemy_bullet_control[bullet]); // remove bullet from the list
                    bulletGotHit = true; // bullet got hit so he doesnt move
                }
                if (!bulletGotHit)
                    MoveBullet(enemy_bullet_control[bullet]);
            }
        }

        /// <summary>
        /// This function update an image. Removes the old one, update with the new one with given locaiton and update rectnangle size
        /// </summary>
        /// <param name="shieldNum"></param> -
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="ImageLocation"></param> -gets new image each time
        void UpdateImage(int shieldNum, double newX, double newY, string ImageLocation)
        {
            //remove old image from canvas
            canvas.Children.Remove(Shields_hp_Images[shieldNum]);
            //update list of hearts image with the right image
            Shields_hp_Images[shieldNum].Source = new BitmapImage(new Uri(ImageLocation));
            //add new image on canvas
            Canvas.SetTop(Shields_hp_Images[shieldNum], newY); // preview new heart image on the canvas by given X AND Y
            Canvas.SetLeft(Shields_hp_Images[shieldNum], newX);
            canvas.Children.Add(Shields_hp_Images[shieldNum]);
            //update rectnangle size
            ShieldRectangles[shieldNum] = new Rect(newX, newY, Shields_hp_Images[shieldNum].Width, Shields_hp_Images[shieldNum].Height);
        }

        void UpdateShieldHpImage(double remainHP, int shieldNum, int bulletNum)
        {
            double newX = 0, newY = 716; // Y is always the same

            //To know where to place the image of the shield hp image we need to know which x to set
            //beacuse we doesnt know which x to set we access the tag of the same image that we have set and its permanent
            //this fixes index image that are getting new index when shield is destroyed
            if (int.Parse(Shields_hp_Images[shieldNum].Tag.ToString()) == 0) 
                newX = 310;
            else if (int.Parse(Shields_hp_Images[shieldNum].Tag.ToString()) == 1)
                newX = 902;
            else if (int.Parse(Shields_hp_Images[shieldNum].Tag.ToString()) == 2)
                newX = 1502;

            if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 1 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 2) // 7 hp (21 - 19)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_7.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 2 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 3) // 6 hp (18 - 16)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_6.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 3 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 4) // 5 hp (15 - 13)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_5.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 4 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 5) // 4 hp (12 - 10)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_4.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 5 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 6) // 3 hp (9 - 7)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_3.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 6 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 7) // 2 hp (6 - 4)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_2.png");
            else if (remainHP <= SHIELD_HP_NUM - SHIELD_REDUCEMENT * 7 && remainHP > SHIELD_HP_NUM - SHIELD_REDUCEMENT * 8) // 1 hp (3-1)
                UpdateImage(shieldNum, newX, newY, "ms-appx:///Assets/HealthPoints/hp_1.png");
            else if (ShieldHp[shieldNum] - enemy_bullet_control[bulletNum].damage <= 0) // if the shield has 0 hp
            {
                //remove images
                canvas.Children.Remove(Shields_Images[shieldNum]); //we remove him from the canvas
                canvas.Children.Remove(Shields_hp_Images[shieldNum]); //we remove the hearts also

                Shields_hp_Images.Remove(Shields_hp_Images[shieldNum]); // we remove the image of the hearts from the list
                Shields_Images.Remove(Shields_Images[shieldNum]); // we remove the image of the shield from the list
                                                               //remove from lists
                ShieldRectangles.Remove(ShieldRectangles[shieldNum]); // we also remove him from the rectangles to not check anymore
                ShieldHp.Remove(ShieldHp[shieldNum]); // we remove its hp
                return; // if the enemy is dead we doesnt continue - fixed index access problem
            }
            ShieldHp[shieldNum] -= enemy_bullet_control[bulletNum].damage; // at the end reduce hp from shield on the array
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
                    bullet = new Bullet(player.getPlayerLocation()[0] + (player.GetWidth() / 2), player.getPlayerLocation()[1], canvas, Bullets.Light_Shell_Default);
                    bullet_Control.Add(bullet); // add to the control list the current bullet
                    counterPress++; // each press we count how much time it was pressed
                }
            }     
        }
    }
}

