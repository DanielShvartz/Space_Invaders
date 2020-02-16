﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Final_Project.Classes
{
    class Data
    {
        public int Level; // added a new parameter of level, it will be default if not chosen in the settings
        public int coins; // how much coins he has - to buy items
        public double player_HitPoints; // need to know how much he had to upgrade it

        public Image player_SpaceShip; //need to know which spaceship and bullet he has
        public Bullets player_Bullet; // to upgrade its bullet type

        List<Image> Shields_Images;
        List<Image> Shields_hp_Images;
        List<double> ShieldHp; // init array of 3 shield hp
        List<Rect> ShieldRectangles; // init array of 3 rectnagle of the shields to check hits

        public Data(int level, int coins, double player_HitPoints, Image player_SpaceShip, Bullets player_Bullet, List<Image> shields_Images, List<Image> shields_hp_Images, List<double> shieldHp, List<Rect> shieldRectangles)
        {
            Level = level;
            this.coins = coins;
            this.player_HitPoints = player_HitPoints;

            this.player_SpaceShip = player_SpaceShip;
            this.player_Bullet = player_Bullet;

            Shields_Images = shields_Images;
            Shields_hp_Images = shields_hp_Images;
            ShieldHp = shieldHp;
            ShieldRectangles = shieldRectangles;
        }
    }
}
