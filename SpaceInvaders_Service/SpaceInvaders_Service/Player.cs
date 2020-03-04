using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpaceInvaders_Service
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Current_Level { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int Coins { get; set; }

        [DataMember]
        public int SpaceShip_Level { get; set; }

        [DataMember]
        public int Bullet_Level { get; set; }

        [DataMember]
        public int Shield1_HP { get; set; }

        [DataMember]
        public int Shield2_HP { get; set; }

        [DataMember]
        public int Shield3_HP { get; set; }
    }
}
