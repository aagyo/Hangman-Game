using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class Game : BaseVM
    {
        public Game()
        {
            currentLevel = "Level 1";
        }
        public int levelContor = 1;
        public string category;
        public int delay;
        public int currentImage;
        public int numberOfX;
        public string wordToGuess;
        public string maskedWord;
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
