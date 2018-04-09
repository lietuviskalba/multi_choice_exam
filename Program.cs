﻿using System;
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
            // Count the correct answers for students
            int[] score = CountCorrectAnswer(answers, studAns);
            //output #1 Print student id and their score
            ShowStudScore(studId, score);
            //output #2 Print total candidate number
            Console.WriteLine("The total amount of eximninations marked : " + ShowTotalCandidates(studId));
            //output #3 Print the correct answer for each question
            ShowCorrectAnswerForQuestion();
 
            Console.ReadKey();
        }
        static public void ShowCorrectAnswerForQuestion()
        {
            //code here
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

            for (int i = 1; i < studAns.Length - 1; i++)
            {
                word = studAns[i].ToCharArray();
                for (int j = 0; j < 20; j++)
                {
                    if (word[j] == ansLetter[j])
                    {
                        score[i] += 4;
                    }
                    else if (word[j] == 'X' || word[j] == 'x')
                    {
                        // no score
                    }
                    else
                    {
                        score[i] -= 1;
                    }
                }
            }

            return score;
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

                for (int i = 1; i < size -1; i++)
                {
                    Console.WriteLine(i + ") \n" +
                        "Student id: "   + studentID[i] + 
                        "\nAnswers:    " + studentAnswers[i]);
                }
            }
        }
    }
}
