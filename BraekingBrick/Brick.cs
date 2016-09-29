using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BraekingBrick
{
    public class Brick
    {
        private int power, width, height;
        public Rectangle brick;
        public Point location;
        public SolidBrush brush;
        public Suprize suprize;
        private Graphics g;


        public Brick(int x , int y , int power , int chanceOfSuprize , int chanceOfGoodSuprize , Form1 form1)
        {
            
            this.brush = new SolidBrush(Color.Bisque);
            this.location = new Point(x, y);
            this.width = 80;
            this.height = 15;
            this.brick = new Rectangle(location.X, location.Y, width, height);
            this.power = power;
            this.suprize = new Suprize(chanceOfSuprize, chanceOfGoodSuprize, new Point(location.X + width/2 - 10, location.Y) , form1);
        }

   

        private void hit(Player player)
        {
            player.score += 20 + this.power * 10;
            power = power - 1;
           
            if (checkIfExplode()) explode();
        }

        private Boolean checkIfExplode() 
        {
            if (power==0) return true;
            else return false;
          
        }

        private void explode()
        {
            
            //do samthing
         
          
           // location.Y = 900;
        }

        public void drawBrick(Graphics g)
        {
           
            if(power>0)
            {
                this.g = g;
                g.FillRectangle(brush,brick);
                if(power == 1) {brush.Color = Color.Crimson;} 
                else if (power == 2)  {brush.Color = Color.BlueViolet;}
                else if (power == 3) { brush.Color = Color.DeepSkyBlue; }
                else { brush.Color = Color.DarkKhaki; }
                g.DrawRectangle(new Pen(Color.Black), brick);
            }
            suprize.g = g;
       }

        public Boolean checkIfBallHit(Ball ball , Player player , Graphics g , Form1 form1)
        {
            Point centerOfBall = ball.centerOfBall;
            
            if (centerOfBall.X >= location.X && centerOfBall.X <= (location.X + width)
                && centerOfBall.Y + ball.size / 2 >= location.Y && centerOfBall.Y <= (location.Y + height)
                && power>0)
            {
                // ball hit brick
                if (suprize.checkSuprize() && suprize.typeOfSuprize != -1)// if should have supriz - add to collection
                {
                  //  form1.suprizes.Add(suprize);

                    form1.suprizes.Add(suprize);
                }
               // g.FillRectangle(new SolidBrush(Color.Aqua), new Rectangle(20, 0, 50, 20));
                hit(player);
                return true;
            }
            else return false;
        }

    }
}
