using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SpaceInvaders_Service
{
    public class SavePlayer_Service : ISavePlayer_Service 
    {
        public bool IsPlayerExists(string username, string password)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection connection = new SqlConnection(ConnectionString);
            //delete by id
            string query = string.Format("SELECT * FROM Player_Table, Where Username = '{0}' AND Password = '{1}' ", username, password);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader SqlReader = cmd.ExecuteReader();

            bool ans;
            if (SqlReader.HasRows) // if he have rows - he exist
                ans = true; // return exist
            else // if he doesnt have rows he doesnt exists
                ans = false; // doesnt exists
            connection.Close();
            return ans; // no need to check we just return true
        }

        //he wants to finish the game - we add him to table or update him
        public bool SavePlayer(Player player)
        {
            //we want to know where is our server so we search for connection string
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            // open a new sql connection
            SqlConnection connection = new SqlConnection(ConnectionString);

            //set the query by formating string - we access the table and insert data.
            string query = string.Format("INSERT INTO Player_Table (Username, Password, Current_Level, HP, Coins, SpaceShip_Level, Bullet_Level, Shield1_HP, Shield2_HP, Shield3_HP) VALUES ('{0}','{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
               player.Username, player.Password, player.Current_Level, player.HP, player.Coins, player.SpaceShip_Level, player.Bullet_Level, player.Shield1_HP, player.Shield2_HP, player.Shield3_HP);

            // intialize a sql command
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            // run the query and see what the result is
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            if (result != 0) // we check if we successed to run the operation.
                return true; // if its 0 we cannot run it
            else
                return false;
        }

        //if he decides in the end of the level to finish - we save him
        public bool UpdatePlayer(Player player)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection connection = new SqlConnection(ConnectionString);
            //delete by id
            string query = string.Format("UPDATE Player_Table SET Current_Level = {0}, HP = {1}, Coins = {2}, SpaceShip_Level = {3}, Bullet_Level = {4}, Shield1_HP = {5}, Shield2_HP = {6}, Shield3_HP = {7} WHERE Username = {8} ",
                player.Current_Level, player.HP, player.Coins, player.SpaceShip_Level, player.Bullet_Level, player.Shield1_HP, player.Shield2_HP, player.Shield3_HP, player.Username);
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return true; // no need to check we just return true
        }

        // if we laod a player it means he exists - so we load the player
        public Player LoadPlayer(string username, string password)
        {
            Player player = new Player();

            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection connection = new SqlConnection(ConnectionString);
            string query = string.Format("SELECT * FROM Player_Table, Where Username = '{0}' AND Password = '{1}' ", username, password);
            SqlCommand cmd = new SqlCommand(query, connection); // we get the wanted player
            connection.Open();
            SqlDataReader SqlReader = cmd.ExecuteReader();

            while (SqlReader.Read()) // we read until we got nothing in the table
            {
                player = new Player(); // we load the player info from the db

                player.Username = username;
                player.Password = password;

                player.Current_Level = int.Parse(SqlReader["Current_Level"].ToString());
                player.HP = int.Parse(SqlReader["HP"].ToString());
                player.Coins = int.Parse(SqlReader["Coins"].ToString());
                player.SpaceShip_Level = int.Parse(SqlReader["SpaceShip_Level"].ToString());
                player.Bullet_Level = int.Parse(SqlReader["Bullet_Level"].ToString());
                player.Shield1_HP = int.Parse(SqlReader["Shield1_HP"].ToString());
                player.Shield2_HP = int.Parse(SqlReader["Shield2_HP"].ToString());
                player.Shield3_HP = int.Parse(SqlReader["Shield3_HP"].ToString());
                player.Shield1_Image = int.Parse(SqlReader["Shield1_Image"].ToString());
                player.Shield2_Image = int.Parse(SqlReader["Shield2_Image"].ToString());
                player.Shield3_Image = int.Parse(SqlReader["Shield3_Image"].ToString());
            }
            connection.Close(); // in end we close the connection.
            return player;

        }
    }
}
