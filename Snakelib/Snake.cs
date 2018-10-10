using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snakelib
    {
    public class Snake : ISnakeable
        {

        public Snake()
            {
            Setup();
            }

        private int snakeX;
        private int snakeY;
        private int fruitX;
        private int fruitY;
        private Keys direction = Keys.Stop;
        private int Heigth { get => 20; }
        private int Width { get => 40; }
        private char arenaChar = '■';
        private char snake = '@';
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
                        Console.Write(snake);
                        }
                    else if (x == fruitX && y == fruitY)
                        {

                        Console.Write("O");
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
            Thread.Sleep(40);
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
                }
            if (snakeX < 0 || snakeY < 0 || snakeX > Width - 1 || snakeY > Heigth - 1)
                {
                gameOver = true;
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
                        else if (score > 9 && score < 99)
                            {
                            x += 25;
                            }
                        else
                            {
                            x += 23;
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

        public void DrawTail()
            {
            ;
            }
        }
    }
