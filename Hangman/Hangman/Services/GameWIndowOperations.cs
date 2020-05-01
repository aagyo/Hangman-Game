using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

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
        public Game StartGame()
        {
            Game game = new Game();
            return game;
        }

        public void StartTimer()
        {
            gameVM.deadline = DateTime.Now.AddSeconds(gameVM.delay);
            gameVM.timer.Start();
        }

        public void TimerTick()
        {
            int secondsRemaining = (gameVM.deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                gameVM.timer.Stop();
                gameVM.timer.IsEnabled = false;
                gameVM.delay = 30;
            }
            else
            {
                gameVM.SecondsRemaining = secondsRemaining.ToString();
            }
        }
    }
}
