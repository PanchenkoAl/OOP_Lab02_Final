using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab_02
{
    public abstract class ISerializable
    {
        public virtual void Serialize(object obj)
        {

        }
        public virtual void Deserialize(string xmlUrl, out List<Employee>? EmployeesList)
        {
            EmployeesList = new List<Employee>();
        }

        public virtual void FindByTagPhrase(string xmlUrl, string tag, out List<Employee> EL)
        {
            EL = new List<Employee>();
        }
    }
}
