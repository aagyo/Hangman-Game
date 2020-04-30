using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Services
{
    class GameWIndowOperations
    {
        private GameWindowVM gameVM;
        //private SerializationActions actions = new SerializationActions();
        public GameWIndowOperations(GameWindowVM gameWindowVM)
        {
            this.gameVM = gameWindowVM;
        }
        public void StartGame()
        {
            Game game;
        }
    }
}
