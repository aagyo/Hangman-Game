using Hangman.Commands;
using Hangman.Models;
using Hangman.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Hangman.ViewModels
{
    public class GameWindowVM : BaseVM
    {
        private GameWIndowOperations operation;
        public User selectedUser;
        public Game currentGame;
        public DispatcherTimer timer = new DispatcherTimer();
        public int delay = 45;
        public DateTime deadline;
        public GameWindowVM(User _selectedUser)
        {
            if (operation == null)
            {
                operation = new GameWIndowOperations(this);
                selectedUser = _selectedUser;
                UserName = selectedUser.Nickname;
                ImageSource = selectedUser.ImageSource;
            }
        }

        private ICommand startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (startGameCommand == null)
                {
                    startGameCommand = new RelayCommand(param => { currentGame = operation.StartGame(); IsStartTextVisible = false;
                        IsNewGameVisible = true; CurrentLevel = currentGame.CurrentLevel;
                        timer.Tick += (sender, args) => operation.TimerTick(); operation.StartTimer();
                    });
                }
                return startGameCommand;
            }
        }

        private string secondsRemaining;
        public string SecondsRemaining
        {
            get { return secondsRemaining; }
            set
            {
                secondsRemaining = value;
                OnPropertyChanged("SecondsRemaining");
            }
        }
        private string currentLevel;
        public string CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;
                OnPropertyChanged("CurrentLevel");
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private bool newGamePannel = false;
        public bool IsNewGameVisible
        {
            get { return newGamePannel; }
            set
            {
                newGamePannel = value;
                OnPropertyChanged("IsNewGameVisible");
            }
        }
        private bool startTextVisible = true;
        public bool IsStartTextVisible
        {
            get { return startTextVisible; }
            set
            {
                startTextVisible = value;
                OnPropertyChanged("IsStartTextVisible");
            }
        }
    }
}
