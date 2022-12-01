using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace OOP_Lab_02
{
    internal class SAXSerializer : ISerializable
    {
        public override void Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee[]));

            using (FileStream fs = new FileStream("Employee.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, obj);
                Console.WriteLine("Object was Serialized");
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
            if (xmlUrl is null)
            {
                MessageBox.Show("File is not open");
                EL = new List<Employee>();
                return;
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
            MessageBox.Show(tAttribute + " " + tPhrase);
            EL = new List<Employee>();
            try
            {
                using (XmlReader xr = XmlReader.Create(xmlUrl))
                {
                    bool flg = false;
                    string element = "";
                    string Id = "";
                    string Name = "";
                    string department = "";
                    string part = "";
                    string Laboratory = "";
                    string start = "";
                    string end = "";
                    Title Title = new Title();
                    Facultee Facultee = new Facultee();
                    while (xr.Read())
                    {

                        if (xr.NodeType == XmlNodeType.Element)
                        {
                            element = xr.Name;
                        }
                        else if (xr.NodeType == XmlNodeType.Text)
                        {
                            switch (element)
                            {
                                case "Id":
                                    //MessageBox.Show(tAttribute + " " + xr.Value + " " + tPhrase);
                                    if (tAttribute == "Id" && xr.Value.Contains(tPhrase))
                                    {
                                        flg = true;
                                        //MessageBox.Show(flg.ToString());
                                    }
                                    Id = xr.Value;
                                    break;
                                case "Name":
                                    if (tAttribute == "Name" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    Name = xr.Value;
                                    break;
                                case "Departerment":
                                    if (tAttribute == "Department" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    department = xr.Value;
                                    break;
                                case "Part":
                                    if (tAttribute == "Part" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    part = xr.Value;
                                    break;
                                case "Laboratory":
                                    if (tAttribute == "Laboratory" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    Laboratory = xr.Value;
                                    break;
                                case "Start":
                                    if (tAttribute == "Start" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    start = xr.Value;
                                    break;
                                case "End":
                                    if (tAttribute == "End" && xr.Value.Contains(tPhrase))
                                        flg = true;
                                    end = xr.Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if ((xr.NodeType == XmlNodeType.EndElement))
                        {
                            element = xr.Name;
                            switch (element)
                            {
                                case "Title":
                                    Title = new Title(start, end);
                                    break;
                                case "Facultee":
                                    Facultee = new Facultee(department, part);
                                    break;
                                case "Employee":
                                    //MessageBox.Show(flg.ToString());
                                    if (flg)
                                    {
                                        EL.Add(new Employee(Id, Name, Facultee, Laboratory, Title));
                                        flg = false;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch(System.ArgumentException)
            {
                MessageBox.Show("Unable to find.");
            }
            
        }
        public override void Deserialize(string xmlUrl, out List<Employee>? EmployeesList)
        {
            EmployeesList = new List<Employee>();
            using (XmlReader xr = XmlReader.Create(xmlUrl))
            {
                string element = "";
                string Id = "";
                string Name = "";
                string department = "";
                string part = "";
                string Laboratory = "";
                string start = "";
                string end = "";
                Title Title = new Title();
                Facultee Facultee = new Facultee();
                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        element = xr.Name;
                    }
                    else if (xr.NodeType == XmlNodeType.Text)
                    {
                        switch (element)
                        {
                            case "Id":
                                Id = xr.Value;
                                break;
                            case "Name":
                                Name = xr.Value;
                                break;
                            case "Departerment":
                                department = xr.Value;
                                break;
                            case "Part":
                                part = xr.Value;
                                break;
                            case "Laboratory":
                                Laboratory = xr.Value;
                                break;
                            case "Start":
                                start = xr.Value;
                                break;
                            case "End":
                                end = xr.Value;
                                break;
                            default:
                                break;
                        }
                    }
                    else if ((xr.NodeType == XmlNodeType.EndElement))
                    {
                        element = xr.Name;
                        switch (element)
                        {
                            case "Title":
                                Title = new Title(start, end);
                                break;
                            case "Facultee":
                                Facultee = new Facultee(department, part);
                                break;
                            case "Employee":
                                EmployeesList.Add(new Employee(Id, Name, Facultee, Laboratory, Title));
                                break;
                        }
                    }
                }
            }
        }
    }
}
