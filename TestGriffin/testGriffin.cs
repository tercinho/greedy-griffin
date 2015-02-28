using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


class testGriffin
{
    struct Object
    {
        public int x;
        public int y;
        public ConsoleColor color;
        public char c;
    }
    static void PrintOnPosition(int x, int y, char c, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.White)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    
    static void Main()
    {
        int playFieldWidth = 10;
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 50;
        Object griffin = new Object();
        griffin.c = 'G';
        griffin.color = ConsoleColor.DarkRed;
        griffin.x = 2;
        griffin.y = Console.WindowHeight - 1;
        Random randomGenerator = new Random();
        List<Object> rocks = new List<Object>();
        StreamReader reader = new StreamReader("score.txt");
        int lives = 5;
        long score = 0;
        string topScoreString = reader.ReadLine();
        reader.Close();
        long topScore = int.Parse(topScoreString);
        
        while (true)
        {
            bool rockHitted = false;
            bool lifeHitted = false;
            bool moneyHitted = false;
            {
                int chance = randomGenerator.Next(0, 100);
                if (chance < 1)
                {
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.Yellow;
                    nextrock.c = 'L';
                    nextrock.x = randomGenerator.Next(1, playFieldWidth);
                    nextrock.y = 0;
                    rocks.Add(nextrock);
                   
                }
                else if(chance < 2)
                {
                    
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.DarkMagenta;
                    nextrock.c = '*';
                    nextrock.x = randomGenerator.Next(1, playFieldWidth);
                    nextrock.y = 0;
                    rocks.Add(nextrock);
                   
                }
                else if (chance < 30)
                {
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.Green;
                    nextrock.c = '$';
                    nextrock.x = randomGenerator.Next(1, playFieldWidth);
                    nextrock.y = 0;
                    rocks.Add(nextrock);
                }
                else
                {
                    Object nextrock = new Object();
                    nextrock.color = ConsoleColor.DarkGray;
                    nextrock.c = '^';
                    nextrock.x = randomGenerator.Next(1, playFieldWidth);
                    nextrock.y = 0;
                    rocks.Add(nextrock);
                }
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (keyPressed.Key == ConsoleKey.LeftArrow)
                {
                    if (griffin.x - 1 >= 1)
                    {
                        griffin.x -= 1;
                    }

                }
                else if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    if (griffin.x + 1 < playFieldWidth)
                    {
                        griffin.x += 1;
                    }
                }
            }
            List<Object> nextrocks = new List<Object>();
            for (int i = 0; i < rocks.Count; i++)
            {
                Object oldrock = rocks[i];
                Object nextrock = new Object();
                nextrock.x = oldrock.x;
                nextrock.y = oldrock.y + 1;
                nextrock.c = oldrock.c;
                nextrock.color = oldrock.color;

                
                if (nextrock.c == '^' &&  nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    rockHitted = true;
                    if (lives < 1)
                    {
                        
                        PrintStringOnPosition(15, 10, "GAME OVER!!!", ConsoleColor.Red);
                        PrintStringOnPosition(15, 12, "Ress Enter to exit", ConsoleColor.Red);
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        if (score > topScore)
                        {
                            topScore = score;
                        }
                        lives--;
                        score = 0;
                    }
                }
                if (nextrock.c == '$' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                { 
                    moneyHitted = true;
                    score += 10;
                }
                if (nextrock.c == '*' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    
                    score *= 2;
                }
                if (nextrock.c == 'L' && nextrock.x == griffin.x && nextrock.y == griffin.y)
                {
                    lifeHitted = true;
                    lives++;
                }
                if (nextrock.y < Console.WindowHeight)
                {
                    nextrocks.Add(nextrock);
                }
            }
            rocks = nextrocks;
            Console.Clear();
            if (rockHitted)
            {
                rocks.Clear();
                PrintOnPosition(griffin.x, griffin.y, 'X', ConsoleColor.Red);
                Console.Beep(500, 1000);
            }
            else if (moneyHitted)
            {
                PrintOnPosition(griffin.x, griffin.y, 'G', ConsoleColor.Green);
            }
            else if (lifeHitted)
            {
                PrintOnPosition(griffin.x, griffin.y, 'G', ConsoleColor.Yellow);
                Console.Beep(1200, 200);
            }
            else
            {
                PrintOnPosition(griffin.x, griffin.y, griffin.c, griffin.color);
            }
            foreach (Object rock in rocks)
            {
                PrintOnPosition(rock.x, rock.y, rock.c, rock.color);
            }
            PrintStringOnPosition(15, 13, "Top Score: " + topScore);
            PrintStringOnPosition(15, 8, "L - Extra life");
            PrintStringOnPosition(15, 7, "* - Bonus: money X 2");
            PrintStringOnPosition(15, 6, "$ - Money");
            PrintStringOnPosition(15, 5, "^ - Rocks!");
            PrintStringOnPosition(15, 10, "Lives: " + lives);
            PrintStringOnPosition(15, 12, "Money: " + score);
            PrintStringOnPosition(17, 0, "$ GREEDY GRIFFIN $", ConsoleColor.Cyan);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                PrintOnPosition(10, i, '*', ConsoleColor.Green);
                PrintOnPosition(0, i, '*', ConsoleColor.Green);
            }

            Thread.Sleep(250);


        }
 
    }
}
 