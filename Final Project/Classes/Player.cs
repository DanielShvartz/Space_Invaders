using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Final_Project.Classes
{
    public enum SpaceShips
    {
        Default = 0, Level1, Level2, Level3
    };
    class Player : Base
    {
        // when you create a new player you give him the location of the image to be initialized
        // because the x and y is changing different in the enemy and in the player 
        //we cant init it in the base only in the players class

        public int SpaceShip_Level;
        static Random rnd = new Random();
        //spaceship speed is also declered by spaceship level - higher level - stronger and faster
        public Player(double dxSpeed, Canvas canvas, string imageLocation, int SpaceShip_Level) : base(dxSpeed + SpaceShip_Level * 4, canvas, imageLocation)
        {
            switch(SpaceShip_Level)
            {
                case (int)SpaceShips.Default:
                    this.image.Width = 75; // after image got initalized
                    this.image.Height = 75;
                    break;
                case (int)SpaceShips.Level1:
                    this.image.Width = 80; // after image got initalized
                    this.image.Height = 80;
                    break;
                case (int)SpaceShips.Level2:
                    this.image.Width = 85; // after image got initalized
                    this.image.Height = 85;
                    break;
                case (int)SpaceShips.Level3:
                    this.image.Width = 90; // after image got initalized
                    this.image.Height = 90;
                    break;
            }
                   

            this.x = (canvas.ActualWidth - image.ActualWidth)/2; // - to place at the middle 885
            this.y = (canvas.ActualHeight - image.ActualHeight);// - to place at  button - 930  

            Canvas.SetLeft(this.image, this.x); // place and add to canvas
            Canvas.SetTop(this.image, this.y);
            canvas.Children.Add(this.image);
            this.SpaceShip_Level = SpaceShip_Level;

        }

        public override void Move(string direction)
        {
            if(direction == "Left")
            {
                if (x - dx < 0) // if it wants to move out the border we block him
                    return;
                else
                {
                    x -= dx; // we move him and update him
                    Canvas.SetLeft(this.image, x);
                }
            }
            else if (direction == "Right")
            {
                if (x + dx > canvas.ActualWidth - this.image.ActualWidth)
                    return; // block
                else
                {
                    x += dx;
                    Canvas.SetLeft(this.image, x); // move
                }
            }
        }
    }
}
