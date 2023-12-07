using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;


class Program
{
    static void Main()
    {
        Console.WriteLine("СПИДРАН ПО ПЕЧАТАНЬЮ ПОЕХАЛИ!");
        Console.Write("Введите свое имя, ну иль ник, есль те угодно: ");
        string userName = Console.ReadLine();

        while (true)
        {
            TypeSpeedTest.RunSpeedTest(userName);

            Console.Write("Ну что хочешь попытаться улучшить результаты? (Да/Нет): ");
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
            " и точность печати в данный момент нужно ввести этот текст.";
        int currentCharIndex = 0;
        int correctCount = 0;
        int mistakeCount = 0;
        bool inputAllowed = true;
        bool flag = true;
        bool success = true;
        Stopwatch stopwatch = new Stopwatch();

        Console.Clear();
        Console.WriteLine($"Здравие тебе, {userName}! Разомни свои пальцы! И помни у тебя будет не больше минуты!");

        Console.WriteLine("Жми клавишу Enter, чтобы начать..");
        Console.ReadLine();
        Console.Clear();
        Console.CursorVisible = false;

        stopwatch.Start();

        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.Yellow;

        var timerThread = new Thread(() =>
        {
            while (stopwatch.Elapsed.TotalMinutes <= 1 && flag)
            {
                Thread.Sleep(1000);
                Console.SetCursorPosition(0, 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Таймер: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}");
            }
            inputAllowed = false;
            if (stopwatch.Elapsed.TotalMinutes > 1)
                success = false;
        });

        stopwatch.Start();
        timerThread.Start();

        while (inputAllowed && correctCount < text.Length)
        {
            Console.SetCursorPosition(currentCharIndex, 0);
            Console.Write(text[currentCharIndex]);

            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.KeyChar == text[currentCharIndex])
            {
                Console.SetCursorPosition(currentCharIndex, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text[currentCharIndex]);

                if (!(text.Length == currentCharIndex + 1))
                    currentCharIndex++;

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("                                                            ");
                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"Осталось символов: {text.Length - currentCharIndex}");
                Console.SetCursorPosition(0, 4);
                Console.WriteLine($"Всего символов: {text.Length}");
                Console.ForegroundColor = ConsoleColor.Yellow;
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

        flag = false;
        Thread.Sleep(1000);
        stopwatch.Stop();

        double minutes = stopwatch.Elapsed.TotalMinutes;
        double seconds = stopwatch.Elapsed.TotalSeconds;

        int charactersPerMinute = (int)(correctCount / minutes);
        int charactersPerSecond = (int)(correctCount / seconds);

        Console.Clear();
        Console.CursorVisible = true;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Тест пройден");
        Console.WriteLine($"Суммарное затраченное время: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}:{stopwatch.Elapsed.Milliseconds:D2}");
        Console.WriteLine($"Знаков в минуту: {charactersPerMinute}");
        Console.WriteLine($"Знаков в секунду: {charactersPerSecond}");
        Console.WriteLine($"Всего набрано символов: {correctCount + mistakeCount}");
        Console.WriteLine($"Всего правильных символов сделано: {correctCount}/{text.Length}");
        Console.WriteLine($"Ошибок сделано: {mistakeCount}");
        Console.WriteLine();

        if (success) {
            var userData = new UserData
            {
                Name = userName,
                CharactersPerMinute = charactersPerMinute,
                CharactersPerSecond = charactersPerSecond
            };
            Leaderboard.AddToLeaderboard(userData);
            Leaderboard.DisplayLeaderboard();
        }
        else {
            Console.WriteLine("Вы не прошли тест, поэтому ваши данные не будут занесены в таблицу рекордов.");
        }
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