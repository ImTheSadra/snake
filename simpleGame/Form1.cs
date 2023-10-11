using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace simpleGame
{
    public partial class gameForm : Form
    {
        public Timer timer = new Timer();
        public Timer moveTimer;
        public int width = 700;
        public int height = 700;
        public int x, y, tileSize, size,score;
        public int[] move = new int[2];
        public List<int[]> olds = new List<int[]>();
        public int foodX, foodY;

        public gameForm()
        {
            config();
            run();
        }

        private void config()
        {
            this.Text = "snake game - made by sadra (https://github.com/SadraZ3R0)";
            this.ClientSize = new Size(width, height);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            move[0] = 1;
            move[1] = 0;

            this.KeyDown += checkKeys;

            size = 50;
            tileSize = width / size;
            x = tileSize / 2;
            y = tileSize / 2;

            var rand = new Random();

            foodX = rand.Next(0, tileSize);
            foodY = rand.Next(0, tileSize);
        }

        private void checkKeys(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString().ToLower();
            switch (key)
            {
                case "right":
                    move[0] = 1;
                    move[1] = 0;
                    break;

                case "left":
                    move[0] = -1;
                    move[1] = 0;
                    break;

                case "up":
                    move[0] = 0;
                    move[1] = -1;
                    break;

                case "down":
                    move[0] = 0;
                    move[1] = 1;
                    break;

                default:
                    break;
            }
        }

        private void run()
        {
            timer.Interval = 16;
            timer.Tick += loop;
            timer.Start();

            moveTimer = new Timer();
            moveTimer.Interval = 500;
            moveTimer.Tick += movePlayer;
            moveTimer.Start();
        }

        private void movePlayer(object sender, EventArgs e)
        {
            int[] pos = new int[2];
            pos[0] = x;
            pos[1] = y;

            this.olds.Add(pos);
            x += move[0];
            y += move[1];

            if (foodX == x){ if (foodY == y) {
                    score += 1;
                    var rand = new Random();

                    foodX = rand.Next(0, tileSize);
                    foodY = rand.Next(0, tileSize);
                } 
            }

            if (x > tileSize | x < 0 | y > tileSize | y < 0)
            {
                x = tileSize / 2;
                y = tileSize / 2;

                var rand = new Random();

                foodX = rand.Next(0, tileSize);
                foodY = rand.Next(0, tileSize);

                score = 0;
            }
        }

        private void loop(object sender, EventArgs e)
        {
            Update();
            render();
        }

        private void render()
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(Color.FromArgb(51,51,51));

                int tx = (int)x * size;
                int ty = (int)y * size;

                Rectangle player = new Rectangle(tx, ty, size, size);
                g.FillRectangle(Brushes.Green, player);

                for (int i = 0; i < this.score; i++)
                {
                    int index = (int)this.olds.Count - i - 1;
                    Rectangle rect = new Rectangle(olds[index][0] * size, olds[index][1] * size, size, size);
                    g.FillRectangle(Brushes.White, rect);
                }

                g.FillEllipse(
                    Brushes.Red,
                    foodX*size, foodY*size, size, size
                );
            }
        }
    }
}
