using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Hangman.Commands;
using Hangman.Models;
using Hangman.Services;

namespace Hangman.ViewModels
{
    [Serializable]
    public class MainWindowViewModel : BaseVM
    {
        [XmlArray]
        public ObservableCollection<User> UsersList { get; set; }
        private MainWindowOperations operation;

        public MainWindowViewModel()
        {
            if (operation == null)
            {
                operation = new MainWindowOperations(this);
            }
        }

        private bool canExecuteCommand = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }

            set
            {
                if (canExecuteCommand == value)
                {
                    return;
                }
                canExecuteCommand = value;
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(param => { SelectedUser = null; ImagePath = null; IsEnterVisible = false; operation.Exit(param); });
                }
                return exitCommand;
            }
        }

        private ICommand newUserCommand;
        public ICommand NewUserCommand
        {
            get
            {
                if (newUserCommand == null)
                {
                    newUserCommand = new RelayCommand(param => { SelectedUser = null; IsEnterVisible = true; });
                }
                return newUserCommand;
            }
        }

        private ICommand openGameCommand;
        public ICommand OpenGameCommand
        {
            get
            {
                if (openGameCommand == null)
                {
                    openGameCommand = new RelayCommand(operation.StartGame,param => CanExecuteCommand);
                }
                return openGameCommand;
            }
        }

        private ICommand deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                {
                    deleteUserCommand = new RelayCommand(operation.DeleteUser, param => CanExecuteCommand);
                }
                return deleteUserCommand;
            }
        }

        private ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    addUserCommand = new RelayCommand(param => {operation.AddUsers(newText, UsersList); SelectedUser = UsersList.Last(); 
                        IsEnterVisible = false; NewText = null; });
                }
                return addUserCommand;
            }
        }

        private ICommand setImageUpCommand;
        public ICommand SetImageUpCommand
        {
            get
            {
                if (setImageUpCommand == null)
                {
                    setImageUpCommand = new RelayCommand(param => { ImagePath = operation.SetImageUpPath(userSelected); operation.Serialize(); });
                }
                return setImageUpCommand;
            }
        }

        private ICommand setImageDownCommand;
        public ICommand SetImageDownCommand
        {
            get
            {
                if (setImageDownCommand == null)
                {
                    setImageDownCommand = new RelayCommand(param => { ImagePath = operation.SetImageDownPath(userSelected); operation.Serialize(); });
                }
                return setImageDownCommand;
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");

            }

        }

        private User userSelected;
        public User SelectedUser
        {
            get
            {
                return userSelected;
            }
            set
            {
                userSelected = value;
                if (userSelected != null)
                {
                    ImagePath = value.ImageSource;
                    IsEnterVisible = false;
                    IsUserSelected = true;
                }
                else
                {
                    ImagePath = null;
                    IsUserSelected = false;
                }
                OnPropertyChanged("SelectedUser");
            }

        }

        private string newText;
        public string NewText
        {
            get
            {
                return newText;
            }
            set
            {
                newText = value;
                OnPropertyChanged("NewText");
            }
        }

        private bool isUserSelected;
        public bool IsUserSelected
        {
            get { return isUserSelected; }
            set
            {
                isUserSelected = value;
                if (isUserSelected == true)
                    canExecuteCommand = true;
                else
                    canExecuteCommand = false;
                OnPropertyChanged("IsUserSelected");
            }
        }


        private bool enterPannel;
        public bool IsEnterVisible
        {
            get { return enterPannel; }
            set
            {
                enterPannel = value;
                OnPropertyChanged("IsEnterVisible");
            }
        }
    }
}
