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
        public List<Button> pressedButtons = new List<Button>();
        public GameWindowVM(User _selectedUser)
        {
            if (operation == null)
            {
                operation = new GameWIndowOperations(this);
                SelectedUser = _selectedUser;
                CurrentGame = new Game();
            }
        }


        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }


        private Game currentGame;
        public Game CurrentGame
        {
            get { return currentGame; }
            set
            {
                currentGame = value;
                OnPropertyChanged("CurrentGame");
            }
        }

        private ICommand startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (startGameCommand == null)
                {
                    startGameCommand = new RelayCommand(param =>
                    {
                        operation.StartGame(CurrentGame); IsStartTextVisible = false;
                        IsNewGameVisible = true;
                        CurrentGame.timer.Tick += (sender, args) => operation.TimerTick(); operation.StartTimer();
                    });
                }
                return startGameCommand;
            }
        }

        public ICommand CheckCategoryCommand { get { return new RelayCommand(param => { operation.UncheckAll(); IsAllChecked = true; }); } }
        public ICommand CheckCarsCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.UncheckAll(); IsCarsChecked = true;
                    operation.ChoseRandomWord("Cars", currentGame);
                });
            }
        }

        public ICommand ButtonClicked
        {
            get { return new RelayCommand(param => operation.ClickedButton(param)); }
        }

        #region WrongClicks
        private string wrongClick1;
        public string WrongClick1
        {
            get { return wrongClick1; }
            set
            {
                wrongClick1 = value;
                OnPropertyChanged("WrongClick1");
            }
        }
        private string wrongClick2;
        public string WrongClick2
        {
            get { return wrongClick2; }
            set
            {
                wrongClick2 = value;
                OnPropertyChanged("WrongClick2");
            }
        }
        private string wrongClick3;
        public string WrongClick3
        {
            get { return wrongClick3; }
            set
            {
                wrongClick3 = value;
                OnPropertyChanged("WrongClick3");
            }
        }
        private string wrongClick4;
        public string WrongClick4
        {
            get { return wrongClick4; }
            set
            {
                wrongClick4 = value;
                OnPropertyChanged("WrongClick4");
            }
        }
        private string wrongClick5;
        public string WrongClick5
        {
            get { return wrongClick5; }
            set
            {
                wrongClick5 = value;
                OnPropertyChanged("WrongClick5");
            }
        }
        private string wrongClick6;
        public string WrongClick6
        {
            get { return wrongClick6; }
            set
            {
                wrongClick6 = value;
                OnPropertyChanged("WrongClick6");
            }
        }
        #endregion

        private bool isAllChecked;
        public bool IsAllChecked
        {
            get { return isAllChecked; }
            set
            {
                isAllChecked = value;
                OnPropertyChanged("IsAllChecked");
            }
        }

        private bool isCarsChecked;
        public bool IsCarsChecked
        {
            get { return isCarsChecked; }
            set
            {
                isCarsChecked = value;
                OnPropertyChanged("IsCarsChecked");
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
