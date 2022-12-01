using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OOP_Lab_02
{
    internal class LINQSerializer : ISerializable
    {
        public override void Serialize(object obj)
        {
            base.Serialize(obj);
        }
        public override void Deserialize(string xmlUrl, out List<Employee>? EmployeesList)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Employee[]));
            Employee[] EmployeeArray;
            using (FileStream stream = new FileStream(xmlUrl, FileMode.Open))
            {
                EmployeeArray = (Employee[])deserializer.Deserialize(stream);
            }
            EmployeesList = new List<Employee>();
            foreach(Employee Employee in EmployeeArray)
            {
                EmployeesList.Add(Employee);
            }
        }
        public override void FindByTagPhrase(string xmlUrl, string tag, out List<Employee> EL)
        {
            string tPhrase = null;
            string tAttribute = null;
            if (tag is null)
            {
                MessageBox.Show("Tag is Empty");
                EL = new List<Employee>();
                return;
            }
            if (xmlUrl == null)
            {
                MessageBox.Show("File is not open");
            }
            
            for (int i = 0; i < tag.Length; i++)
            {

                if (tag[i] == '(')
                {
                    while (tag[i + 1] != ')')
                    {
                        ++i;
                        tAttribute += tag[i];
                    }
                    break;
                }
                else
                {
                    tPhrase += tag[i];
                }
            }
            EL = new List<Employee>();
            try
            {
                XDocument doc = XDocument.Load(xmlUrl);
                XElement Employees = doc.Element("ArrayOfEmployee");
                if (Employees is not null)
                {
                    foreach (XElement employee in Employees.Elements("Employee"))
                    {
                        XElement tEl = employee.Element(tAttribute);
                        if (tEl.Value.Contains(tPhrase))
                        {
                            XElement id = employee.Element("Id");
                            XElement name = employee.Element("Name");
                            XElement fac = employee.Element("Facultee");
                            XElement dep = fac.Element("Departerment");
                            XElement part = fac.Element("Part");
                            XElement lab = employee.Element("Laboratory");
                            XElement tit = employee.Element("Title");
                            XElement start = tit.Element("Start");
                            XElement end = tit.Element("End");
                            Title title = new Title(start.Value, end.Value);
                            Facultee facultee = new Facultee(dep.Value, part.Value);
                            Employee emp = new Employee(id.Value, name.Value, facultee, lab.Value, title);
                            EL.Add(emp);
                        }
                    }
                }
            }
            catch(System.ArgumentException)
            {
                MessageBox.Show("Unable to find");
            }
            
        }
    }
}
