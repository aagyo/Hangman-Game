using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public void StartGame(Game cG)
        {
            //gameVM.CurrentGame = new Game();
            cG.CurrentLevel = "Level 1";
        }
        public void StartTimer()
        {
            gameVM.CurrentGame.deadline = DateTime.Now.AddSeconds(gameVM.CurrentGame.delay);
            gameVM.CurrentGame.timer.Start();
        }

        public void TimerTick()
        {
            int secondsRemaining = (gameVM.CurrentGame.deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                gameVM.CurrentGame.timer.Stop();
                gameVM.CurrentGame.timer.IsEnabled = false;
                gameVM.CurrentGame.delay = 30;
            }
            else
            {
                gameVM.CurrentGame.SecondsRemainingStr = secondsRemaining.ToString();
            }
        }

        private void EnableBtns()
        {
            foreach(Button btn in gameVM.pressedButtons)
            {
                btn.IsEnabled = true;
            }
            gameVM.pressedButtons.Clear();
        }

        public void ClickedButton(object param)
        {
            Button btn = param as Button;
            gameVM.pressedButtons.Add(btn);
            btn.IsEnabled = false;
            UnmaskWord(btn.Content.ToString());
        }

        public void UncheckAll()
        {
            gameVM.IsAllChecked = false;
            gameVM.IsCarsChecked = false;
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        private bool CheckWinStatus(Game cG)
        {
            if (cG.wordToGuess.ToLower() == cG.MaskedWord.Replace(" ", string.Empty).ToLower())
                return true;
            else
                return false;
        }

        private void UnmaskWord(string content)
        {
            Game cG = gameVM.CurrentGame;
            string matchString = Regex.Escape(content);

            var _match = Regex.Match(cG.wordToGuess, matchString, RegexOptions.IgnoreCase);

            if (!_match.Success)
            {
                //imagine si x-uri
        
            }
            else
            {
                string word = cG.MaskedWord;
                foreach (Match match in Regex.Matches(cG.wordToGuess, matchString, RegexOptions.IgnoreCase))
                {
                    word = word.Remove(match.Index * 2, 1).Insert(match.Index * 2, content);
                    cG.MaskedWord = word;
                }
                if (CheckWinStatus(cG) == true)
                {
                    if (cG.levelContor != 5)
                    {
                        cG.levelContor++;
                        cG.CurrentLevel = "Level " + cG.levelContor.ToString();
                        ChoseRandomWord(cG.CurrentCategory.Substring(0,cG.CurrentCategory.Length - 3), cG);
                        EnableBtns();
                    }
                }
            }
        }
        private void MaskWord(Game cG)
        {
            if (cG.wordToGuess != null)
                cG.MaskedWord = new Regex("([a-zA-z])").Replace(cG.wordToGuess, "_ ");
        }

        public void ChoseRandomWord(string category, Game cG)
        {
            switch (category)
            {
                case "Cars":
                    cG.CurrentCategory = category;
                    var lines = File.ReadAllLines(cG.categories[0]);
                    var randomNr = RandomNumber(0, lines.Length - 1);
                    cG.wordToGuess = lines[randomNr];
                    MaskWord(cG);
                    break;
                default:
                    break;
            }
        }
    }
}
