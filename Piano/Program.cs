Console.WriteLine("!___---ПиАнИнО---___! \n");
Console.WriteLine("Нажмите <L> для отображения легенды.");

static void reproduce(int hertz)
{
    Console.Beep(hertz, 200);
}

static int[] octave_change(int octava)
{
    int[] fifthOctave = new int[]
    {
        523, 554, 578, 623, 659, 698, 740, 748, 830, 880, 932, 987
    };
    int[] sixthOctave = new int[]
    {
        1047, 1109, 1175, 1245, 1319, 1397, 1480, 1568, 1661, 1760, 1865, 1976
    };
    int[] seventhOctave = new int[] {
        2093, 2217, 2349, 2489, 2637, 2794, 2960, 3136, 3322, 3520, 3729, 3951
    };

    if (octava == 5)
    {
        return fifthOctave;
    }
    else if (octava == 6)
    {
        return sixthOctave;
    }
    else if (octava == 7)
    {
        return seventhOctave;
    }
    return fifthOctave;
}


ConsoleKeyInfo press;
int[] octave = octave_change(5);

do
{
    press = Console.ReadKey(true);
    switch (press.Key)
    {
        // Октавы
        case ConsoleKey.F5:
            octave = octave_change(5);
            break;
        case ConsoleKey.F6:
            octave = octave_change(6);
            break;
        case ConsoleKey.F7:
            octave = octave_change(7);
            break;
        // Клавиши
        case ConsoleKey.D1:
            reproduce(octave[0]);
            break;
        case ConsoleKey.Q:
            reproduce(octave[1]);
            break;
        case ConsoleKey.D2:
            reproduce(octave[2]);
            break;
        case ConsoleKey.W:
            reproduce(octave[3]);
            break;
        case ConsoleKey.D3:
            reproduce(octave[4]);
            break;
        case ConsoleKey.D4:
            reproduce(octave[5]);
            break;
        case ConsoleKey.R:
            reproduce(octave[6]);
            break;
        case ConsoleKey.D5:
            reproduce(octave[7]);
            break;
        case ConsoleKey.T:
            reproduce(octave[8]);
            break;
        case ConsoleKey.D6:
            reproduce(octave[9]);
            break;
        case ConsoleKey.Y:
            reproduce(octave[10]);
            break;
        case ConsoleKey.D7:
            reproduce(octave[11]);
            break;
        case ConsoleKey.L:
            Console.WriteLine("Легенда:");
            Console.WriteLine("С - 1   C# - Q   D - 2   D# - W   E - 3   F - 4");
            Console.WriteLine("F# - R   G - 5   G# - T   A - 6   A# - Y   B - 7 \n");
            Console.WriteLine("5 октава - F5   6 октава - F6   7 октава - F7 \n");
            Console.WriteLine("Выход - ESC");
            break;
    }
} while (press.Key != ConsoleKey.Escape);


