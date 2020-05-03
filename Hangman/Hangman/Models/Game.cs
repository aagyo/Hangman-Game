using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Hangman.Models
{
    [Serializable]
    public class Game : BaseVM
    {
        public List<string> categories = new List<string>();
        public int levelContor = 1;
        public int lastLvl;
        public int numberOfX = 0;
        public string wordToGuess;
        public int secondsRemaining;
        public DispatcherTimer timer = new DispatcherTimer();
        public int delay = 45;
        public DateTime deadline;

        public Game()
        {
            CurrentImage = "../HangmanFrames/1.jpg";
            secondsRemaining = 0;
            fillCategories();
        }

        private string currentImage;
        public string CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        private string currentCategory;
        public string CurrentCategory
        {
            get { return currentCategory; }
            set
            {
                if (value.Contains(" : "))
                    currentCategory = value;
                else
                    currentCategory = value + " : ";
                OnPropertyChanged("CurrentCategory");
            }
        }

        private string secondsRemainingStr;
        public string SecondsRemainingStr
        {
            get { return secondsRemainingStr; }
            set
            {
                secondsRemainingStr = value;
                OnPropertyChanged("SecondsRemainingStr");
            }
        }

        private string maskedWord;
        public string MaskedWord
        {
            get { return maskedWord; }
            set
            {
                maskedWord = value;
                OnPropertyChanged("MaskedWord");
            }
        }

        private void fillCategories()
        {
            categories.Add("../../Categories/cars.txt");
            categories.Add("../../Categories/dogbreeds.txt");
            categories.Add("../../Categories/cities.txt");
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
    }
}
