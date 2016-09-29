using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace BraekingBrick
{
    public class Ball
    {

        public double xSpeed, ySpeed;
         public int   size;
        private int power;
        public SolidBrush brush;
        public Point centerOfBall;

        private String startupPath = System.IO.Directory.GetCurrentDirectory();
        public Image ballImage;
        public float rotation;
        
         public   Ball()
            {
                centerOfBall = new Point();
                brush = new SolidBrush(Color.Blue);
                ySpeed = 2;
                xSpeed = 0;
                rotation = 0;
                size = 20;
                Image ballImage = Image.FromFile(startupPath + "//ball.png");
            }

         public void drawBall(Graphics g)
         {
         //    g.FillEllipse(brush, centerOfBall.X - size/2, centerOfBall.Y -size/2, size, size);

            //Rotate Ball
             String ballNumber = "";
             if (Math.Abs(xSpeed) >= 8) ballNumber = "2";
             ballImage = Image.FromFile(startupPath + "//ball" +ballNumber + ".png");
             rotation = rotation + (float)xSpeed*2;
             ballImage = RotateImage(ballImage, rotation);
             //Draw Ball
             g.DrawImage(ballImage, new Point(centerOfBall.X - size / 2, centerOfBall.Y - size / 2));

         }

         public Boolean checkUnderBorder(Form1 form)
         {
             if (centerOfBall.Y + size*2  >= form.Size.Height) return true;
             else return false;
         }

        public Boolean tuchUpperWall()
        {
            if (centerOfBall.Y -size/2 <= 0)
            {
                return true;
            }
            else return false;
        }

        public Boolean tuchWalls(Form1 form)
        {
            if (((centerOfBall.X - size / 2) <= 0) || (centerOfBall.X + size) >= form.Size.Width)
            {
                return true;
            }
            else return false;

        }


        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }
    }
}
