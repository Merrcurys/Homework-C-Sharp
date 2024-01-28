using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        Button[] buttons;
        int count_enabled = 0;
        string player_metka = "x";
        string robot_metka = "o";

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9 };
            bt_lock(false);
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            count_enabled = 0;
            end_block.Text = "";
            bt_lock(true);
            bt_clear();
            if (player_metka == "x") {
                player_metka = "o";
                robot_metka = "x";
                move_robot();

            }
            else {
                player_metka = "x";
                robot_metka = "o";
            }
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            move_player(sender);
            if (print_win())
                return;

            move_robot();
            print_win();
        }

        private void move_player(object sender)
        {
            (sender as Button).Content = player_metka;
            (sender as Button).IsEnabled = false;
            (sender as Button).Foreground = player_metka == "x" ? Brushes.Red : Brushes.Blue;
            count_enabled++;
        }

        private void move_robot() {
            Random random = new Random();
            int random_choice = random.Next(0, 9);

            if (count_enabled != 9) {
                while (buttons[random_choice].IsEnabled == false)
                    random_choice = random.Next(0, 9);

                buttons[random_choice].Content = robot_metka;
                buttons[random_choice].IsEnabled = false;
                buttons[random_choice].Foreground = robot_metka == "x" ? Brushes.Red : Brushes.Blue;
                count_enabled++;
            }
        }

        private void bt_lock(bool is_lock)
        {
            foreach (Button bt in buttons)
                bt.IsEnabled = is_lock;
        }

        private void bt_clear()
        {
            foreach (Button bt in buttons)
                bt.Content = null;
        }

        private bool print_win() {
            string win = check_win();
            if (win != "")
            {
                bt_lock(false);
                if (win != "draw")
                {
                    end_block.Text = $"ПОБЕДА {win.ToUpper()}";
                    end_block.Foreground = win == "x" ? Brushes.IndianRed : Brushes.LightBlue;
                }
                else
                {
                    end_block.Text = $"   НИЧЬЯ";
                    end_block.Foreground = Brushes.ForestGreen;
                }
                return true;
            }
            return false;
        }

        private string check_win()
        {
            // горизонталь
            if (buttons[0].Content == buttons[1].Content && buttons[1].Content == buttons[2].Content && buttons[0].Content is not null)
                return buttons[0].Content == "x" ? "x" : "o";
            else if (buttons[3].Content == buttons[4].Content && buttons[4].Content == buttons[5].Content && buttons[3].Content is not null)
                return buttons[3].Content == "x" ? "x" : "o";
            else if (buttons[6].Content == buttons[7].Content && buttons[7].Content == buttons[8].Content && buttons[6].Content is not null)
                return buttons[6].Content == "x" ? "x" : "o";
            // вертикаль
            else if (buttons[0].Content == buttons[3].Content && buttons[3].Content == buttons[6].Content && buttons[0].Content is not null)
                return buttons[0].Content == "x" ? "x" : "o";
            else if (buttons[1].Content == buttons[4].Content && buttons[4].Content == buttons[7].Content && buttons[1].Content is not null)
                return buttons[1].Content == "x" ? "x" : "o";
            else if (buttons[2].Content == buttons[5].Content && buttons[5].Content == buttons[8].Content && buttons[2].Content is not null)
                return buttons[2].Content == "x" ? "x" : "o";
            // диагональ
            else if (buttons[0].Content == buttons[4].Content && buttons[4].Content == buttons[8].Content && buttons[0].Content is not null)
                return buttons[0].Content == "x" ? "x" : "o";
            else if (buttons[2].Content == buttons[4].Content && buttons[4].Content == buttons[6].Content && buttons[2].Content is not null)
                return buttons[2].Content == "x" ? "x" : "o";
            // ничья
            else if (count_enabled == 9)
                return "draw";
            return "";
        }
    }
}
