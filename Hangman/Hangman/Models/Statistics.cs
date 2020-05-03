using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    [Serializable]
    public class Statistics : BaseVM
    {
        [XmlAttribute]
        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        [XmlAttribute]
        private int nrWon;
        public int NrWon
        {
            get { return nrWon; }
            set
            {
                nrWon = value;
                OnPropertyChanged("NrWon");
            }
        }

        [XmlAttribute]
        private int nrLost;
        public int NrLost
        {
            get { return nrLost; }
            set
            {
                nrLost = value;
                OnPropertyChanged("NrLost");
            }
        }

        [XmlAttribute]
        private int nrTotal;
        public int NrTotal
        {
            get { return nrTotal; }
            set
            {
                nrTotal = value;
                OnPropertyChanged("NrTotal");
            }
        }
    }
}
