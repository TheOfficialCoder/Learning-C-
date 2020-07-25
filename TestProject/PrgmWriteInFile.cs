using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class PrgmWriteInFile
    {
        static void Main(string[] args)
        {
            int totalrows = 100001;   //Total data to write
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
                for (int j = executedrow; j <= selectedrow; j++)
                {
                    data += j + "\n";
                    executedrow++;
                }
                WriteDataInCSVFile(data, filepath);
                data = "";
            }
        }

        private static void WriteDataInCSVFile(string data, string filepath)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
            {
                try
                {
                    sw.WriteLine("Numbers" + "\n");
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