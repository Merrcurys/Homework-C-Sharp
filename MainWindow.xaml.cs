using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public DateTime Today { get { return DateTime.Today; } }
        public List<Note> Notes { get; set; } = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Notes = JsonData.Deserialize<List<Note>>();
            Display();
        }

        public void Display()
        {
            name_note.Text = string.Empty;
            description_note.Text = string.Empty;
            delete_button.IsEnabled = false;
            save_button.IsEnabled = false;
            create_button.IsEnabled = true;

            if (pik.SelectedDate.HasValue)
            {
                notesdisplay.ItemsSource = Notes.Where(n => n.Date == pik.SelectedDate.Value.ToString("dd.MM.yyyy")).ToList();
                notesdisplay.DisplayMemberPath = "Title";
            }

        }

        private void Create_Note(object sender, RoutedEventArgs e)
        {
            if (name_note.Text == string.Empty || description_note.Text == string.Empty)
            {
                MessageBox.Show("Вы не заполнили все окна!");
                return;
            } else
            {
                Note note = new Note
                {
                    Title = name_note.Text,
                    Description = description_note.Text,
                    Date = pik.Text
                };

                Notes.Add(note);
                JsonData.Serialize(Notes);

                Display();
            }
        }

        private void Delete_Note(object sender, RoutedEventArgs e)
        {
            if (name_note.Text == string.Empty || description_note.Text == string.Empty)
            {
                MessageBox.Show("Вы не заполнили все окна!");
                return;
            }
            else {
                Note SelectedNote = (Note)notesdisplay.SelectedItem;
                Notes.Remove(SelectedNote);
                JsonData.Serialize(Notes);
                Display();
            }   
        }

        private void Save_Note(object sender, RoutedEventArgs e)
        {
            if (name_note.Text == string.Empty || description_note.Text == string.Empty)
            {
                MessageBox.Show("Вы не заполнили все окна!");
                return;
            }
            else
            {
                Note SelectedNote = (Note)notesdisplay.SelectedItem;
                SelectedNote.Title = name_note.Text;
                SelectedNote.Description = description_note.Text;
                JsonData.Serialize(Notes);
                Display();
                return;
            }
        }

        private void pik_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Display();
        }

        private void notesdisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (notesdisplay.SelectedIndex != -1)
            {
                Note SelectedNote;
                create_button.IsEnabled = false;
                save_button.IsEnabled = true;
                delete_button.IsEnabled = true;

                SelectedNote = (Note)notesdisplay.Items[notesdisplay.SelectedIndex];
                name_note.Text = SelectedNote.Title;
                description_note.Text = SelectedNote.Description;

            }

        }
    }
}


