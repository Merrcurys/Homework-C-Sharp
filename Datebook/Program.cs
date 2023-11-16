namespace Datebook
{
    internal class Program
    {
        static DateTime date = DateTime.Now;
        static int position = 1;
        static int maxPosition = 0;

        // Почему нужно уже прописывать заметки? Это слишком много лишних строчек кода :(
        static Note task1 = new Note() { Title = "Скипнуть пару", Description = "С кайфом не придти на пару", Date = "15.11.2023", Time = "7:00" };
        static Note task2 = new Note() { Title = "Залететь на C#", Description = "Просто посидеть на паре", Date = "15.11.2023", Time = "12:00" };
        static Note task3 = new Note() { Title = "Выбросить мусор", Description = "Выбросить мусор в мусорку", Date = "17.11.2023", Time = "17:31" };
        static Note task4 = new Note() { Title = "Найти кота", Description = "Найти кота и поиграть с ним", Date = "18.11.2023", Time = "9:00" };
        static Note task5 = new Note() { Title = "Посадить дерево", Description = "Посадить дерево, вырастить С#, построить консольное приложение", Date = "18.11.2023", Time = "22:00" };
        static List<Note> tasks = new List<Note>();
        static List<Note> selectedTasks = new List<Note>();

        static void DefaultAdd()
        {
            // снова лишние строчки кода из-за дефолтных значений :(
            tasks.Add(task1);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task4);
            tasks.Add(task5);
        }

        static void Main()
        {
            DefaultAdd();
            NotesMenu();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        ArrowMenu(key.Key);
                        break;

                    case ConsoleKey.DownArrow:
                        ArrowMenu(key.Key);
                        break;

                    case ConsoleKey.LeftArrow:
                        DateChange(-1);
                        break;

                    case ConsoleKey.RightArrow:
                        DateChange(1);
                        break;

                    case ConsoleKey.Enter:
                        if (selectedTasks.Any())
                            ShowDescription(selectedTasks[position - 1]);
                        break;

                    case ConsoleKey.Spacebar: // создание новой заметки через SpaceBar
                        CreateNote();
                        break;
                }
            } while (key.Key != ConsoleKey.Escape);
        }

        static void NotesMenu()
        {
            Console.Clear();
            Console.WriteLine("Выбрана дата: "  + date.ToLongDateString());
            int num = 1;

            foreach (Note note in tasks)
            {
                if (note.Date == date.ToShortDateString())
                {
                    Console.WriteLine("   " + num + ". " + note.Title);
                    selectedTasks.Add(note);
                    maxPosition++;
                    num++;
                }
                    
            }
        }

        static void DateChange(int day)
        {
            date = date.AddDays(day);
            maxPosition = 0;
            position = 1;
            selectedTasks.Clear();
            NotesMenu();
        }

        static void ArrowMenu(ConsoleKey key)
        {
            if (maxPosition != 0)
            {
                Console.SetCursorPosition(0, position);
                Console.WriteLine("  ");

                if (key == ConsoleKey.UpArrow)
                {
                    if (position != 1)
                        position--;
                }
                else if (key == ConsoleKey.DownArrow)
                    if (position < maxPosition)
                        position++;

                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");
            }
        }

        static void ShowDescription(Note task)
        {
            Console.Clear();
            Console.WriteLine(task.Title);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Описание: " + task.Description);
            Console.WriteLine("Когда: " + task.Date + " в " + task.Time);

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Enter);
            maxPosition = 0;
            position = 1;
            NotesMenu();
        }

        static void CreateNote()
        {
            Console.Clear();
            Console.WriteLine("Введите название заметки:");
            string note_title =  Console.ReadLine();
            Console.WriteLine("Введите описание заметки:");
            string note_description = Console.ReadLine();
            Console.WriteLine("Введите дату заметки в формет ДД.ММ.ГГГГ:");
            string note_date = Console.ReadLine();
            Console.WriteLine("Введите время:");
            string note_time = Console.ReadLine();
            Note task = new Note() { Title = note_title, Description = note_description, Date = note_date, Time = note_time };
            tasks.Add(task);
            maxPosition = 0;
            position = 1;
            NotesMenu();
        }
    }

}