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
        Light_Shell_Default = 0, Medium_Shell, Heavy_Shell, Sniper_Shell, Granade_Shell, Plasma, Laser
    };
    class Bullet
    {
        private double x; // given parameters by ship
        private double y; // x and y given by ships location and canvas
        private Canvas canvas;

        private double weight; // are set as each images, values are different
        private double height;

        private Bullets bullet_type; // set values by chosen bullet type
        private double speedDy;
        private int damage; // speed of bullet, dmg and image
        Image bullet_image;

        public DispatcherTimer bullet_timer_movement = new DispatcherTimer();

        public Bullet(double Shipx, double Shipy, Canvas canvas, Bullets bullet_type)
        {
            this.canvas = canvas;
            this.bullet_type = bullet_type;
            this.bullet_image = new Image();

            // bullet type defines which speed, dmg and which image will be places, ship places the bullet on the canvas
            switch (bullet_type)
            {
                case Bullets.Light_Shell_Default:
                    speedDy = -20; // fast but no dmg
                    damage = 1;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Light_Shell_Default.png"));
                    bullet_image.Width = 19;
                    bullet_image.Height = 29;
                    break;

                case Bullets.Medium_Shell:
                    speedDy = -15; // medium speed but good dmg
                    damage = 2;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Medium_Shell.png"));
                    bullet_image.Width = 27;
                    bullet_image.Height = 41;
                    break;

                case Bullets.Heavy_Shell:
                    speedDy = -10;
                    damage = 3; // slow but big dmg
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Heavy_Shell.png"));
                    bullet_image.Width = 36;
                    bullet_image.Height = 59;
                    break;

                case Bullets.Sniper_Shell:
                    speedDy = -30; // fast and very high dmg
                    damage = 4;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Sniper_Shell.png"));
                    bullet_image.Width = 28;
                    bullet_image.Height = 58;
                    break;

                case Bullets.Granade_Shell:
                    speedDy = -20;
                    damage = 5;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Granade_Shell.png"));
                    bullet_image.Width = 24;
                    bullet_image.Height = 47;
                    break;

                case Bullets.Plasma:
                    speedDy = -35;
                    damage = 6;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Plasma.png"));
                    bullet_image.Width = 29;
                    bullet_image.Height = 97;
                    break;

                case Bullets.Laser:
                    speedDy = -40; // highest dmg unit in game
                    damage = 7;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Laser.png"));
                    bullet_image.Width = 15;
                    bullet_image.Height = 92;
                    break;
            }

            this.y = Shipy - bullet_image.Height;
            this.x = Shipx - 10; // x and y are set by ship so we dont know the given locations only the ship gives it
            //+ bullet_image.Width + 10

            Canvas.SetLeft(bullet_image, this.x); // place and add to canvas
            Canvas.SetTop(bullet_image, this.y);
            canvas.Children.Add(bullet_image);


            Debug.WriteLine("actual width - " + bullet_image.ActualWidth + " actual height - " + bullet_image.ActualHeight);

            height = bullet_image.ActualHeight;
            weight = bullet_image.ActualWidth;
            // bullet_image.Width = weight;
            //bullet_image.Height = height;


            //bullet_timer_movement.Interval = TimeSpan.FromTicks(1); set of interval timer is being set in the game page and is not needing in the bullet
        }

        public double[] getBulletInfo()
        {
            double[] ret = { this.x, this.y, 75}; // height and weight are the same so we return the value
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
    }
}

