using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    static class Utils
    {
        public static ObservableCollection<User> Initialization()
        {
            User user1 = new User();

            user1.Nickname = "andreixx2xd";

            ObservableCollection<User> users = new ObservableCollection<User>();
            users.Add(user1);

            return users;
        }
    }
}
