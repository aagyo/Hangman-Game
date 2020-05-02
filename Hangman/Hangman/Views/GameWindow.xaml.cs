using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
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
        private User sU;
        public GameWindow(User selectedUser)
        {
            InitializeComponent();
            DataContext = new GameWindowVM(selectedUser);
            sU = selectedUser;
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
            DataContext = actions.DeserializeObject($"{sU.Nickname.ToLower()}Saved.xml");
        }
    }
}
