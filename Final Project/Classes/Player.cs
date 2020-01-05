using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Final_Project.Classes
{
    class Player : Base
    {
        // when you create a new player you give him the location of the image to be initialized
        // because the x and y is changing different in the enemy and in the player 
        //we cant init it in the base only in the players class

        public Player(double dxSpeed, Canvas canvas, string imageLocation) : base(dxSpeed, canvas, imageLocation)
        {
            this.image.Width = 75; // after image got initalized
            this.image.Height = 75;

            this.x = (canvas.ActualWidth - image.ActualWidth)/2; // - to place at the middle 885
            this.y = (canvas.ActualHeight - image.ActualHeight);// - to place at  button - 930  

            Canvas.SetLeft(this.image, this.x); // place and add to canvas
            Canvas.SetTop(this.image, this.y);
            canvas.Children.Add(this.image);
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
