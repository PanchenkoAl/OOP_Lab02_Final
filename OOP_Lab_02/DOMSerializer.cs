using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OOP_Lab_02
{
    internal class DOMSerializer : ISerializable
    {
        public override void Serialize(object obj)
        {

        }
        public override void Deserialize(string xmlUrl, out List<Employee>? EmployeesList)
        {
            EmployeesList = new List<Employee>();
            XmlDocument doc = new XmlDocument();
            string[] st =
            {
                "Id",
                "Name",
                "Departerment",
                "Part",
                "Laboratory",
                "Start",
                "End"
            };

            doc.Load(xmlUrl);
            List<List<string>> ListList = new List<List<string>>();
            List<string> List = new List<string>();

            for (int k = 0; k < 7; k++)
            {
                SetCollection(st[k], doc, out List);
                ListList.Add(List);
            }

            for (int i = 0; i < ListList[0].Count; i++)
            {
                Facultee facultee = new Facultee(ListList[2][i], ListList[3][i]);
                Title title = new Title(ListList[5][i], ListList[6][i]);
                Employee employee = new Employee(ListList[0][i], ListList[1][i], facultee, ListList[4][i], title);
                EmployeesList.Add(employee);
            }
        }

        public override void FindByTagPhrase(string xmlUrl, string tag, out List<Employee> EL)
        {
            string tPhrase = null;
            string tAttribute = null;
            if(tag is null)
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
            XmlDocument doc = new XmlDocument();
            string[] st =
            {
                "Id",
                "Name",
                "Departerment",
                "Part",
                "Laboratory",
                "Start",
                "End"
            };
            int index = 0;
            for(int k = 0; k < 7; k ++)
            {
                if (st[k] == tAttribute)
                    index = k;
            }
            try
            {
                doc.Load(xmlUrl);
                List<List<string>> ListList = new List<List<string>>();
                List<string> List = new List<string>();

                for (int k = 0; k < 7; k++)
                {
                    SetCollection(st[k], doc, out List);
                    ListList.Add(List);
                }
                List<int> idxs = new List<int>();
                for (int k = 0; k < ListList[index].Count; k++)
                {
                    if (ListList[index][k].Contains(tPhrase))
                    {
                        idxs.Add(k);
                    }
                }
                for (int i = 0; i < idxs.Count; i++)
                {
                    Facultee facultee = new Facultee(ListList[2][idxs[i]], ListList[3][idxs[i]]);
                    Title title = new Title(ListList[5][idxs[i]], ListList[6][idxs[i]]);
                    Employee employee = new Employee(ListList[0][idxs[i]], ListList[1][idxs[i]], facultee, ListList[4][idxs[i]], title);
                    EL.Add(employee);
                }
            }
            catch(System.ArgumentException)
            {
                MessageBox.Show("Unable to find.");
            }
            
        }
        private void SmolDes(XmlNodeList xnl, out List<Employee> EL, string xmlUrl)
        {
            EL = new List<Employee>();
            XmlDocument doc = new XmlDocument();
            XmlDocument old = new XmlDocument();
            old.Load(xmlUrl);
            foreach(XmlNode node in xnl)
            {
                XmlNode importNode = old.ImportNode(node, true);
                doc.AppendChild(importNode);
            }
            for(int i = xmlUrl.Length - 1; i >= 0; i --)
            {
                if (xmlUrl[i] == 92)
                {
                    xmlUrl = xmlUrl.Substring(0, i + 1);
                    xmlUrl += "AAAAAAAA.xml";
                    MessageBox.Show(xmlUrl);
                    break;
                }
            }
            
            doc.Save(xmlUrl);
            Deserialize(xmlUrl, out EL);
        }
        private void SetCollection(string ss, XmlDocument doc, out List<string> List)
        {
            int i = 0;
            List = new List<string>();
            while (i < doc.GetElementsByTagName(ss).Count && doc.GetElementsByTagName(ss)[i].ChildNodes[0].Value != null)
            {
                string temp = doc.GetElementsByTagName(ss)[i].ChildNodes[0].Value;
                List.Add(temp);
                i++;
            }
        }
    }
}
