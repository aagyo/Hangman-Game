using Hangman.Models;
using Hangman.ViewModels;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hangman.Services
{
    public class MainWindowOperations
    {
        private MainWindowViewModel mainWindowVM;
        private SerializationActions<MainWindowViewModel> actions = new SerializationActions<MainWindowViewModel>();
        public MainWindowOperations(MainWindowViewModel mainWindowVM)
        {
            this.mainWindowVM = mainWindowVM;
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
            File.Delete($"{mainWindowVM.SelectedUser.Nickname}Saved.xml");
            File.Delete($"{mainWindowVM.SelectedUser.Nickname}Stats.xml");
            uL.Remove(mainWindowVM.SelectedUser);
        }
        public void StartGame(object param)
        {
            mainWindowVM.SelectedUser = null;
            mainWindowVM.ImagePath = null;
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
