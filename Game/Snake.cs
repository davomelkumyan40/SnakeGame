using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    class Snake : ISnake
    {

        public Snake()
        {
            Setup();
        }

        private int snakeX;
        private int snakeY;
        private int fruitX;
        private int fruitY;
        private int[] tailX = new int[10];
        private int[] tailY = new int[10];
        private int tailCount = 0;
        private Keys direction = Keys.Stop;
        private int Heigth { get => 20; }
        private int Width { get => 40; }
        private char arenaChar = '■';
        private char snake = '0';
        private char snakeTail = 'o';

        private bool gameOver;
        private int score;
        private char key;

        public void Start()
        {
            while (gameOver != true)
            {
                Drawing();
                Input();
                Logic();
                if (Win())
                {
                    EndGame();
                    break;
                }
                if (gameOver)
                    EndGame();
            }
        }

        public void Setup()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Snake Game";
            Console.WindowHeight = 30;
            Console.WindowWidth = 60;
            gameOver = false;
            Random rnd = new Random();
            snakeX = Width / 2;
            snakeY = Heigth / 2;
            fruitX = rnd.Next(1, Width - 1);
            fruitY = rnd.Next(1, Heigth - 1);
            score = 0;
            tailX = new int[10];
            tailY = new int[10];
            tailCount = 0;
        }

        public void Drawing()
        {
            Console.Clear();

            for (int x = 0; x < Width; x++)
            {
                Console.Write(arenaChar);
            }
            Console.Write($"{new string(' ', 5)}Score:  {score}");
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
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(snake);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (x == fruitX && y == fruitY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        bool noDraw = false;
                        for (int i = 0; i < tailCount; i++)
                        {
                            if (tailX[i] == x && tailY[i] == y)
                            {
                                Console.Write(snakeTail);
                                noDraw = true;
                            }
                        }
                        if (!noDraw)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(' ');
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                }
                Console.WriteLine();
            }

            for (int x = 0; x < Width; x++)
            {
                Console.Write(arenaChar);
            }
            Thread.Sleep(100);
        }

        public void Input()
        {
            if (Console.KeyAvailable)
            {
                this.key = Console.ReadKey().KeyChar;
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
            int prevX = tailX[0];
            int prevY = tailY[0];
            int prev2X, prev2Y;
            tailX[0] = snakeX;
            tailY[0] = snakeY;
            for (int i = 1; i < tailCount; i++)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevX;
                tailY[i] = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }

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
                score += 10;
                new Thread(Beep).Start();
                fruitX = r.Next(1, Width - 1);
                fruitY = r.Next(1, Heigth - 1);
                tailCount++;
            }
            if (snakeX < 1 || snakeY < 0 || snakeX > Width - 2 || snakeY > Heigth - 1)
            {
                gameOver = true;
            }
            for (int i = 1; i < tailX.Length; i++)
            {
                if (snakeX == tailX[i] && snakeY == tailY[i])
                {
                    gameOver = true;
                }
            }
        }

        private void Beep() => Console.Beep();

        public bool Win()
        {
            if (score >= 100)
            {
                return true;
            }
            return false;
        }

        public void EndGame()
        {
            Console.Clear();
            Console.WriteLine(new string(arenaChar, 40));
            string action = string.Empty;
            if (!gameOver)
                action = "Win";
            else
                action = "loose";

            for (int y = 0; y < Heigth; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x == 0 || x == Width - 1)
                    {
                        Console.Write(arenaChar);
                    }
                    else if (x == 8 && y == Heigth / 2 - 2)
                    {
                        Console.Write($"You {action}. Your Score : {score}");
                        if (score == 0)
                        {
                            x += 24;
                        }
                        else if (score > 0 && score < 99)
                        {
                            x += 25;
                        }
                        else
                        {
                            x += 24;
                        }
                    }
                    else if (x == 8 && y == Heigth / 2)
                    {
                        Console.Write("Press \"Enter\" to Play Again");
                        x += 26;
                    }
                    else if (x == 8 && y == Heigth / 2 + 2)
                    {
                        Console.Write("Press \"Delete\" to Exit");
                        x += 21;
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string(arenaChar, 40));
            ConsoleKeyInfo keyPress = default(ConsoleKeyInfo);
            while (true)
            {
                if (keyPress.Key == ConsoleKey.Enter || keyPress.Key == ConsoleKey.Delete)
                    break;
                keyPress = Console.ReadKey();
            }
            if (keyPress.Key == ConsoleKey.Enter)
            {
                gameOver = false;
                Setup();
                Start();
            }
        }
    }

    enum Keys
    {
        Up = 0,
        Down = 1,
        Right,
        Left,
        Stop,
    }
}
