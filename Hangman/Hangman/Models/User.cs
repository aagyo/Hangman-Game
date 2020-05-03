using Hangman.Commands;
using Hangman.Services;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Hangman.Models
{
    [Serializable]
    public class User : BaseVM
    {
        public int contor = 1;
        [XmlAttribute]
        private string nickname;
        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
                OnPropertyChanged("Nickname");
            }
        }

        [XmlAttribute]
        private string imageSource = "../Avatars/1.jpg";
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        [XmlArray]
        public ObservableCollection<Statistics> stats { get; set; }
    }
}
