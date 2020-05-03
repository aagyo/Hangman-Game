using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        SerializationActions<GameWindowVM> actions = new SerializationActions<GameWindowVM>();
        SerializationActions<User> actions1 = new SerializationActions<User>();
        private User sU;
        public GameWindow(User selectedUser)
        {
            InitializeComponent();
            if (File.Exists($"{ selectedUser.Nickname.ToLower()}Stats.xml"))
            {
                selectedUser = actions1.DeserializeObject($"{selectedUser.Nickname.ToLower()}Stats.xml");
            }
            DataContext = new GameWindowVM(selectedUser);
            sU = selectedUser;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            sU = (this.DataContext as GameWindowVM).SelectedUser;
            actions1.SerializeObject($"{sU.Nickname.ToLower()}Stats.xml", sU);
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow wind = new AboutWindow();
            wind.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenGameBtn_Click(object sender, RoutedEventArgs e)
        {
            actions1.SerializeObject($"{sU.Nickname.ToLower()}Stats.xml", sU);
            if (!File.Exists($"{sU.Nickname.ToLower()}Saved.xml"))
            {
                MessageBox.Show("You have no saved game!");
                (this.DataContext as GameWindowVM).IsSaveEnabled = false;
            }
            else
            {
                DataContext = actions.DeserializeObject($"{sU.Nickname.ToLower()}Saved.xml");
                (this.DataContext as GameWindowVM).SelectedUser = actions1.DeserializeObject($"{sU.Nickname.ToLower()}Stats.xml");
                (this.DataContext as GameWindowVM).IsSaveEnabled = true;
            }
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            StatsWindow wind = new StatsWindow(this.DataContext as GameWindowVM);
            wind.Show();
        }
    }
}
