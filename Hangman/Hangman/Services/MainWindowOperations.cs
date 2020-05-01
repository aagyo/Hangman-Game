using Hangman.Models;
using Hangman.ViewModels;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hangman.Services
{
    public class MainWindowOperations
    {
        private MainWindowViewModel mainWindowVM;
        private SerializationActions actions = new SerializationActions();
        public MainWindowOperations(MainWindowViewModel mainWindowVM)
        {
            this.mainWindowVM = mainWindowVM;
        }
        public void Exit(object param)
        {
            Window thisWindow = param as Window;
            actions.SerializeObject("users.xml", mainWindowVM);
            thisWindow.Close();
        }
        public void AddUsers(string newTxt, ObservableCollection<User> usersList)
        {
            User user = new User();
            user.Nickname = newTxt;
            usersList.Add(user);
            MessageBox.Show("Now you can chose your image!");
        }
        public void DeleteUser(object param)
        {
            ObservableCollection<User> uL = param as ObservableCollection<User>;
            uL.Remove(mainWindowVM.SelectedUser);
        }
        public void StartGame(object param)
        {
            //MainWindow wind = new MainWindow();
            //wind.DataContext = GameWindow
            //ObservableCollection<User> uL = param as ObservableCollection<User>;
            //uL.Remove(mainWindowVM.SelectedUser);
            GameWindow view = new GameWindow(mainWindowVM.SelectedUser);
            view.Show();
        }
        public string SetImageUpPath(User user)
        {
            string imgPath = "../Avatars/";
            if(user.contor < 16)
                ++user.contor;
            user.ImageSource = imgPath + user.contor.ToString() + ".jpg";
            return imgPath + user.contor.ToString() + ".jpg";
        }
        public void Serialize()
        {
            actions.SerializeObject("users.xml", mainWindowVM);
        }
        public string SetImageDownPath(User user)
        {
            string imgPath = "../Avatars/";
            if (user.contor > 1)
                --user.contor;
            user.ImageSource = imgPath + user.contor.ToString() + ".jpg";
            return imgPath + user.contor.ToString() + ".jpg";
        }
        public void HideEnterPannel(bool flag)
        {
            flag = false;
        }
    }
}
