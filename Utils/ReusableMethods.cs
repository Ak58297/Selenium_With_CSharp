using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWithCsharp.Utils
{
    class ReusableMethods
    {
        public string ReverseString(string GivenString)
        {
            string reversedString="";
            char[] ch = GivenString.ToCharArray();
            int size = GivenString.Length;
            for(int i=size-1;i>=0;i--)
            { 
                reversedString += ch[i];
                reversedString.Trim();
            }
            return reversedString;
        }

    }
}
