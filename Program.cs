using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT_Multiple_Choice_Exam
{
    //Mantas Lingaitis 101165443
    //
    class Program
    {
        class RetrieveData
        {
            //Read the whole data file
            static public void ReadFileData()
            {
                try
                {   // Read each line of the file into a string array.
                    string mantas_path = "D:/Developed Course Programs/C#/PROJECT_Multiple_Choice_Exam/PROJECT_Multiple_Choice_Exam/exam.txt";
                    string path = mantas_path; //Add you path here to read from .txt file
                    string[] lines = File.ReadAllLines(@path);

                    SplitData(lines);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception while reading file:  " + e.Message);
                }
                Console.WriteLine("File read finsihed... \n");
            }
            //Split up all data into specific variables
            static private void SplitData(string[] lines)
            {
                int size = lines.Length;
                string[] splitData = { };
                string answerLine;
                string[] studentID = new string[size];
                string[] studentAnswers = new string[size];

                answerLine = lines[0];

                for (int i = 1; i < size - 1; i++)
                {
                    splitData = lines[i].Split();
                    studentID[i] = splitData[0];
                    studentAnswers[i] = splitData[1];
                }

                ExamData ed = new ExamData();
                ed.SetAnswerLine(answerLine);
                ed.SetStudentId(studentID);
                ed.SetStudentAnswers(studentAnswers);
            }
            static public void DisplayData(bool showThis)
            {
                ExamData ed = new ExamData();

                string[] studentID = ed.GetStudntId();
                string[] studentAnswers = ed.GetStudntAnswers();
                string answerLine = ed.GetAnswerLine();
                int size = studentID.Length;

                if (showThis == true)
                {

                    Console.WriteLine("\n>>>Answer line: " + answerLine + " ");

                    for (int i = 1; i < size - 1; i++)
                    {
                        Console.WriteLine(i + ") \n" +
                            "Student id: " + studentID[i] +
                            "\nAnswers:    " + studentAnswers[i]);
                    }
                }
            }
        }
        class ExamData{
            //Get and set specific data

            static private string ansLine;
            static private string[] studIds;
            static private string[] studAns;
            static private int[] correctCountAns;

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

            public static void SetCorrectCount(int[] count)
            {
                correctCountAns = count;
            }

            public static int[] GetCorrectCount()
            {
                return correctCountAns;
            }
        }

        static void Main(string[] args)
        {
            //input
            RetrieveData.ReadFileData();
            RetrieveData.DisplayData(false); // change to true to see red data from .txt

            ExamData ed = new ExamData();

            string answers = ed.GetAnswerLine();
            string[] studId = ed.GetStudntId();
            string[] studAns = ed.GetStudntAnswers();

            //logic
            Console.WriteLine(" ********* MCQ STUDENT EXAM REPORT ********* ");
            int[] score = CountCorrectAnswer(answers, studAns);
            //output #1 Print student id and their score
            ShowStudScore(studId, score);
            //output #2 Print total candidate number
            Console.WriteLine("The total amount of eximninations marked : " + ShowTotalCandidates(studId));
            //output #3 Print the correct answer for each question
            ShowCorrectAnswerForQuestion();
 
            Console.ReadKey();
        }

        static public int ShowTotalCandidates(string [] students)
        {
            int count = 1;

            for(int i=1; i<students.Length-1; i++)
            {
                //Console.WriteLine(" -- >" + count);
                count++;
            }

            return count;
        }
        static public void ShowStudScore(string[] studID, int[] score)
        {
            Console.WriteLine("{0,-20} {1,5}", "Student number", "Mark");
            for(int i=1; i<studID.Length-1; i++)
            {
                Console.WriteLine("{0,-22} {1:D}", studID[i], score[i]);
            }
        }
        static public int[] CountCorrectAnswer(string ansSheet, string[] studAns)
        {
            int[] score = new int[studAns.Length];
            char[] word;
            char[] ansLetter = ansSheet.ToCharArray();
            int[] correctCountQuestion = new int[20]; // 20 for amount of questions
            int count = 0; // the correct question counter

            for (int i = 1; i < studAns.Length - 1; i++)
            {
                word = studAns[i].ToCharArray();
                for (int j = 0; j < 20; j++)
                {
                    // if the score is correct add 4 points
                    if (word[j] == ansLetter[j])
                    {
                        score[i] += 4;
                        //count +1 when correct
                        count++;
                        correctCountQuestion[j] += count;
                        count = 0;
                    }
                    else if (word[j] == 'X' || word[j] == 'x')
                    {
                        // no score
                    }
                    else
                    {
                        //Minus one point if answer was incorrect
                        score[i] -= 1;
                    }
                }
                count = 0; // reset counter once couted all 20 score for 1 question
            }
            ExamData.SetCorrectCount(correctCountQuestion); // save correct answers count
            return score;
        }
        static public void ShowCorrectAnswerForQuestion()
        {
            Console.WriteLine("number of correct responses for each question:");

            int[] corrAnsTimes = ExamData.GetCorrectCount();

            FormatLines(0, 10, "question", " ", corrAnsTimes);
            FormatLines(0, 10, "#correct", " ", corrAnsTimes);
            
            Console.WriteLine();

            FormatLines(10, 20, "question", " ", corrAnsTimes);
            FormatLines(10, 20, "#correct", "  ", corrAnsTimes);
    
        }

        static public void FormatLines(int min, int max, string text, string space, int[] corrAns)
        {
            Console.Write(text + ": ");
            for (int y = min; y < max; y++)
            {
                if (text.Equals("question"))
                    Console.Write((y + 1) + space);
                if (text.Equals("#correct"))
                    Console.Write(corrAns[y] + space);
            }
            Console.WriteLine();
        }
    }
}
