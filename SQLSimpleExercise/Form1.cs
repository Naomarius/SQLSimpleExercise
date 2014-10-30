using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace SQLSimpleExercise
{
    public partial class Form1 : Form
    {
        /*
        1. create an application of their choosing that:
        a. connects to a local SQL database
        b. queries the data
        c. returns the record set excluding null values and a simple condition on a value be greater than 4%
        d. comment their code
        */

        private string MyConnectionString = "";
        List<Project> Projects = new List<Project>();

        public Form1()
        {
            InitializeComponent();

            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ProjectsConnectionString"];
            MyConnectionString = connection.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(MyConnectionString))
            {
                con.Open();

                SqlCommand command = new SqlCommand("Select * FROM [Projects].[dbo].[Projects]", con);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader["PercentComplete"] != DBNull.Value)
                            Projects.Add(new Project((int)reader["ID"], reader["ProjectName"].ToString(), (int)reader["PercentComplete"]));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Project p in Projects)
            {
                listBox1.Items.Add(p);
            }
        }
    }

    public class Project
    {
        public int ID = 0;
        public string ProjectName = string.Empty;
        public int PercentComplete = 0;

        public Project ()
        {

        }

        public Project(int _ID, string _ProjectName, int _PercentComplete)
        {
            ID = _ID;
            ProjectName = _ProjectName;
            PercentComplete = _PercentComplete;
        }

        public override string ToString()
        {
            string ProjectDetails = string.Format("{0} : {1}", ProjectName, PercentComplete.ToString());
            return ProjectDetails;
        }
    }
}
