using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringIntern
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "jakis_string";
            string str2 = "jakis_string";
            string str3 = new StringBuilder().Append("jakis").Append("_").Append("string").ToString();
            string str4 = String.Intern(str3);

            Console.WriteLine((object) str1 == (object) str2);
            Console.WriteLine((object) str2 == (object) str3);
            Console.WriteLine((object) str1 == (object) str4);

            Console.ReadLine();
        }
    }
}