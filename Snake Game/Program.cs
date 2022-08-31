using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Threading;
using Snake_Game.Exceptions;
using Snake_Game.CustomObjects;
using System.Data.SqlClient;
using Snake_Game.DbConstants;

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
            catch (SnakeException ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < 3; i++) Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"{ex.Message}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Your Score -> {snake.SnakeSize}{Environment.NewLine}");

                int score = snake.SnakeSize;
                
                string username = Environment.UserName;

                if (!Db.UserExists(username))
                {
                    Db.Insert(username, score.ToString());
                }
                if (Db.UserExists(username))
                {
                    string[] output = Db.Read(username);
                    int userScore = int.Parse(output[1]);
                    if(score > userScore)
                    {
                        Db.UpdateDb(score.ToString(), username);
                        Console.WriteLine($"Good job {output[0]}, new best score -> {score}{Environment.NewLine}");
                    }
                    else Console.WriteLine($"Your Best score -> {output[0]} - {userScore}{Environment.NewLine}");
                }
                
                for (int i = 0; i < 5; i++)
                    Console.Beep();
            }
        }
    }
}