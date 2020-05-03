using Hangman.Commands;
using Hangman.Models;
using Hangman.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace Hangman.ViewModels
{
    [Serializable]
    public class GameWindowVM : BaseVM
    {
        private GameWIndowOperations operation;
        [XmlIgnore]
        public Dictionary<string, bool> alphabet = new Dictionary<string, bool>();
        [XmlArray]
        public Letter[] Alphabet
        {
            get
            {
                return alphabet.Select(kv => new Letter() { key = kv.Key, value = kv.Value }).ToArray();
            }
            set
            {
                alphabet = value.ToDictionary(i => i.key, i => i.value);
            }
        }

        public GameWindowVM()
        {
            if (operation == null)
            {
                operation = new GameWIndowOperations(this);
            }
        }
        public GameWindowVM(User _selectedUser)
        {
            if (operation == null)
            {
                operation = new GameWIndowOperations(this);
                operation.FillDict();
                SelectedUser = _selectedUser;
                CurrentGame = new Game();
            }
        }

        #region ICommands
        public bool wasStartPressed = false;
        private ICommand startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (startGameCommand == null)
                {
                    startGameCommand = new RelayCommand(param =>
                    {
                        operation.StartGame(CurrentGame);
                        if (wasStartPressed == false)
                        {
                            IsStartTextVisible = false;
                            IsSaveEnabled = true;
                            IsNewGameVisible = true;
                            operation.StartTimer();
                        }
                        else
                            IsSaveEnabled = false;
                    });
                }
                return startGameCommand;
            }
        }
        public ICommand CheckCategoryCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.ChoseRandomWord("All categories", currentGame);
                    operation.UncheckAll();
                    IsAllChecked = true;
                });
            }
        }
        public ICommand CheckCarsCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.ChoseRandomWord("Cars", currentGame);
                    operation.UncheckAll();
                    IsCarsChecked = true;
                });
            }
        }
        public ICommand CheckDogBreeds
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.ChoseRandomWord("Dog Breeds", currentGame);
                    operation.UncheckAll();
                    IsDogBreedsChecked = true;
                });
            }
        }
        public ICommand CheckCities
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.ChoseRandomWord("Cities", currentGame);
                    operation.UncheckAll();
                    IsCitiesChecked = true;
                });
            }
        }
        public ICommand OpenClick { get { return new RelayCommand(param => IsOpenEnabled = false); } }
        public ICommand ButtonClicked { get { return new RelayCommand(operation.ClickedButton, param => alphabet[operation.BtnCont(param)]); } }
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(param =>
                {
                    operation.SaveCurrentGame(selectedUser);
                });
            }
        }
        #endregion

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

        #region Properties
        private bool isAllChecked;
        public bool IsAllChecked
        {
            get { return isAllChecked; }
            set
            {
                isAllChecked = value;
                if (value == true)
                {
                    IsNewGameEnabled = true;
                    wasStartPressed = false;
                }
                OnPropertyChanged("IsAllChecked");
            }
        }

        private bool isOpenEnabled = true;
        public bool IsOpenEnabled
        {
            get { return isOpenEnabled; }
            set
            {
                isOpenEnabled = value;
                OnPropertyChanged("IsOpenEnabled");
            }
        }

        private bool isNewGameEnabled = false;
        public bool IsNewGameEnabled
        {
            get { return isNewGameEnabled; }
            set
            {
                isNewGameEnabled = value;
                OnPropertyChanged("IsNewGameEnabled");
            }
        }
        public bool isSaveEnabled = false;
        public bool IsSaveEnabled
        {
            get { return isSaveEnabled; }
            set
            {
                isSaveEnabled = value;
                OnPropertyChanged("IsSaveEnabled");
            }
        }

        private bool isCarsChecked = false;
        public bool IsCarsChecked
        {
            get { return isCarsChecked; }
            set
            {
                isCarsChecked = value;
                if (value == true)
                {
                    IsNewGameEnabled = true;
                    wasStartPressed = false;
                }
                OnPropertyChanged("IsCarsChecked");
            }
        }

        private bool isCitiesChecked = false;
        public bool IsCitiesChecked
        {
            get { return isCitiesChecked; }
            set
            {
                isCitiesChecked = value;
                if (value == true)
                {
                    IsNewGameEnabled = true;
                    wasStartPressed = false;
                }
                OnPropertyChanged("IsCitiesChecked");
            }
        }

        private bool isDogBreedsChecked = false;
        public bool IsDogBreedsChecked
        {
            get { return isDogBreedsChecked; }
            set
            {
                isDogBreedsChecked = value;
                if (value == true)
                {
                    IsNewGameEnabled = true;
                    wasStartPressed = false;
                }
                OnPropertyChanged("IsDogBreedsChecked");
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
                if (currentGame.secondsRemaining != 0)
                {
                    operation.StartTimer();
                }
                OnPropertyChanged("CurrentGame");
            }
        }
        #endregion
    }
}
