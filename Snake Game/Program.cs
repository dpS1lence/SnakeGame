using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Threading;
using Snake_Game.Exceptions;
using Snake_Game.CustomObjects;

namespace Name
{
    class Program
    {
        static void Main()
        {
            Snake snake = new();
            try
            {
                snake.Board();
                while (true)
                {
                    snake.Score();
                    snake.Input();
                    snake.Logic();
                }
            }
            catch (SnakeRangeException ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n\nYou died!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Your Score -> {snake.snakeSize}\n");
                using (StreamWriter writer = new StreamWriter("memory.txt", true))
                {
                    writer.WriteLine(snake.snakeSize);
                }
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
                        Console.WriteLine($"{ex.Msg}{Environment.NewLine}");
                        Console.WriteLine($"Best score -> {bestScore}\n\n");
                    }
                }

                for (int i = 0; i < 5; i++)
                    Console.Beep();
            }
        }
    }
}