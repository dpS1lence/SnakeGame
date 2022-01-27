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

    class Snake
    {
        int boardHeight = 20;
        int boardWidth = 50;

        int[] snakeX = new int[50];
        int[] snakeY = new int[50];

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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.Write($"Size -> {snakeSize}");
            for (int i = 1; i <= boardWidth + 2; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("=");
            }
            for (int i = 1; i < boardWidth + 2; i++)
            {
                Console.SetCursorPosition(i, boardHeight + 2);
                Console.Write("=");
            }
            for (int i = 1; i <= boardHeight + 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("||");
            }
            for (int i = 1; i < boardHeight + 2; i++)
            {
                Console.SetCursorPosition(boardWidth + 2, i);
                Console.Write("||");
            }
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }
        public void WritePoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"{(char)42}");
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
                    fruitX = random.Next(4, boardWidth - 4);
                    fruitY = random.Next(4, boardHeight - 4);
                }
            }
            for (int i = snakeSize; i > 0; i--)
            {

                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
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
            }
            for (int i = 0; i <= snakeSize - 1; i++)
            {
                bool isInrange = snakeX[i] > 1 && snakeX[i] < 53 && snakeY[i] > 1 && snakeY[i] < 23;
                if (isInrange)
                {
                    WritePoint(snakeX[i], snakeY[i]);
                }
                else if(snakeX[i] != 0)
                {
                    throw new SnakeRangeException();
                }
                WriteFruit(fruitX, fruitY);
            }
            Thread.Sleep(100);
        }
        static void Main()
        {
            Snake snake = new Snake();
            try
            {
                while (true)
                {
                    snake.Board();
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
                Console.WriteLine($"Your size-> { snake.snakeSize}\n\n\n");
                for (int i = 0; i < 5; i++)
                    Console.Beep();
            }
        }
    }
}