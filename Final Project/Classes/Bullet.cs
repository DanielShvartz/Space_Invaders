using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;
using System.Diagnostics;

namespace Final_Project.Classes
{
    public enum Bullets
    {
        Light_Shell_Default = 0, Enemy_Shell, Plasma, Laser, Medium_Shell, Heavy_Shell, Sniper_Shell, Granade_Shell
    };
    class Bullet
    {
        private double x; // given parameters by ship
        private double y; // x and y given by ships location and canvas
        private Canvas canvas;

        private double width; // are set as each images, values are different
        private double height;

        private Bullets bullet_type; // set values by chosen bullet type
        private double speedDy;
        public double damage; // speed of bullet, dmg and image
        Image bullet_image;

        private const int PLAYER_EXTRA_DMG = 2;

        public Bullet(double Shipx, double Shipy, Canvas canvas, Bullets bullet_type, int currentLevel)
        {
            this.canvas = canvas;
            this.bullet_type = bullet_type;
            this.bullet_image = new Image();
            this.x = Shipx - 10; // x and y are set by ship so we dont know the given locations only the ship gives it

            //if the players spaceship is strong - it deals more dmg
            // bullet type defines which speed, dmg and which image will be places, ship places the bullet on the canvas
            switch (bullet_type)
            {
                //user bullets - go from down to up so speed is minues
                case Bullets.Light_Shell_Default:
                    speedDy = -17; // fast but no dmg
                    damage = 0.5 + currentLevel * PLAYER_EXTRA_DMG; 
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Light_Shell_Default.png"));
                    bullet_image.Width = 19;
                    bullet_image.Height = 29;
                    break;

                case Bullets.Medium_Shell:
                    speedDy = -11; // medium speed but good dmg
                    damage = 1 + currentLevel * PLAYER_EXTRA_DMG;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Medium_Shell.png"));
                    bullet_image.Width = 22;
                    bullet_image.Height = 31;
                    break;

                case Bullets.Heavy_Shell:
                    speedDy = -7;
                    damage = 1.5 + currentLevel * PLAYER_EXTRA_DMG; // slow but big dmg
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Heavy_Shell.png"));
                    bullet_image.Width = 31;
                    bullet_image.Height = 49;
                    this.x -= 5;
                    break;

                case Bullets.Sniper_Shell:
                    speedDy = -27; // fast and very high dmg
                    damage = 2.5 + currentLevel * PLAYER_EXTRA_DMG;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Sniper_Shell.png"));
                    bullet_image.Width = 28;
                    bullet_image.Height = 58;
                    this.x -= 5; // location set
                    break;

                case Bullets.Granade_Shell:
                    speedDy = -16;
                    damage = 2;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Granade_Shell.png"));
                    bullet_image.Width = 19;
                    bullet_image.Height = 37;
                    break;

                //enemy bullets - go up to down - so speed plus
                // to adujst by enemy levels these are as enemy levels so we can choose each much easly
                case Bullets.Enemy_Shell:
                    speedDy = 17;
                    damage = 0.5 + currentLevel;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Enemys/Enemy_Bullet.png"));
                    bullet_image.Width = 21;
                    bullet_image.Height = 96;
                    break;

                case Bullets.Plasma:
                    speedDy = 19;
                    damage = 1.5 + currentLevel;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Plasma_Enemy.png"));
                    bullet_image.Width = 24;
                    bullet_image.Height = 87;
                    this.x -= 3.5;
                    break;

                case Bullets.Laser:
                    speedDy = 21; 
                    damage = 2.5 + currentLevel;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Laser_Enemy.png"));
                    bullet_image.Width = 12;
                    bullet_image.Height = 72;
                    this.x -= 6;
                    break;

               
            }

            if (isPlayerBullet()) // if the bullet is players
                this.y = Shipy - bullet_image.Height; // we move the image up
            else
                this.y = Shipy + bullet_image.Height; // if its enemy we move the image down

            if (bullet_type == Bullets.Laser)
                this.y += 20;

            Canvas.SetLeft(bullet_image, this.x); // place and add to canvas
            Canvas.SetTop(bullet_image, this.y);
            canvas.Children.Add(bullet_image);

            height = bullet_image.ActualHeight;
            width = bullet_image.ActualWidth;
        }

        public double[] getBulletInfo() // bullets height and width are different
        {
            double[] ret = { this.x, this.y, this.width, this.height};
            return ret;
        }

        public Image GetBulletImage()
        {
            return this.bullet_image;
        }

        public double GetBulletSpeed()
        {
            return this.speedDy;
        }

        public void Move()
        {
            this.y += speedDy;
        }

        public bool isPlayerBullet()
        {
            // if the bullet is between 1-3 is enemy if not its players
            return !((int)this.bullet_type >= 1 && (int)bullet_type <= 3);
        }
    }
}

