using Hangman.Models;
using Hangman.ViewModels;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerializationActions<MainWindowViewModel> actions = new SerializationActions<MainWindowViewModel>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = actions.DeserializeObject("users.xml");
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            MainWindowViewModel mwv = DataContext as MainWindowViewModel;
            mwv.SelectedUser = null;
            mwv.IsEnterVisible = false;
            actions.SerializeObject("users.xml", DataContext as MainWindowViewModel);
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            GameWindow view = new GameWindow((DataContext as MainWindowViewModel).SelectedUser);
            view.Show();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
