using System;
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

        public int player_SpaceShip_Level; //need to know which spaceship and bullet he has
        public Bullets player_Bullet; // to upgrade its bullet type

        public List<Image> Shields_Images;
        public List<Image> Shields_hp_Images;
        public List<double> ShieldHp; // init array of 3 shield hp
        // there is no need to pass ShieldRectangles because it stays the same and we doesnt need to pass him

        public string username; // to save username and password and not ask twice
        public string password;
        public bool isExitingInDB; // added is existing in db to save the username and password and not access the db twice to check if he is existing or not

        public Data(int level, int coins, double player_HitPoints, int player_SpaceShip_Level, Bullets player_Bullet, List<Image> shields_Images, List<Image> shields_hp_Images, List<double> shieldHp, string username, string password, bool isExistingInDB)
        {
            Level = level;
            this.coins = coins;
            this.player_HitPoints = player_HitPoints;

            this.player_SpaceShip_Level = player_SpaceShip_Level;
            this.player_Bullet = player_Bullet;

            Shields_Images = shields_Images;
            Shields_hp_Images = shields_hp_Images;
            ShieldHp = shieldHp;

            this.username = username;
            this.password = password;
            this.isExitingInDB = isExistingInDB;
        }
        public Data() { }
    }
}
