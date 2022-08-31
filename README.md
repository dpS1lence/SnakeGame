# SnakeGame

If you dont want Sql Connection just replace it with this:

		Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < 3; i++) Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"You died!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Your Score -> {snake.SnakeSize}{Environment.NewLine}");
                using (StreamWriter writer = new("memory.txt", true))
                {
                    writer.WriteLine(snake.SnakeSize);
                }
                using (StreamReader reader = new("memory.txt"))
                {
                    var line = reader.ReadLine();
                    List<int> scores = new();
                    while (line != null)
                    {
                        scores.Add(int.Parse(line));
                        line = reader.ReadLine();
                    }
                    int bestScore = scores.Max();
                    bool isNewBestScore = snake.SnakeSize > bestScore;
                    if (isNewBestScore)
                    {
                        Console.WriteLine($"New best score -> {snake.SnakeSize}");
                    }
                    if (!isNewBestScore)
                    {
                        Console.WriteLine($"{ex.Message}{Environment.NewLine}");
                        Console.WriteLine($"Best score -> {bestScore}{Environment.NewLine}{Environment.NewLine}");
                    }
                }

                for (int i = 0; i < 5; i++)
                    Console.Beep();