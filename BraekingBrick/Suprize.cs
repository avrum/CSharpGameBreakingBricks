using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Windows.Forms;

namespace BraekingBrick
{
    public class Suprize
    {
        public int chance , good;
        public Boolean goodSuprize = true;
        public int typeOfSuprize=1;
        public String startupPath = System.IO.Directory.GetCurrentDirectory();
        public Graphics g;
        public Point Location;
        public String goodOrBad = "";
        public Form1 form1;

        public Suprize(int chance, int good , Point Location , Form1 form1)
        {
            this.Location = Location;
            this.chance = chance;
            this.good = good;
            this.form1 = form1;
        }


        public void drawSuprize(Graphics g)
        {
            if (Location.Y < form1.Height -20)
            {
                Image suprizeImage = Image.FromFile(startupPath + "//suprize" + goodOrBad + typeOfSuprize + ".png");
                g.DrawImage(suprizeImage, Location);
                Location.Y += 4;
            }
            else
                 {
                     this.typeOfSuprize = -1;
                 }

            
        }

        public Boolean checkSuprize()
        {
            
            Random randomSuprize = new Random();
            int randomRange = randomSuprize.Next(0, 100);
            if (randomRange <= chance) // you should drop a suprize
            {
                randomRange = randomSuprize.Next(0, 100);
                if (randomRange <= good) // you should drop a Good suprize
                {
                    goodSuprize = true;
                }
                else // you should drop a Bad suprize
                {
                    goodSuprize = false;
                }
                if (goodSuprize) goodOrBad = "good";
                else goodOrBad = "bad";
                typeOfSuprize = (new Random()).Next(1, 3);
                return true;
            }
            else return false;
        }

        public void updatePlayerStat(Player player)
        {
            if (goodSuprize) // good suprize
            {
                if(typeOfSuprize == 1)
                {
                   // player.life += 1 ;
                    int numberOfcurrectBalls = form1.balls.Count;
                    for (int i = 0; i<numberOfcurrectBalls ; i++ )
                    {
                        Ball newBall = new Ball();
                        newBall.centerOfBall = form1.balls.ElementAt(i).centerOfBall;
                        newBall.xSpeed = -1 * form1.balls.ElementAt(i).xSpeed;
                        form1.balls.Add(newBall);
                    }
                }
                else if (typeOfSuprize == 2)
                {
                    player.width = (int)Math.Round(player.width *1.5);
                }
                player.score += 100;
            }
            else // bad suprize
            {
                if (typeOfSuprize == 1)
                {
                    player.life -= 1;
                }
                else if (typeOfSuprize == 2)
                {
                    player.width = (int)Math.Round(player.width * 0.5);
                }
                player.score -= 100;
            }
        }


    }
}
