using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Services
{
    class MyValidators
    {
        public static bool CanExecuteDeleteOrStart(double firstValue, double secondValue)
        {
            if (firstValue == 0 || secondValue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
