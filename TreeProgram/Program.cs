Console.WriteLine("ВЕЛИКИЕ ТРИ ПРОГРАММЫ МПТ");
Console.WriteLine("Выберите программу:");
Console.WriteLine("1. Игра \"Угадай число\"");
Console.WriteLine("2. Таблица умножения");
Console.WriteLine("3. Вывод делителей числа");
Console.WriteLine("4. Выход из програмы");

bool flag = true;

do
{
    Console.Write("Выберите программу: ");
    switch (Console.ReadLine())
    {
        case "1":
            RandomNumber();
            break;
        case "2":
            MultiTable();
            break;
        case "3":
            Deviders();
            break;
        case "4":
            Console.WriteLine("Спасибо что выбираете нас ^^");
            flag = false;
            break;
    }
} while (flag);

static void RandomNumber()
{
    int num = (new Random()).Next(0, 100);
    int count = 1;
    do
    {
        Console.Write("Введите число: ");
        int in_num = int.Parse(Console.ReadLine());
        if (in_num == num)
        {
            Console.WriteLine("Вы угадали число за " + count + " попыток!");
            break;
        } else if (in_num > num) {
            Console.WriteLine("Меньше ;)");
        } else if (in_num < num)
        {
            Console.WriteLine("Больше ;)");
        }
        ++count;
    } while (true);
    Console.WriteLine(num);
    return;
}

static void MultiTable()
{
    int[,] table = new int[9, 9];
    for (int i = 1; i < 10; i++)
    {
        for (int j = 1; j < 10; j++)
        {
             table[i, j] = i*j;
        }
    }
    foreach (int[] row in table) {    
        foreach (int num in row}
    {

    }
    return;
}

static void Deviders()
{
    List<int> deviders = new List<int>();
    Console.Write("Введите число для которого нужно найти делитель: ");
    int num = int.Parse(Console.ReadLine());
    for (int i = 1; i <= num; i++) {
        if (num % i == 0)
        {
            deviders.Add(i);
        }
    }
    Console.WriteLine("Все делители:");
    foreach (int devider in deviders) {
        Console.Write(devider + "\t");
    }
    Console.WriteLine();
    return;
}