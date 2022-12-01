using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab_02
{
    public class Title
    {
        public string Start { get; set; }
        public string End { get; set; }
        public Title()
        {

        }
        public Title(string start, string end)
        {
            Start = start;
            End = end;
        }
        public override string ToString()
        {
            return String.Format("{0}, {1}", Start, End);
        }
    }
}
