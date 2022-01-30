using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Name
{
    public class SnakeRangeException : Exception
    {
        public SnakeRangeException() { }
    }
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    class Snake
    {
        int boardHeight = 20;
        int boardWidth = 50;

        int[] snakeX = new int[50];
        int[] snakeY = new int[50];

        public List<Position> position = new List<Position>();

        int fruitX;
        int fruitY;
        public int snakeSize = 3;

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'W';

        Random random = new Random();
        Snake()
        {
            snakeX[0] = 5;
            snakeY[0] = 5;
            Console.CursorVisible = false;
            fruitX = random.Next(4, boardWidth - 4);
            fruitY = random.Next(4, boardHeight - 4);
        }
        public void Board()
        {
            //Console.BackgroundColor = ConsoleColor.Gray;
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
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }
        public void WritePoint(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        public void WriteEmptyPoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        public void WriteFruit(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(x, y);
            Console.Write($"{(char)64}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void Logic()
        {
            if (snakeX[0] == fruitX)
            {
                if (snakeY[0] == fruitY)
                {
                    snakeSize++;
                    Score();
                    fruitX = random.Next(4, boardWidth - 4);
                    fruitY = random.Next(4, boardHeight - 4);
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
                        WritePoint(snakeX[i], snakeY[i], '*'); 
                    }
                    else WritePoint(snakeX[i], snakeY[i], '▀');
                }
                else if (snakeX[i] != 0)
                {
                    throw new SnakeRangeException();
                }
                WriteFruit(fruitX, fruitY);
            }
            Thread.Sleep(150);
        }
        static void Main()
        {
            Snake snake = new Snake();
            try
            {
                snake.Board();
                while (true)
                {
                    snake.Score();
                    snake.Input();
                    snake.Logic();
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n\nYou died!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Your Score -> {snake.snakeSize}\n");
                using (StreamReader reader = new StreamReader("memory.txt"))
                {
                    var line = reader.ReadLine();
                    List<int> scores = new List<int>();
                    while (line != null)
                    {
                        scores.Add(int.Parse(line));
                        line = reader.ReadLine();
                    }
                    int bestScore = scores.Max();
                    bool isNewBestScore = snake.snakeSize > bestScore;
                    if (isNewBestScore)
                    {
                        Console.WriteLine($"New best score -> {snake.snakeSize}");
                    }
                    if (!isNewBestScore)
                    {
                        Console.WriteLine($"Best score -> {bestScore}\n\n");
                    }
                }
                using (StreamWriter writer = new StreamWriter("memory.txt", true))
                {
                    writer.WriteLine(snake.snakeSize);
                }

                for (int i = 0; i < 5; i++)
                    Console.Beep();
            }
        }
    }
}