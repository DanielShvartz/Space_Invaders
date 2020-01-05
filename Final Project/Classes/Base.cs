using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Final_Project.Classes
{
    class Base
    {
        // coordinates
        protected double x;
        protected double y;
        // for movment of the player - he moves from left to right and doesnt need dy ( dy is needed only enemys that move up/down left/right)
        protected double dx;
        // placing images on canvas
        protected Canvas canvas;
        protected Image image;

        // the base doesnt init x and y he only sets given arguments - set arguments wil be in each class
        public Base(double dxSpeed, Canvas canvas, string imageLocaiton)
        {
            this.dx = dxSpeed; // the dx is given by levels so it needs to be initialized
            this.canvas = canvas;

            this.image = new Image();
            this.image.Source = new BitmapImage(new Uri(imageLocaiton));
        }

        virtual public void Move(string direction)
        {
            return;
        }

        public double[] getPlayerLocation()
        {
            double[] ret = { this.x, this.y };
            return ret;
        }

        public double GetWidth()
        {
            return this.image.Width;
        }

        public double GetHeight()
        {
            return this.image.Height;
        }

        public Image GetImage()
        {
            return this.image;
        }
    }
}
