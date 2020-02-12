using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.SQLite
{
    class Level
    {
        //bullet and spaceship speed updates in the shop by buyment and not by leveling up
        [PrimaryKey]
        public int Currentlevel { get; set; } // affects enemy: dx, dx - faster, hp - inc, bullet dmg - inc
        /*
         enemy dx and dy are set random so no need to pass them, enemyHP is set by current level
         no need to send also the playerHP because the player hp gets inc each level depands on his current HP
         and doesnt reset and then inc, its just inc so we dont do it each level 
         */
        
        public Level()
        {

        }
        //enemy dx dy and hitpoints
        public Level(int Currentlevel)
        {
            this.Currentlevel = Currentlevel;
        }
        
    }
}
