using System;
using System.Collections.Generic;
using System.Threading;

enum Border
{
    MaxRight = 23,
    MaxBottom = 11
}

class Snake : Game
{

    static private void ClearTail()
    {
        foreach (var position in snake)
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(" ");
        }
    }

    static public void Move()
    {
        ClearTail();

        Position snakeHead = snake[0];
        Position newHead = new Position(snakeHead.X, snakeHead.Y);

        switch (direction)
        {
            case Direction.Up:
                newHead.Y--;
                break;
            case Direction.Down:
                newHead.Y++;
                break;
            case Direction.Left:
                newHead.X--;
                break;
            case Direction.Right:
                newHead.X++;
                break;
        }

        snake.Insert(0, newHead);

        if (snakeHead.X == food.X && snakeHead.Y == food.Y)
        {
            score++;
            food = GenerateFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }

    static public void Draw()
    {

        if (snake.Count > 0)
        {
            foreach (var position in snake)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("#");
            }
        }

        Console.SetCursorPosition(food.X, food.Y);
        Console.Write("+");

        Console.SetCursorPosition(0, (int)Border.MaxBottom + 2);
        Console.Write("Очки: " + score);
    }

    static public void CheckStrike()
    {
        Position snakeHead = snake[0];

        if (snakeHead.X < 1 || snakeHead.X >= (int)Border.MaxRight || snakeHead.Y < 1 || snakeHead.Y >= (int)Border.MaxBottom)
        {
            isGameOver = true;
        }

        for (int i = 1; i < snake.Count; i++)
        {
            if (snake[i].X == snakeHead.X && snake[i].Y == snakeHead.Y)
            {
                isGameOver = true;
                break;
            }
        }
    }
}

class Game
{
    static public List<Position> snake;
    static public Position food;
    static public int score;
    static public bool isGameOver;
    static public Direction direction;

    public Game()
    {
        snake = new List<Position> { new Position(10, 10) };
        food = GenerateFood();
        score = 0;
        isGameOver = false;
        direction = Direction.Right;
    }

    public void Start()
    {
        Console.CursorVisible = false;
        DrawBorders();

        Thread inputThread = new Thread(ReadInput);
        inputThread.Start();

        while (!isGameOver)
        {
            Snake.Move();
            Snake.Draw();
            Snake.CheckStrike();
            Thread.Sleep(80);
        }

        Console.Clear();
        Console.WriteLine("Игра окончена! Очков набрано: " + score);
    }

    private void ReadInput()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (direction != Direction.Down)
                            direction = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != Direction.Up)
                            direction = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != Direction.Right)
                            direction = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != Direction.Left)
                            direction = Direction.Right;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }

    private void DrawBorders()
    {
        for (int i = 0; i < (int)Border.MaxRight; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.WriteLine("-");
        }

        for (int i = 0; i < (int)Border.MaxRight; i++)
        {
            Console.SetCursorPosition(i, (int)Border.MaxBottom);
            Console.WriteLine("-");
        }

        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < (int)Border.MaxBottom + 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.WriteLine("|");
        }

        for (int i = 0; i < (int)Border.MaxBottom + 1; i++)
        {
            Console.SetCursorPosition((int)Border.MaxRight, i);
            Console.WriteLine("|");
        }
    }

    static public Position GenerateFood()
    {
        Random rand = new Random();
        int x = rand.Next(1, (int)Border.MaxRight);
        int y = rand.Next(1, (int)Border.MaxBottom);

        return new Position(x, y);
    }

}

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}