using Snake.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        bool crash()
        {
            for (int i = 0; i < tailPieces.Count; i++)
            {
                if (tailPieces[i].Bounds.IntersectsWith(Snake.Bounds))
                {
                    return true;
                }
            }
            return false;
        }

        List<PictureBox> tailPieces = new List<PictureBox>();
        Random rng = new Random();
        int i = 0;
        string direction = "right";

        const int Size_Snake = 32;
        const int Size_Food = 20;
        int points = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(Size_Snake * 20, Size_Snake * 15);
            Snake.Size = new Size(Size_Snake, Size_Snake);
            Food.Size = new Size(Size_Food, Size_Food);

            Food.Location = new Point(rng.Next(0, ClientSize.Width - Size_Food), rng.Next(0, ClientSize.Height - Size_Food));
            Snake.Location = new Point(0, 0);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')
            {
                direction = "left";
            }
            else if (e.KeyChar == 'd')
            {
                direction = "right";
            }
            else if (e.KeyChar == 'w')
            {
                direction = "up";
            }
            else if (e.KeyChar == 's')
            {
                direction = "down";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            moveTail();

            if (direction == "right")
            {
                Snake.Left += Size_Snake;
            }
            else if (direction == "left")
            {
                Snake.Left -= Size_Snake;
            }
            else if (direction == "up")
            {
                Snake.Top -= Size_Snake;
            }
            else if (direction == "down")
            {
                Snake.Top += Size_Snake;
            }

            if (Snake.Bounds.IntersectsWith(Food.Bounds))
            {
                Food.Location = new Point(rng.Next(0, ClientSize.Width - Size_Food), rng.Next(0, ClientSize.Height - Size_Food));
                addTail();
                points = points + 1;
                label1.Text = "Points: " + points.ToString();
                timer1.Interval = 100 - points / 2;
            }

            if (Snake.Bounds.Location.X < -25)
            {
                Snake.Left = 700;
            }
            if (Snake.Bounds.Location.X > 700)
            {
                Snake.Left = -25;
            }

            if (Snake.Bounds.Location.Y < -20)
            {
                Snake.Top = 530;
            }
            if (Snake.Bounds.Location.Y > 530)
            {
                Snake.Top = 25;
            }
        }
        void addTail()
        {
            PictureBox tailpiece = new PictureBox();
            tailpiece.Image = Resources.Snake_tailpiece;
            tailpiece.Location = Snake.Location;
            tailpiece.Size = Snake.Size;
            tailpiece.BackColor = Snake.BackColor;

            Controls.Add(tailpiece);
            tailPieces.Add(tailpiece);
        }

        void moveTail()
        {
            if (tailPieces.Count > 0) 
            {
                tailPieces[i].Location = Snake.Location;

                i = i + 1;

                if (i >= tailPieces.Count)
                {
                    i = 0;
                }
            }
        }
    }
}
