using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace BraekingBrick
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Player player = new Player(100 , 3);
        public Collection<Ball> balls = new Collection<Ball>();
        private Brick[] bricks ;
        //public ArrayList suprizes;
        public Collection<Suprize> suprizes;
        //public LinkedList<Object> suprizeList;

        public Form1()
        {
            InitializeComponent();
            player.recArray[0].Location = new Point(this.Size.Width/2 - player.width/2, this.Size.Height - 60);
            Ball firstBall = new Ball();
            firstBall.centerOfBall = new Point(this.Size.Width / 2 - 10, this.Size.Height / 2 - 10);
            balls.Add(firstBall);
            notifications.Text = "";
            bricks = new Brick[32];
            //Random randomColor = new Random();
            for(int j = 1 ; j<5 ; j++)
            for (int i = 0; i < 8; i++)
            {
              //  int random = randomColor.Next(1, 4);
                bricks[i + 8 * (j - 1)] = new Brick(i * 81 + 10, j * 16, 5-j , 99 , 50 , this);
            }
          this.suprizes = new Collection<Suprize>();
          //LinkedList<Object> suprizeList = new LinkedList<Object>();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
          //  label1.Text = "Mouse X is: " + e.X;
            Point newLoc = new Point(e.X - player.width / 2, this.Size.Height - 60);

            player.recArray[0].Location= newLoc;
            //mouse right border
            if (e.X > this.Width-25)
            {
                Point mouseLoc = new Point(this.Location.X+20, Cursor.Position.Y);
                Cursor.Position = mouseLoc;
            }
            //mouse left border
            if (e.X < 10)
            {
                Point mouseLoc = new Point(this.Location.X - 30 +this.Width, Cursor.Position.Y);
                Cursor.Position = mouseLoc;
            }
            

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            player.drawPlayer(g);
            foreach (Ball ball in balls)
            {
                ball.drawBall(g);
            }
            for (int i = 0; i < bricks.Length; i++ )
                bricks[i].drawBrick(g);
            for (int i = 0; i < player.life; i++)
            {
                String startupPath = System.IO.Directory.GetCurrentDirectory();
                Image ballImage = Image.FromFile(startupPath + "//ball.png");
                g.DrawImage(ballImage, new Point(10 + 22 *i, 400));
            }
           // draw current suprizes
            foreach (Suprize suprize in suprizes)
            {
               
            suprize.drawSuprize(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           //3
            ((Timer)sender).Interval = 3;
            //MessageBox.Show("ff");
            foreach (Ball ball in balls)
            {
            ball.centerOfBall.Y += (int)Math.Round(ball.ySpeed);
            ball.centerOfBall.X += (int)Math.Round(ball.xSpeed);
            checkBallBorders(ball);
            checkHitBrick(ball);
            if(checkUnderBorder(ball)) break;
            }
            
          
            player.tuchSuprise(suprizes);
          //  checkIfPause();
            scoreLabel.Text = "Score : " + player.score;

             this.Invalidate();
        }

        private Boolean checkUnderBorder(Ball ball)
        {
            if (ball.checkUnderBorder(this))
            {
                player.life -= 1;
                ball.xSpeed = 0;
                ball.centerOfBall = new Point(this.Size.Width / 2 - 10, this.Size.Height / 2 - 10);
                timer1.Enabled = false;
                label2.Show();
                if (player.life >= 0)
                {
                    label2.Text = "Press Mouse To Start \rYour have " + player.life + " balls Left";
                }
                else
                {
                    label2.Text = "Press Mouse To Start \rYour have Lost! :-(";
                    player.life = 3;
                    player.score = 0;
                }
                Ball firstBall = balls.ElementAt(0);
                balls.Clear();
                balls.Add(firstBall);
                return true;
            }
            else return false;
        }

        private void checkBallBorders(Ball ball)
        {
            if ( player.tuchBall(ball) || ball.tuchUpperWall())
            {
                // ball tuuch player
                ball.ySpeed = ball.ySpeed * (-1);
                label1.Text = "Speed: " + Math.Abs(ball.xSpeed).ToString();
                if (Math.Abs(ball.xSpeed) >= 8)
                {
                    notifications.Text = "Ultimate Speed!!!";
                    ball.brush.Color = Color.Red;
                }
                else if (Math.Abs(ball.xSpeed) < 8 && Math.Abs(ball.xSpeed) >= 5)
                {
                    notifications.Text = "High Speed!";
                    ball.brush.Color = Color.Orange;
                }
                else if (Math.Abs(ball.xSpeed) < 5 && Math.Abs(ball.xSpeed) >= 2)
                {

                    ball.brush.Color = Color.Blue;
                    notifications.Text = "Normal Speed!";
                }
                else
                {
                    ball.brush.Color = Color.Blue;
                    notifications.Text = "Slow Speed :)";
                    
                }
            }
            if(ball.tuchWalls(this))
            {
                ball.xSpeed = ball.xSpeed * (-1);
            }
        }

        private void checkHitBrick(Ball ball)
        {
            foreach (Brick brick in bricks)
            {
              //  MessageBox.Show("fff");
                //g.FillRectangle(new SolidBrush(Color.Aqua), new Rectangle(20, 0, 50, 20));
                if (brick.checkIfBallHit(ball , player , g , this) && Math.Abs(ball.xSpeed)< 8)
                {
                    ball.ySpeed = ball.ySpeed * (-1);
                }

            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Enabled=true;
            label2.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void checkIfPause()
        {
            //if (Cursor.Position.X < 0 || Cursor.Position.X > form1.Size.Width ||
            //    Cursor.Position.Y < 0 || Cursor.Position.Y > this.Size.Height)
            //{
            //    timer1.Enabled = false;
            //    label2.Show();
            //    label2.Text = "Pause...\nClick Here To Continue";
                

            //}
            MessageBox.Show("fff");
        }

    }
}
