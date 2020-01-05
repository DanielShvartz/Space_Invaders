using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Final_Project.Classes
{
    class Enemy : Base
    {
        public double hitPoints;
        private double dy; // the enemys also move up and down
        
        // when you create a new player you give him the location of the image to be initialized
        // because the x and y is changing different in the enemy and in the player 
        //we cant init it in the base only in the players class
           
        public Enemy(double x, double y, double dySpeed, double dxSpeed, Canvas canvas, string imageLocation, int EnemyLevel) : base(dxSpeed, canvas, imageLocation)
        {
            switch(EnemyLevel) // maybe changed the way he gets hp, maybe from the constrcutor get a hp or from outside
            {
                case 1:
                    hitPoints = 2;
                    this.image.Width = 39;
                    this.image.Height = 104;
                    break;
                case 2:
                    hitPoints = 3;
                    this.image.Width = 56;
                    this.image.Height = 108;
                    break;
                case 3:
                    hitPoints = 4;
                    this.image.Width = 90; 
                    this.image.Height = 108;
                    break;
            }
            this.dy = dySpeed;

            this.x = x; // values are given by an array that initalizes some enemies, 
            this.y = y; // he will give values and add to aray

            Canvas.SetLeft(this.image, this.x); // place and add to canvas
            Canvas.SetTop(this.image, this.y);
            canvas.Children.Add(this.image);
        }
    }
}
