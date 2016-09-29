using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BraekingBrick
{
    public class Player 

    {
        public int width;
        private int xLocation;
        private SolidBrush brush;
        public Rectangle[] recArray;
        public int life;
        public int score = 0;


        public Player(int newWidth , int life)
        {
            this.width = newWidth;
            brush = new SolidBrush(Color.Black);
            recArray = new Rectangle[1];
            recArray[0] = new Rectangle(20, 0, width, 20);
            this.life = life;


        }

        public void drawPlayer(Graphics g)
        {
            recArray[0] = new Rectangle(recArray[0].X, recArray[0].Y, width, 20);
            foreach (Rectangle rec in recArray)
            {
                g.FillRectangle(brush,rec);
            }
        }


        public Boolean tuchBall(Ball ball)
        {

           Point centerOfBall = ball.centerOfBall;

            if (centerOfBall.X >= recArray[0].X && centerOfBall.X <= (recArray[0].X + recArray[0].Width)
                && centerOfBall.Y +ball.size/2 >= recArray[0].Y && centerOfBall.Y <= (recArray[0].Y + recArray[0].Height))
            {
                // ball hit player
                //check where it hit to set X vilosoti
                int gap = centerOfBall.X - recArray[0].X - recArray[0].Width/2; // (should be between -50  to +50)
                double precent = ((double)gap / (double)(recArray[0].Width/2))*1.5;
                ball.xSpeed += precent;
                return true;
            }
            else return false;

        }

        public void tuchSuprise(Collection<Suprize> suprizes)
        {
            Collection<Suprize> suprizeToDelete = new Collection<Suprize>();
            foreach (Suprize suprize in suprizes)
            {
                Point suprizeLocation = suprize.Location;
                // if suprize hit player
                if (suprizeLocation.X >= recArray[0].X && suprizeLocation.X <= (recArray[0].X + recArray[0].Width)
                    && suprizeLocation.Y +10 >= recArray[0].Y && suprizeLocation.Y <= (recArray[0].Y + recArray[0].Height))
                {
                    // suprize hit player
                    suprize.updatePlayerStat(this);
                    //add to remove suprize from collection
                    suprizeToDelete.Add(suprize);
                }
                else if(suprize.typeOfSuprize == -1)  // if suprize is out of screen - delete it
                {
                    //add to remove suprize from collection
                    suprizeToDelete.Add(suprize);
                }
            }
            //remove used suprizes from collection
            foreach (Suprize suprize in suprizeToDelete)
            {
                suprizes.Remove(suprize);
            }
        }


    }
}
