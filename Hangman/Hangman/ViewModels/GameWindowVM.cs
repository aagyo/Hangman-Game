using Hangman.Commands;
using Hangman.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    public class GameWindowVM : MainWindowViewModel
    {
        private GameWIndowOperations operation;
        public GameWindowVM()
        {
            if (operation == null)
            {
                operation = new GameWIndowOperations(this);
            }
        }

        private ICommand startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (startGameCommand == null)
                {
                    startGameCommand = new RelayCommand(param => { operation.StartGame(); IsStartTextVisible = false; IsNewGameVisible = true; });
                }
                return startGameCommand;
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
