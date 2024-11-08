using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp17
{
    public partial class Form1 : Form
    {
        private List<Point> snake; 
        private Point food;
        private int score; 
        private int direction; 
        private Random rand; 
        private int gridSize = 20;

        public Form1()
        {
            InitializeComponent();
            snake = new List<Point> { new Point(100, 100), new Point(80, 100), new Point(60, 100) };
            rand = new Random();
            food = GenerateFood();
            score = 0;
            direction = 2; 
            gameTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();

            Graphics g = pictureBox1.CreateGraphics();

            // Отрисовка змейки
            foreach (Point p in snake)
            {
                g.FillRectangle(Brushes.Green, new Rectangle(p.X, p.Y, gridSize, gridSize));
            }

            // Отрисовка еды
            g.FillRectangle(Brushes.Red, new Rectangle(food.X, food.Y, gridSize, gridSize));
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            this.Invalidate(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (direction != 2) direction = 0;
                    break;
                case Keys.Up:
                    if (direction != 3) direction = 1;
                    break;
                case Keys.Right:
                    if (direction != 0) direction = 2;
                    break;
                case Keys.Down:
                    if (direction != 1) direction = 3;
                    break;
            }
        }

        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case 0:
                    newHead.X -= gridSize;
                    break;
                case 1:
                    newHead.Y -= gridSize;
                    break;
                case 2:
                    newHead.X += gridSize;
                    break;
                case 3:
                    newHead.Y += gridSize;
                    break;
            }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                score++;
                lblScore.Text = "Счет: " + score;
                food = GenerateFood(); 
            }
            else
            {
                snake.RemoveAt(snake.Count - 1); 
            }
        }

        private void CheckCollision()
        {
            Point head = snake[0];
            if (head.X < 0 || head.Y < 0 || head.X >= pictureBox1.Width || head.Y >= pictureBox1.Height)
            {
                GameOver();
            }
            for (int i = 1; i < snake.Count; i++)
            {
                if (head == snake[i])
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show("Игра окончена! Ваш счет: " + score);
            ResetGame();
        }

        private void ResetGame()
        {
            snake = new List<Point> { new Point(100, 100), new Point(80, 100), new Point(60, 100) };
            direction = 2;
            score = 0;
            lblScore.Text = "Счет: 0";
            food = GenerateFood();
            gameTimer.Start();
        }

        private Point GenerateFood()
        {
            int x = rand.Next(0, pictureBox1.Width / gridSize) * gridSize;
            int y = rand.Next(0, pictureBox1.Height / gridSize) * gridSize;
            return new Point(x, y);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
