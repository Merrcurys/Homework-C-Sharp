(double, double) input_numbers()
{
    Console.Write("Введите число: ");
    double num_1 = double.Parse(Console.ReadLine());
    Console.Write("Введите второе число: ");
    double num_2 = double.Parse(Console.ReadLine());
    return (num_1, num_2);
}

double input_number()
{
    Console.Write("Введите число: ");
    double num_1 = double.Parse(Console.ReadLine());
    return num_1;
}

Console.WriteLine("КАЛЬКУЛЯТОР");
Console.WriteLine("Меню команд:");
Console.WriteLine("1. Сложение");
Console.WriteLine("2. Вычитание");
Console.WriteLine("3. Умножение");
Console.WriteLine("4. Деление");
Console.WriteLine("5. Возведение в степень");
Console.WriteLine("6. Квадратный корень");
Console.WriteLine("7. Один процент от числа");
Console.WriteLine("8. Факториал");
Console.WriteLine("9. Выйти из программы");

bool flag = true;

while (flag)
{
    Console.Write("Введите команду: ");
    int command = int.Parse(Console.ReadLine());
    switch (command)
    {
        case 1:
            (double num_1, double num_2) = input_numbers();
            Console.WriteLine("Результат:" + (num_1 + num_2));
            break;
        case 2:
            (num_1, num_2) = input_numbers();
            Console.WriteLine("Результат:" + (num_1 - num_2));
            break;
        case 3:
            (num_1, num_2) = input_numbers();
            Console.WriteLine("Результат:" + (num_1 * num_2));
            break;
        case 4:
            (num_1, num_2) = input_numbers();
            if (num_2 != 0)
            {
                Console.WriteLine("Результат:" + (num_1 / num_2));
            }
            else
            {
                Console.WriteLine("Делить на ноль нельзя!");
            }
            break;
        case 5:
            (num_1, num_2) = input_numbers();
            Console.WriteLine("Результат:" + Math.Pow(num_1, num_2));
            break;
        case 6:
            num_1 = input_number();
            Console.WriteLine("Результат:" + Math.Sqrt(num_1));
            break;
        case 7:
            num_1 = input_number();
            Console.WriteLine("Результат:" + (num_1 / 100));
            break;
        case 8:
            num_1 = (int)input_number();
            int count = 1;
            for (int i = 1; i <= num_1; i++)
            {
                count *= i;
            }
            Console.WriteLine("Результат:" + count);
            break;
        case 9:
            flag = false;
            break;
        default:
            Console.WriteLine("Такой команды не существует!");
            break;
    }
}

