using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab_02
{
    public class Employee : ISerializable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Facultee Facultee { get; set; }
        public string Laboratory { get; set; }
        public Title Title { get; set; }

        public Employee()
        {

        }
        public Employee(string id, string name, Facultee facultee, string laboratory, Title title)
        {
            Id = id;
            Name = name;
            Facultee = facultee;
            Laboratory = laboratory;
            Title = title;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}", Id, Name, Facultee, Laboratory, Title);
        }
    }
}
