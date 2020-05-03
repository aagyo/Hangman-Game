using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (gameVM.IsStartTextVisible == false)
                ConfirmationMessage("lost");
            else
                cG.CurrentLevel = "Level 1";
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
            SavedMessage();
        }
        private void ResetAll()
        { 
            gameVM.IsNewGameVisible = false;
            gameVM.IsStartTextVisible = true;
            gameVM.IsOpenEnabled = true;
            gameVM.CurrentGame.lastLvl = 0;
            gameVM.CurrentGame.levelContor = 1;
            gameVM.CurrentGame.CurrentLevel = "";
            gameVM.CurrentGame.CurrentCategory = "";
            gameVM.CurrentGame.CurrentImage = "../HangmanFrames/1.jpg";
            gameVM.CurrentGame.timer = new DispatcherTimer();
            gameVM.wasStartPressed = true;
            gameVM.IsNewGameEnabled = false;
            gameVM.IsSaveEnabled = false;
            FillDict();
            ResetWrongBtns();
            UncheckAll();
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
                ConfirmationMessage("lost");
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
            gameVM.CurrentGame.numberOfX = 0;
        }
        private void ResetNextLvl(Game cG)
        {
            ResetWrongBtns();
            cG.CurrentImage = "../HangmanFrames/1.jpg";
            cG.numberOfX = 0;
            cG.delay = 45;
            FillDict();
            StartTimer();
        }
        public string BtnCont(object param)
        {
            Button btn = param as Button;
            return btn.Content.ToString();
        }
        public void EnableOpen(object param)
        {
            MenuItem mI = param as MenuItem;
            mI.IsEnabled = true;
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
            gameVM.IsDogBreedsChecked = false;
            gameVM.IsCitiesChecked = false;
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
        private void StatsGameCount(User sU, string state)
        {
            Game cG = gameVM.CurrentGame;
            if (sU.stats == null)
            {
                Statistics newStat = new Statistics();
                newStat.Category = cG.CurrentCategory.Substring(0, cG.CurrentCategory.Length - 3);
                if (state == "lost")
                    newStat.NrLost++;
                else
                    newStat.NrWon++;
                newStat.NrTotal = newStat.NrWon + newStat.NrLost;
                ObservableCollection<Statistics> sts = new ObservableCollection<Statistics>();
                sts.Add(newStat);
                sU.stats = sts;
            }
            else
            {
                bool found = false;
                foreach (Statistics stats in sU.stats.ToList())
                {
                    if (stats.Category == cG.CurrentCategory.Substring(0, cG.CurrentCategory.Length - 3))
                    {
                        found = true;
                        if (state == "lost")
                            stats.NrLost++;
                        else
                            stats.NrWon++;
                        stats.NrTotal = stats.NrWon + stats.NrLost;
                    }
                }
                if(found == false)
                {
                    Statistics newStat = new Statistics();
                    newStat.Category = cG.CurrentCategory.Substring(0, cG.CurrentCategory.Length - 3);
                    if (state == "lost")
                        newStat.NrLost++;
                    else
                        newStat.NrWon++;
                    newStat.NrTotal = newStat.NrWon + newStat.NrLost;
                    sU.stats.Add(newStat);
                }
            }
        }
        private void StopTimer()
        {
            gameVM.CurrentGame.timer.Stop();
            gameVM.CurrentGame.timer.IsEnabled = false;
            gameVM.CurrentGame.delay = 45;
        }
        private void SavedMessage()
        {
            StopTimer();
            MessageBoxResult result = MessageBox.Show("You saved the game! \n Would you like to exit now?",
                                        "Confirmation",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == gameVM) item.Close();
                }
            }
            else
            {
                ResetAll();
            }
        }
        private void ConfirmationMessage(string state)
        {
            string winMessage = "You win :) \n Would you like to play again?";
            string lostMessage = "You lost :( \n Would you like to retry?";
            string stateMessage = "";
            switch (state)
            {
                case "win":
                    stateMessage = winMessage;
                    break;    
                case "lost":
                    stateMessage = lostMessage;
                    break;
                case "save":
                    SavedMessage();
                    break;
                default:
                    break;
            }

            if(state == "win" || state == "lost")
            {
                StopTimer();
                StatsGameCount(gameVM.SelectedUser, state);
                MessageBoxResult result = MessageBox.Show($"{stateMessage}",
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
        }
        private void UnmaskWord(string content)
        {
            Game cG = gameVM.CurrentGame;
            User sU = gameVM.SelectedUser;
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
                    ConfirmationMessage("lost");
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
                        ConfirmationMessage("win");
                        //WinMessage();
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
        private void RandomWordSelect(string category,Game cG)
        {
            if (cG.lastLvl == cG.levelContor && cG.secondsRemaining!=0)
            {
                UncheckAll();
                ConfirmationMessage("lost");
            }
            else
                cG.lastLvl = cG.levelContor;
            cG.CurrentCategory = category;

            if (category != "All categories")
            {
                string matchString = $"../../Categories/{category.Replace(" ", string.Empty).ToLower()}.txt";
                int index = cG.categories.FindIndex(x => x == matchString);
                var lines = File.ReadAllLines(cG.categories[index]);
                var randomNr = RandomNumber(0, lines.Length - 1);
                cG.wordToGuess = lines[randomNr];
                MaskWord(cG);
            }
            else
            {
                int randomFolder = RandomNumber(0, cG.categories.Count() - 1);
                var lines = File.ReadAllLines(cG.categories[randomFolder]);
                var randomNr = RandomNumber(0, lines.Length - 1);
                cG.wordToGuess = lines[randomNr];
                MaskWord(cG);
            }
        }
        public void ChoseRandomWord(string category, Game cG)
        {
            switch (category)
            {
                case "All categories":
                    RandomWordSelect(category, cG);
                    break;
                case "Cars":
                    RandomWordSelect(category, cG);
                    break;
                case "Dog Breeds":
                    RandomWordSelect(category, cG);
                    break;
                case "Cities":
                    RandomWordSelect(category, cG);
                    break;
                default:
                    break;
            }
        }
    }
}
