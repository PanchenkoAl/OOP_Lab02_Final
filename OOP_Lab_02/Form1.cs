using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace OOP_Lab_02
{
    public partial class Form1 : Form
    {

        private string FileLocation = "";
        private List<Employee> EmployeeList;
        private string Tag = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FileLocation == "")
            {
                MessageBox.Show("File path is empty. Open file before serializing");
            }
            else
            {
                SAXSerializer sSerializer = new SAXSerializer();
                List<Employee> EmployeesList;
                sSerializer.Deserialize(FileLocation, out EmployeesList);
                EmployeeList = EmployeesList;
                if(EmployeeList != null)
                {
                    MessageBox.Show("File was successfully serialized by SAX");
                }
                DGVFillData(EmployeeList);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (FileLocation == "")
            {
                MessageBox.Show("File path is empty. Open file before serializing");
            }
            else
            {
                DOMSerializer dSerializer = new DOMSerializer();
                List<Employee> EmployeesList;
                dSerializer.Deserialize(FileLocation, out EmployeesList);
                EmployeeList = EmployeesList;
                if (EmployeeList != null)
                {
                    MessageBox.Show("File was successfully serialized by DOM");
                }
                DGVFillData(EmployeeList);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader rd = new StreamReader(openFileDialog.FileName);
                FileLocation = openFileDialog.FileName;
                rd.Close();
            }
            textBox1.Text = FileLocation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] Headers =
            {
                "Id",
                "Name",
                "Departerment",
                "Part",
                "Laboratory",
                "Start",
                "End"
            };
            dataGridView1.AllowUserToAddRows = false;
            for(int i = 0; i < 7; i ++)
            {
                dataGridView1.Columns.Add(Name, Headers[i]);
            } 
            for(int i = 0; i < 1; i ++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }
        }

        private void DGVFillData(List<Employee> employees)
        {
            if(employees != null)
            {
                int RowIndex = 0;
               while(dataGridView1.Rows.Count < employees.Count)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[RowIndex].HeaderCell.Value = RowIndex.ToString();
                    RowIndex++;
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = (dataGridView1.Rows.Count - 1).ToString();
                for(int i = 0; i < employees.Count; i ++)
                {
                    dataGridView1[0, i].Value = employees[i].Id;
                    dataGridView1[1, i].Value = employees[i].Name;
                    dataGridView1[2, i].Value = employees[i].Facultee.Departerment;
                    dataGridView1[3, i].Value = employees[i].Facultee.Part;
                    dataGridView1[4, i].Value = employees[i].Laboratory;
                    dataGridView1[5, i].Value = employees[i].Title.Start;
                    dataGridView1[6, i].Value = employees[i].Title.End;
                }
            }
            else
            {
                MessageBox.Show("Error. Input List is null");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FileLocation == "")
            {
                MessageBox.Show("File path is empty. Open file before serializing");
            }
            else
            {

                LINQSerializer dSerializer = new LINQSerializer();
                List<Employee> EmployeesList;
                dSerializer.Deserialize(FileLocation, out EmployeesList);
                EmployeeList = EmployeesList;
                if (EmployeeList != null)
                {
                    MessageBox.Show("File was successfully serialized by LINQ");
                }
                DGVFillData(EmployeeList);
            }
        }
        private void ResetDgv()
        {
            for(int i = 0; i < dataGridView1.Rows.Count; i ++)
            {
                for(int j = 0; j < 7; j ++)
                {
                    dataGridView1[j, i].Value = "";
                }
            }
            for (int i = dataGridView1.Rows.Count - 1; i > 0; i--)
            { 
                dataGridView1.Rows.RemoveAt(i);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Tag = textBox2.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetDgv();
            SAXSerializer sax = new SAXSerializer();
            List<Employee> List;
            sax.FindByTagPhrase(FileLocation, Tag, out List);
            DGVFillData(List);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResetDgv();
            DOMSerializer dom = new DOMSerializer();
            List<Employee> List;
            dom.FindByTagPhrase(FileLocation, Tag, out List);
            DGVFillData(List);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ResetDgv();
            LINQSerializer linq = new LINQSerializer();
            List<Employee> List;
            linq.FindByTagPhrase(FileLocation, Tag, out List);
            DGVFillData(List);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ResetDgv();
            DGVFillData(EmployeeList);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //HTMLUIControl htmlu = new HTMLUIControl();
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"D:\MyXslt.xsl");
            if (FileLocation != null && FileLocation != "")
            {
                xslt.Transform(FileLocation, "EmployeesT.html");
                MessageBox.Show("Complete");
            }
            else
                MessageBox.Show("File Location is incorrect!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author's name Panchenko Olexandr, gr. K-25. \n Open .xml file with 'Open File' button. \n To Deserialize, press SAX/DOM/LINQ Deserialize button. \n To find something by phrase, type phrase in the texbox in the right corner. Use 'phrase(Attribute)' (For example Alex(Name), 1234(Id), etc.). \n Press 'Convert to HTML' button to get .html file, using special .xsl file, that you can find in the projects folder (file name MyXslt.xsl). form.");
        }
    }
}