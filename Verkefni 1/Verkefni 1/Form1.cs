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
        // Colors for randomization
        Color[] colors = new Color[10]{Color.Blue, Color.Red, Color.Tomato, Color.Yellow, Color.HotPink, Color.Purple, Color.IndianRed, Color.LimeGreen, Color.SteelBlue, Color.Gold};

        private void paint()
        {
            while (true)
            {
                Thread.Sleep(16);
                /*
                * Faster than BallPanel.Invalidate();
                */
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                });
            }
        }

        private void addBalls(int amount, MouseEventArgs e)
        {
            for (int i = 0; i < nud_AmountOfBalls.Value; i++)
            {
                // Create ball and add to the ball list for drawing.
                // The ball starts at mouse coordinates, and has random speed, size and color.
                // Then start the ball movement calculations in other thread
                Ball ball = new Ball(e.X, e.Y, generator.Next(30, 60), colors[generator.Next(0, 10)], generator.Next(2, 20), generator.Next(2, 20), BallPanel.Size.Width, BallPanel.Size.Height);
                balls.Add(ball);

                Thread baller = new Thread(new ThreadStart(ball.Run));
                baller.Start();
                // Count the balls
                counter++;
                this.Invoke((MethodInvoker)delegate
                {
                    ballCounter.Text = counter.ToString();
                });
            }
        }

        public Form1()
        {
            InitializeComponent();
            Thread painter = new Thread(new ThreadStart(paint));
            painter.Start();
        }

        private void BallPanel_Paint(object sender, PaintEventArgs e)
        {
            // Only draw when there are some balls
            Graphics gr = e.Graphics;
            foreach (var ball in balls)
            {
                SolidBrush sb = new SolidBrush(ball.Color);
                gr.FillEllipse(sb, ball.X, ball.Y, ball.Radius, ball.Radius);
            }
        }

        private void BallPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Thread ballAdder = new Thread(() => addBalls((int)nud_AmountOfBalls.Value, e));
            ballAdder.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Killing it with fire
            Environment.Exit(Environment.ExitCode);
        }
    }
}
