using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;


class Program
{
    static void Main()
    {
        Console.WriteLine("Добро пожаловать в 'СПИДРАН ПО ПЕЧАТАНЬЮ'!");
        Console.Write("Введите свое имя, ну иль ник, есль угодно: ");
        string userName = Console.ReadLine();

        while (true)
        {
            TypeSpeedTest.RunSpeedTest(userName);

            Console.Write("Красава, хочешь попытаться улучшить результаты? (Да/Нет): ");
            string again = Console.ReadLine();

            if (again?.ToLower() != "да")
            {
                Console.Write("Ну тогда покеда!");
                break;
            }
               
        }
    }
}

class TypeSpeedTest
{
    public static void RunSpeedTest(string userName)
    {
        string text = "Чтобы узнать насколько хороша ваша скорость" +
            " и точность печати в данный момент.";
        int currentCharIndex = 0;
        int correctCount = 0;
        int mistakeCount = 0;
        Stopwatch stopwatch = new Stopwatch();

        Console.Clear();
        Console.WriteLine($"Здравие тебе, {userName}! Разомни свои пальцы!");

        Console.WriteLine("Жми клавишу Enter, чтобы начать..");
        Console.ReadLine();
        Console.Clear();
        Console.CursorVisible = false;

        stopwatch.Start();

        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.Yellow;

        while (currentCharIndex < text.Length)
        {
            Console.SetCursorPosition(currentCharIndex, 0);
            Console.Write(text[currentCharIndex]);

            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.KeyChar == text[currentCharIndex])
            {
                Console.SetCursorPosition(currentCharIndex, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text[currentCharIndex]);
                Console.ForegroundColor = ConsoleColor.Yellow;

                currentCharIndex++;
                correctCount++;
            }
            else
            {
                Console.SetCursorPosition(currentCharIndex, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(text[currentCharIndex]);

                mistakeCount++;
            }
        }

        stopwatch.Stop();

        double minutes = stopwatch.Elapsed.TotalMinutes;
        double seconds = stopwatch.Elapsed.TotalSeconds;

        int charactersPerMinute = (int)(correctCount / minutes);
        int charactersPerSecond = (int)(correctCount / seconds);

        Console.Clear();
        Console.CursorVisible = true;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Тест пройден - победа!");
        Console.WriteLine($"Суммарное затраченное время: {stopwatch.Elapsed}");
        Console.WriteLine($"Знаков в минуту: {charactersPerMinute}");
        Console.WriteLine($"Знаков в секунду: {charactersPerSecond}");
        Console.WriteLine($"Символов набрано: {correctCount}");
        Console.WriteLine($"Ошибок сделано: {mistakeCount}");

        var userData = new UserData
        {
            Name = userName,
            CharactersPerMinute = charactersPerMinute,
            CharactersPerSecond = charactersPerSecond
        };
        Leaderboard.AddToLeaderboard(userData);
        Console.WriteLine();
        Leaderboard.DisplayLeaderboard();
    }
}

public class UserData
{
    public string Name { get; set; }
    public int CharactersPerMinute { get; set; }
    public int CharactersPerSecond { get; set; }
}

public static class Leaderboard
{
    private const string LeaderboardFilePath = "leaderboard.json";
    private static List<UserData> leaderboardData;

    static Leaderboard()
    {
        if (File.Exists(LeaderboardFilePath))
        {
            string json = File.ReadAllText(LeaderboardFilePath);
            leaderboardData = JsonConvert.DeserializeObject<List<UserData>>(json);
        }
        else
        {
            leaderboardData = new List<UserData>();
        }
    }

    public static void AddToLeaderboard(UserData user)
    {
        leaderboardData.Add(user);
        leaderboardData = leaderboardData.OrderBy(user => user.CharactersPerMinute).ToList();
        SaveLeaderboardToFile();
    }

    public static void DisplayLeaderboard()
    {
        Console.WriteLine("Таблица рекордов:");
        Console.WriteLine("Имя\t\tКСМ\t\tКСС");
        foreach (var user in leaderboardData)
        {
            Console.WriteLine($"{user.Name}\t\t{user.CharactersPerMinute}\t\t{user.CharactersPerSecond}");
        }
    }

    private static void SaveLeaderboardToFile()
    {
        string json = JsonConvert.SerializeObject(leaderboardData, Formatting.Indented);
        File.WriteAllText(LeaderboardFilePath, json);
    }
}