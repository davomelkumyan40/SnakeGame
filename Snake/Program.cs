using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            snake.Setup();
            snake.Start();
        }
    }

    class Snake : ISnakeable
    {
        private int snakeX;
        private int snakeY;
        private int fruitX;
        private int fruitY;
        private Keys direction = Keys.Stop;
        private int Heigth { get => 20; }
        private int Width { get => 20; }
        private char arenaChar = '#';
        private char snake = '@';
        private bool gameOver;
        private int score;
        private char key;

        public void Start()
        {
            while (gameOver != true)
            {
                //Setup();
                Picture();
                Input();
                Logic();
                if (Win())
                {
                    break;
                }
                if (gameOver)
                    Gameover();
            }
        }

        public void Setup()
        {
            gameOver = false;
            Random rnd = new Random();
            snakeX = Width / 2;
            snakeY = Heigth / 2;
            fruitX = rnd.Next(1, Width - 1);
            fruitY = rnd.Next(1, Heigth - 1);
            score = 0;
        }

        public void Picture()
        {
            Console.Clear();
            
            for (int x = 0; x < Width; x++)
            {
                Console.Write(arenaChar);
            }
            Console.WriteLine();
            for (int y = 0; y < Heigth; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || x == Width - 1)
                    {
                        Console.Write(arenaChar);
                    }
                    else if (x == snakeX && y == snakeY)
                    {
                        Console.Write(snake);
                    }
                    else if (x == fruitX && y == fruitY)
                    {
                        Console.Write("0");
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            for (int x = 0; x < Width; x++)
            {
                Console.Write(arenaChar);
            }
            Thread.Sleep(50);
        }

        public void Input()
        {
            char key = this.key;
            if (Console.KeyAvailable)
            {
                this.key = key = Console.ReadKey().KeyChar;
                KeyCheck(key);
            }
            
        }

        public void KeyCheck(char key)
        {
            switch (key)
            {
                case 'w':
                    direction = Keys.Up;
                    break;
                case 'a':
                    direction = Keys.Left;
                    break;
                case 'd':
                    direction = Keys.Right;
                    break;
                case 's':
                    direction = Keys.Down;
                    break;
                default:
                    direction = Keys.Stop;
                    break;
            }
        }

        public void Logic()
        {
            Random r = new Random();
            switch (direction)
            {
                case Keys.Up:
                    snakeY--;
                    break;
                case Keys.Down:
                    snakeY++;
                    break;
                case Keys.Right:
                    snakeX++;
                    break;
                case Keys.Left:
                    snakeX--;
                    break;
            }
            if (snakeX == fruitX && snakeY == fruitY)
            {
                score++;
                fruitX = r.Next(1, Width - 1);
                fruitY = r.Next(1, Heigth - 1);
            }
            if (snakeX < 0 || snakeY < 0 || snakeX > Width - 1 || snakeY > Heigth - 1)
            {
                gameOver = true;
            }
        }

        public bool Win()
        {
            if (score == 10)
            {
                return true;
            }
            return false;
        }

        public void Gameover()
        {
            Console.Clear();
            Console.WriteLine(new string(arenaChar, 20));
            for (int x = 0; x < Width; x++)
            {
                if (x == 0 || x == Width - 1)
                {
                    Console.Write(arenaChar);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine(new string(arenaChar, 20));
            Console.WriteLine("Game Over");
        }
    }
}
