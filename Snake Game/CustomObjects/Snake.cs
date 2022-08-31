using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake_Game.Exceptions;
using Snake_Game.CustomObjects;

namespace Snake_Game.CustomObjects
{
    public class Snake
    {
        private readonly int boardHeight = 20;
        private readonly int boardWidth = 50;

        private readonly int[] snakeX = new int[50];
        private readonly int[] snakeY = new int[50];

        private int snakeSize = 3;

        private Position HeadPos { get; set; }

        private int FruitX { get; set; }
        private int FruitY { get; set; }
        private int WallX { get; set; }
        private int WallY { get; set; }
        public int SnakeSize
        {
            get
            {
                return snakeSize;
            }
        }
        private List<int> WallsX { get; set; }
        private List<int> WallsY { get; set; }

        private ConsoleKeyInfo KeyInfo { get; set; }
        private char key = 'W';

        private Random Random { get; set; }
        public Snake()
        {
            HeadPos = new Position();
            WallsX = new List<int>();
            WallsY = new List<int>();
            KeyInfo = new ConsoleKeyInfo();
            Random = new Random();

            snakeX[0] = 5;
            snakeY[0] = 5;
            Console.CursorVisible = false;
            FruitX = this.Random.Next(4, boardWidth - 4);
            FruitY = Random.Next(4, boardHeight - 4);
        }
        public void Board()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            for (int i = 1; i <= boardWidth + 2; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("▀");
            }
            for (int i = 1; i < boardWidth + 2; i++)
            {
                Console.SetCursorPosition(i, boardHeight + 2);
                Console.Write("▀");
            }
            for (int i = 1; i <= boardHeight + 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("▀");
            }
            for (int i = 1; i < boardHeight + 2; i++)
            {
                Console.SetCursorPosition(boardWidth + 2, i);
                Console.Write("▀");
            }
        }
        public void Score()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($" Score -> {snakeSize}");
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey(true);
                key = KeyInfo.KeyChar;
            }
        }
        private static void WritePoint(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        private static void WriteEmptyPoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        private static void WriteFruit(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y);
            Console.Write($"{(char)64}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void Logic()
        {
            if (WallsX.Count > 0)
            {
                for (int i = 0; i < WallsX.Count; i++)
                {
                    if (snakeX[0] == WallsX[i])
                    {
                        if (snakeY[0] == WallsY[i])
                        {
                            throw new SnakeRangeException("Died from RIPbox!");
                        }
                    }
                }
            }
            if (snakeX[0] == FruitX)
            {
                if (snakeY[0] == FruitY)
                {
                    snakeSize++;
                    Score();
                    FruitX = Random.Next(4, boardWidth - 4);
                    FruitY = Random.Next(4, boardHeight - 4);

                    Console.ForegroundColor = ConsoleColor.Red;
                    WallX = Random.Next(4, boardWidth - 4);
                    WallY = Random.Next(4, boardHeight - 4);
                    WallsX.Add(WallX);
                    WallsY.Add(WallY);
                    Console.SetCursorPosition(WallX, WallY);
                    Console.Write("=");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            for (int i = snakeSize; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
                WriteEmptyPoint(snakeX[i], snakeY[i]);
            }
            switch (key)
            {
                case 'w':
                    snakeY[0]--;
                    break;
                case 's':
                    snakeY[0]++;
                    break;
                case 'd':
                    snakeX[0]++;
                    break;
                case 'a':
                    snakeX[0]--;
                    break;
                default:
                    snakeX[0]++;
                    break;
            }
            for (int i = 0; i <= snakeSize - 1; i++)
            {
                bool isInrange = snakeX[i] > 2 && snakeX[i] < 52 && snakeY[i] > 2 && snakeY[i] < 22;
                if (isInrange)
                {
                    if (i == 0)
                    {
                        Position wantedSnakePos = new()
                        {
                            X = snakeX[i],
                            Y = snakeY[i]
                        };
                        HeadPos = wantedSnakePos;
                        WritePoint(wantedSnakePos.X, wantedSnakePos.Y, '+');
                    }
                    else
                    {
                        Position wantedSnakePos = new()
                        {
                            X = snakeX[i],
                            Y = snakeY[i]
                        };
                        if (wantedSnakePos.X == HeadPos.X && wantedSnakePos.Y == HeadPos.Y)
                        {
                            throw new SnakeRangeException("You bited your tail!");
                        }
                        WritePoint(snakeX[i], snakeY[i], '0');
                    }
                }
                else if (snakeX[i] != 0)
                {
                    throw new SnakeRangeException("Died from board!");
                }
                WriteFruit(FruitX, FruitY);
            }
            Thread.Sleep(150);
        }
    }
}