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
    public class Game : BaseVM
    {
        public List<string> categories = new List<string>();
        public int levelContor = 1;
        public int numberOfX = 0;
        public string wordToGuess;

        public Game()
        {
            //CurrentLevel = "Level 1";
            CurrentImage = "../HangmanFrames/1.jpg";
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
                currentCategory = value + " : ";
                OnPropertyChanged("CurrentCategory");
            }
        }



        public DispatcherTimer timer = new DispatcherTimer();
        public int delay = 45;
        public DateTime deadline;

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
