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
        private SerializationActions<GameWindowVM> actions = new SerializationActions<GameWindowVM>();
        public GameWIndowOperations(GameWindowVM gameWindowVM)
        {
            this.gameVM = gameWindowVM;
        }

        public void StartGame(Game cG)
        {
            //gameVM.CurrentGame = new Game();
            cG.CurrentLevel = "Level 1";
            //FillDict();
        }

        public void FillDict()
        {
            for (char c = 'A'; c <= 'Z'; c++)
                gameVM.alphabet[c.ToString()] = true;
        }

        public void SaveCurrentGame(User sU)
        {
            gameVM.CurrentGame.delay = gameVM.CurrentGame.secondsRemaining;
            actions.SerializeObject($"{sU.Nickname.ToLower()}Saved.xml", gameVM);
        }

        private void ResetAll()
        {
            gameVM.CurrentGame = new Game();
            gameVM.IsNewGameVisible = false;
            gameVM.IsStartTextVisible = true;
            gameVM.IsOpenEnabled = true;
            FillDict();
            ResetWrongBtns();
            ResetCheckedCategory();
        }

        public void StartTimer()
        {
            gameVM.CurrentGame.deadline = DateTime.Now.AddSeconds(gameVM.CurrentGame.delay);
            gameVM.CurrentGame.timer.Start();
            gameVM.CurrentGame.timer.Tick += new EventHandler(TimerTick);
        }

        public void TimerTick(object send, EventArgs e)
        {
            gameVM.CurrentGame.secondsRemaining = (gameVM.CurrentGame.deadline - DateTime.Now).Seconds;
            if (gameVM.CurrentGame.secondsRemaining == -1)
            {
                gameVM.CurrentGame.timer.Stop();
                gameVM.CurrentGame.timer.IsEnabled = false;
                gameVM.CurrentGame.delay = 45;
                LostMessage();
            }
            else
            {
                gameVM.CurrentGame.SecondsRemainingStr = gameVM.CurrentGame.secondsRemaining.ToString();
            }
        }

        private void ResetWrongBtns()
        {
            gameVM.WrongClick1 = "";
            gameVM.WrongClick2 = "";
            gameVM.WrongClick3 = "";
            gameVM.WrongClick4 = "";
            gameVM.WrongClick5 = "";
            gameVM.WrongClick6 = "";
        }
        private void ResetNextLvl(Game cG)
        {
            ResetWrongBtns();
            cG.CurrentImage = "../HangmanFrames/1.jpg";
            cG.numberOfX = 0;
            FillDict();
            cG.delay = 45;
            StartTimer();
        }
        private void ResetCheckedCategory()
        {
            gameVM.IsAllChecked = false;
            gameVM.IsCarsChecked = false;
        }

        public string BtnCont(object param)
        {
            Button btn = param as Button;
            return btn.Content.ToString();
        }

        public void ClickedButton(object param)
        {
            Button btn = param as Button;
            gameVM.alphabet[btn.Content.ToString()] = false;
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
            if (cG.wordToGuess.Replace(" ", string.Empty).ToLower() == cG.MaskedWord.Replace(" ", string.Empty).ToLower())
                return true;
            else
                return false;
        }

        private void LostMessage()
        {
            MessageBoxResult result = MessageBox.Show("You lost :( \n Would you like to retry?",
                                        "Confirmation",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ResetAll();
            }
            else
            {
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == gameVM) item.Close();
                }
            }
        }
        private void WinMessage()
        {
            MessageBoxResult result = MessageBox.Show("You win :) \n Would you like to play again?",
                                       "Confirmation",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ResetAll();
            }
            else
            {
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == gameVM) item.Close();
                }
            }
        }

        private void UnmaskWord(string content)
        {
            Game cG = gameVM.CurrentGame;
            string matchString = Regex.Escape(content);

            var _match = Regex.Match(cG.wordToGuess, matchString, RegexOptions.IgnoreCase);

            if (!_match.Success)
            {
                ++cG.numberOfX;
                switch (cG.numberOfX)
                {
                    case 1:
                        cG.CurrentImage = "../HangmanFrames/2.jpg";
                        gameVM.WrongClick1 = "X";
                        break;
                    case 2:
                        cG.CurrentImage = "../HangmanFrames/3.jpg";
                        gameVM.WrongClick2 = "X";
                        break;
                    case 3:
                        cG.CurrentImage = "../HangmanFrames/4.jpg";
                        gameVM.WrongClick3 = "X";
                        break;
                    case 4:
                        cG.CurrentImage = "../HangmanFrames/5.jpg";
                        gameVM.WrongClick4 = "X";
                        break;
                    case 5:
                        cG.CurrentImage = "../HangmanFrames/6.jpg";
                        gameVM.WrongClick5 = "X";
                        break;
                    case 6:
                        cG.CurrentImage = "../HangmanFrames/7.jpg";
                        gameVM.WrongClick6 = "X";
                        break;
                    default:
                        break;
                }
                if (cG.numberOfX == 6)
                {
                    cG.timer.Stop();
                    LostMessage();
                }
            }
            else
            {
                string word = cG.MaskedWord;
                foreach (Match match in Regex.Matches(cG.wordToGuess, matchString, RegexOptions.IgnoreCase))
                {
                    word = word.Remove(match.Index*2, 1).Insert(match.Index*2, content);
                    cG.MaskedWord = word;
                }
                if (CheckWinStatus(cG) == true)
                {
                    if (cG.levelContor != 5 && cG.numberOfX != 6)
                    {
                        cG.levelContor++;
                        cG.CurrentLevel = "Level " + cG.levelContor.ToString();
                        ChoseRandomWord(cG.CurrentCategory.Substring(0, cG.CurrentCategory.Length - 3), cG);
                        ResetNextLvl(cG);
                    }
                    else
                    {
                        cG.timer.Stop();
                        WinMessage();
                    }
                }
            }
        }
        private void MaskWord(Game cG)
        {
            if (cG.wordToGuess != null)
            {
                string wordCopy = cG.wordToGuess;
                cG.wordToGuess = cG.wordToGuess.Replace(" ", "  ");
                cG.wordToGuess = cG.wordToGuess.Replace("'", "' ");
                cG.wordToGuess = cG.wordToGuess.Replace("-", "- ");
                cG.MaskedWord = new Regex("([a-zA-z])").Replace(cG.wordToGuess, "_ ");
                cG.wordToGuess = wordCopy;
            }
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
