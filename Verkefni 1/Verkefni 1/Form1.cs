using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Verkefni_1
{
    public partial class Form1 : Form
    {
        List<Ball> balls = new List<Ball>();
        private Random generator = new Random();
        public int counter = 0;
        Color[] colors = new Color[10]{Color.Blue, Color.Red, Color.Tomato, Color.Yellow, Color.HotPink, Color.Honeydew, Color.IndianRed, Color.LightGoldenrodYellow, Color.SteelBlue, Color.Gold};

        private void paint()
        {
            while (true)
            {
                Thread.Sleep(40);
                BallPanel.Invalidate();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void BallPanel_Paint(object sender, PaintEventArgs e)
        {
            if (balls != null)
            {
                Graphics gr = e.Graphics;
                foreach (var ball in balls)
                {
                    SolidBrush sb = new SolidBrush(ball.Color);
                    gr.FillEllipse(sb, ball.X, ball.Y, ball.Radius, ball.Radius);   
                }
            }
        }

        private void BallPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Ball ball = new Ball(e.X, e.Y, 20, colors[generator.Next(10)], generator.Next(10), generator.Next(10), BallPanel.Size.Width, BallPanel.Size.Height);
            balls.Add(ball);

            Thread baller = new Thread(new ThreadStart(ball.Run));
            baller.Start();
            counter++;
            ballCounter.Text = counter.ToString();

            if (balls.Any()) 
            {
                Thread painter = new Thread(new ThreadStart(paint));
                painter.Start();
            }
            BallPanel.Invalidate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
