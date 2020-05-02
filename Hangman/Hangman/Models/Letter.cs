using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    [Serializable]
    public class Letter
    {
        [XmlAttribute]
        public string key;
        [XmlAttribute]
        public bool value;
    }
}
