using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hangman.Models
{
    class SerializationActions<T>
    {
        public void SerializeObject(string xmlFileName, T entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(T));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }

        public T DeserializeObject(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(T));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return (T)entity;
        }
    }
}
