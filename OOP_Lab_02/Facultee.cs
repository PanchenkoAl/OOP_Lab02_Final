using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab_02
{
    public class Facultee
    {
        public string Departerment { get; set; }
        public string Part { get; set; }
        public Facultee()
        {

        }
        public Facultee(string departerment, string part)
        {
            Departerment = departerment;
            Part = part;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", Departerment, Part);
        }
    }
}
