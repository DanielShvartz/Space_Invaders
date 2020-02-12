using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.SQLite
{
    static class DataAccessLayer
    {
        public static void CreateDataBase() //creates the db
        {
            var sqlpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                "LevelsDB.sqlite"); // get the path of the db
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlpath))
            {
                conn.CreateTable<Level>(); // and create table
            }
        }

        public static void Insert(Level level) // add value - adds new level
        {
            var sqlpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                "LevelsDB.sqlite"); // get folder path
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlpath))
            {
                conn.RunInTransaction(() =>
                conn.Insert(level)); // add level
            };
        }
        public static Level SelectByNum(int num) // select level row by level num
        {
            var sqlpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                            "LevelsDB.sqlite");
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlpath))
            {
                Level level = conn.Query<Level>("select * from Level where Currentlevel=" + num).FirstOrDefault();
                return level; // return currect level by selected num
            }
        }
        public static void DeleteAll() // deletes the db - good before creation - to check that doesnt exist
        {
            var sqlpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                            "LevelsDB.sqlite");
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlpath))
            {
                conn.DropTable<Level>();
                conn.CreateTable<Level>();
                conn.Dispose();
                conn.Close();
            }
        }

        public static List<Level> GetAllLevels() // to know how much levels we have
        {
            List<Level> levels;
            var sqlpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                           "LevelsDB.sqlite");
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), sqlpath))
            {
                levels = (from level in conn.Table<Level>()
                          select level).ToList();
            }
            return levels;
        }
    }
}
