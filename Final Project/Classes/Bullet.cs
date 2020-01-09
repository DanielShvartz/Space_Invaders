﻿using System;
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

        public Bullet(double Shipx, double Shipy, Canvas canvas, Bullets bullet_type)
        {
            this.canvas = canvas;
            this.bullet_type = bullet_type;
            this.bullet_image = new Image();
            this.x = Shipx - 10; // x and y are set by ship so we dont know the given locations only the ship gives it


            // bullet type defines which speed, dmg and which image will be places, ship places the bullet on the canvas
            switch (bullet_type)
            {
                //user bullets - go from down to up so speed is minues
                case Bullets.Light_Shell_Default:
                    speedDy = -17; // fast but no dmg
                    damage = 0.5;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Light_Shell_Default.png"));
                    bullet_image.Width = 19;
                    bullet_image.Height = 29;
                    break;

                case Bullets.Medium_Shell:
                    speedDy = -11; // medium speed but good dmg
                    damage = 1;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Medium_Shell.png"));
                    bullet_image.Width = 22;
                    bullet_image.Height = 31;
                    break;

                case Bullets.Heavy_Shell:
                    speedDy = -7;
                    damage = 1.5; // slow but big dmg
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Heavy_Shell.png"));
                    bullet_image.Width = 31;
                    bullet_image.Height = 49;
                    this.x -= 5;
                    break;

                case Bullets.Sniper_Shell:
                    speedDy = -27; // fast and very high dmg
                    damage = 2.5;
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
                case Bullets.Plasma:
                    speedDy = 19;
                    damage = 2.5;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Plasma_Enemy.png"));
                    bullet_image.Width = 24;
                    bullet_image.Height = 87;
                    this.x -= 2.5;
                    break;

                case Bullets.Laser:
                    speedDy = 21; 
                    damage = 3.5;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/SpaceShip/Bullets/Laser_Enemy.png"));
                    bullet_image.Width = 15;
                    bullet_image.Height = 92;
                    this.x += 2.5;
                    break;

                case Bullets.Enemy_Shell:
                    speedDy = 17;
                    damage = 1.5;
                    bullet_image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Enemys/Enemy_Bullet.png"));
                    bullet_image.Width = 21;
                    bullet_image.Height = 96;
                    break;
            }

            this.y = Shipy - bullet_image.Height;
            //+ bullet_image.Width + 10

            Canvas.SetLeft(bullet_image, this.x); // place and add to canvas
            Canvas.SetTop(bullet_image, this.y);
            canvas.Children.Add(bullet_image);


            //Debug.WriteLine("actual width - " + bullet_image.ActualWidth + " actual height - " + bullet_image.ActualHeight);

            height = bullet_image.ActualHeight;
            width = bullet_image.ActualWidth;
            //bullet_timer_movement.Interval = TimeSpan.FromTicks(1); set of interval timer is being set in the game page and is not needing in the bullet
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

