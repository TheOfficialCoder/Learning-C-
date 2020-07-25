using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
    class PrgmWriteStudentDetailsInMultipleCSV
    {
        static void Main(string[] args)
        {
            List<Student> students = GetStudentData();
            int totalrows = students.Count;   //Total data to write
            int maxrows = int.Parse(Console.ReadLine());    //Maximum rows to write in a file
            int maxfile = 0;    //Maximum file needed
            int selectedrow = 0;  //Select row till which we need to write in single file
            int executedrow = 0;    //Record till that its already executed
            if (maxrows != 0)      //Calculate Maximum files needed to write complete data
                maxfile = totalrows / maxrows;
            if ((totalrows % maxrows) != 0)
                maxfile = (totalrows / maxrows) + 1;
            string data = "";
            //Folder to store file
            string folder = @"C:\Users\Himanshu Goyal\Documents\Alstom\WriteInFile\";
            string filename, filepath;
            //Write in File Object
            for (int i = 1; i <= maxfile; i++)
            {
                selectedrow += maxrows;
                if (selectedrow > totalrows)
                    selectedrow = totalrows;

                filename = DateTime.UtcNow.ToString("ddMMyyyyhhmm") + "-" + i + " of " + maxfile + ".csv";
                filepath = folder + filename;   //Full File Path
                for (int j = executedrow; j < selectedrow; j++)
                {
                    data +=students[j].Id+","+students[j].Name+","+students[j].Gender+","+students[j].Email + "\n";
                    executedrow++;
                }
                WriteDataInCSVFile(data, filepath);
                data = "";
            }
        }
        private static List<Student> GetStudentData()
        {
            string connectionString = @"Data Source=DESKTOP-EG8NCOP;Initial Catalog=Student;Integrated Security=True";
            List<Student> students = new List<Student>();
            string sql = "select * from Student_Detail";
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, con);
            con.Open();
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            con.Close();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Student stud = new Student
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["Id"]),
                    Name = dataTable.Rows[i]["Name"].ToString(),
                    Gender = dataTable.Rows[i]["Gender"].ToString(),
                    Email = dataTable.Rows[i]["Email"].ToString()
                };
                students.Add(stud);
            }
            return students;
        }
        private static void WriteDataInCSVFile(string data, string filepath)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
            {
                try
                {
                    sw.WriteLine("Id,Name,Gender,Email" + "\n");
                    sw.WriteLine(data);
                }
                finally
                {
                    sw.Close();
                }
            }
        }
    }
}
