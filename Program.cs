using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_Multiple_Choice_Exam
{
    class Program
    {
        static string path = "D:/Developed Course Programs/C#/PROJECT_Multiple_Choice_Exam/PROJECT_Multiple_Choice_Exam/exam.txt";
        static bool showRedFile = false; //Change to true to see red file

        class ExamData{
            //Get and set specific data

            static private string ansLine;
            static private string[] studIds;
            static private string[] studAns;

            public void SetAnswerLine(string ans)
            {
                ansLine = ans;
            }
            public string GetAnswerLine()
            {
                return ansLine;
            }
            public void SetStudentId(string[] stID)
            {
                studIds = stID;
            }
            public string[] GetStudntId()
            {
                return studIds;
            }
            public void SetStudentAnswers(string[] stAns)
            {
                studAns = stAns;
            }
            public string[] GetStudntAnswers()
            {
                return studAns;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("System started... \n");

            //input
            ReadFileData();

            ExamData ed = new ExamData();

            string answers = ed.GetAnswerLine();
            string[] studId = ed.GetStudntId();
            string[] studAns = ed.GetStudntAnswers();

            //logic
            Console.WriteLine(" ********* MCQ STUDENT EXAM REPORT ********* ");


            //output
 
            Console.ReadKey();
        }
        //Working with files methods
        private static void ReadFileData()
        {
            try
            {   // Read each line of the file into a string array.
                string[] lines = System.IO.File.ReadAllLines(@path);

                SplitData(lines);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while reading file:  " + e.Message);
            }
            Console.WriteLine("File read finsihed... \n");
        }
        static public void SplitData(string[] lines)
        {
            int size = lines.Length;
            string[] splitData = { };
            string answerLine;
            string[] studentID = new string[size];
            string[] studentAnswers = new string[size];   

            answerLine = lines[0];

            for (int i=1; i<size-1; i++)
            {
                splitData = lines[i].Split();                   
                studentID[i] = splitData[0];
                studentAnswers[i] = splitData[1];
            }

            ExamData ed = new ExamData();
            ed.SetAnswerLine(answerLine);
            ed.SetStudentId(studentID);
            ed.SetStudentAnswers(studentAnswers);

            DisplayData(showRedFile, lines, studentID, studentAnswers, answerLine, size);
        }
        static public void DisplayData(bool showThis, string[] lines, string[] studentID, string[] studentAnswers, string answerLine, int size )
        {
            if (showThis == true)
            {
                // Display the file contents by using a foreach loop.
                Console.WriteLine("Contents of exam.txt");
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    Console.WriteLine(line);
                }

                Console.WriteLine("\n>>>Answer line: " + answerLine + " ");

                for (int i = 1; i < size - 1; i++)
                {
                    Console.WriteLine(i + ") Student id: " + studentID[i] + "\n   Student answers: " + studentAnswers[i]);
                }
            }
        }
    }
}
