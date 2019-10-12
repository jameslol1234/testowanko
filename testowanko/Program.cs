﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Projekt
{
    class Program
    {
        static void DLFile()
        {
            Console.WriteLine("1. Download file from internet.");
            string remoteUri = "https://s3.zylowski.net/public/input/";
            string fileName = "3.txt";
            WebClient myWebClient = new WebClient();
            string myStringWebResource = remoteUri + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            myWebClient.DownloadFile(myStringWebResource, fileName);
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
        }
        static int CountLetters()
        {
            Console.WriteLine("2. Count number of letters in the file.");
            string FileText;
            try
            {
                FileText = File.ReadAllText("3.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file 3.txt");
                return 1;
            }

            int LettersQty = 0;
            foreach (char c in FileText)
            {
                if (!char.IsWhiteSpace(c) && char.IsLetter(c))
                {
                    LettersQty++;
                }
            }
            Console.WriteLine("Count of letters in file: " + LettersQty);
            return 0;
        }
        static int CountWords()
        {
            Console.WriteLine("3. Count number of words in the file.");
            string FileText;
            try
            {
                FileText = File.ReadAllText("3.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file 3.txt");
                return 1;
            }
            string[] Words = FileText.Split(' ');
            int WordsQty = 0;
            foreach (string word in Words)
            {
                WordsQty++;
            }
            Console.WriteLine("Number of words is: " + WordsQty);
            return 0;
        }
        static int CountPuncMarks()
        {
            Console.WriteLine("4. Count number of punctuation marks in the file.");
            string FileText;
            try
            {
                FileText = File.ReadAllText("3.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file 3.txt");
                return 1;
            }

            int PuncMarksQty = 0;
            foreach (char c in FileText)
            {
                if (char.IsPunctuation(c))
                {
                    PuncMarksQty++;
                }
            }
            Console.WriteLine("Count of punctuation marks in file: " + PuncMarksQty);
            return 0;
        }
        static int CountSentence()
        {
            Console.WriteLine("5. Count number of sentences in the file.");
            string FileText;
            try
            {
                FileText = File.ReadAllText("3.txt");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file 3.txt");
                return 1;
            }
            string[] Words = FileText.Split('.', '?', ';', '!');
            int SentenceQty = 0;
            foreach (string word in Words)
            {
                SentenceQty++;
            }
            Console.WriteLine("Number of sentences is: " + SentenceQty);
            return 0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("TEXT ANALYZER");
            while (true)
            {
                Console.WriteLine("\nMENU: \n1. Download file from internet.\n2. Count number of letters in the file.\n3. Count number of words in the file.\n4. Count number of punctuation marks in the file.\n5. Count number of sentences in the file.\n6. Report about usage of letters (A-Z).\n7. Save statistics from points 2-5 to the file(statystki.txt)\n8. Exit and close application.\n");
                Console.Write("Choose option to execute: ");
                Console.WriteLine("JD");
                Console.WriteLine("JD");
                Console.WriteLine("JD");
                int MenuOpt;
                if (!int.TryParse(Console.ReadLine(), out MenuOpt))
                {
                    Console.WriteLine("Wrong format. Please choose number 1-8");
                    continue;
                }
                if (MenuOpt == 1)
                {
                    DLFile();
                }
                else if (MenuOpt == 2)
                {
                    if (CountLetters() == 1) continue;
                }
                else if (MenuOpt == 3)
                {
                    if (CountWords() == 1) continue;
                }
                else if (MenuOpt == 4)
                {
                    if (CountPuncMarks() == 1) continue;
                }
                else if (MenuOpt == 5)
                {
                    if (CountSentence() == 1) continue;
                }
                else if (MenuOpt == 6)
                {
                    Console.WriteLine("6. Report about usage of letters (A-Z).");
                    int[] c = new int[(int)char.MaxValue];
                    string FileText;
                    try
                    {
                        FileText = File.ReadAllText("3.txt");
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Could not find file 3.txt");
                        continue;
                    }
                    foreach (char t in FileText)
                    {
                        c[(int)t]++;
                    }
                    for (int i = 0; i < (int)char.MaxValue; i++)
                    {
                        if (c[i] > 0 &&
                            char.IsLetter((char)i))
                        {
                            Console.WriteLine("{0} : {1}",
                                (char)i,
                                c[i]);
                        }
                    }
                }
                else if (MenuOpt == 7)
                {
                    Console.WriteLine("7. Save statistics from points 2-5 to the file(statystki.txt)");
                    FileStream ostrm;
                    StreamWriter writer;
                    TextWriter oldOut = Console.Out;
                    try
                    {
                        ostrm = new FileStream("./statystyki.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        writer = new StreamWriter(ostrm);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Cannot open statystyki.txt for writing");
                        Console.WriteLine(e.Message);
                        return;
                    }
                    Console.SetOut(writer);
                    CountLetters();
                    CountWords();
                    CountPuncMarks();
                    CountSentence();
                    Console.SetOut(oldOut);
                    writer.Close();
                    ostrm.Close();
                    Console.WriteLine("Saving to file done!");
                }
                else if (MenuOpt == 8)
                {
                    if (File.Exists("3.txt"))
                    {
                        File.Delete("3.txt");
                        Console.WriteLine("Deleted file: 3.txt");
                    }
                    if (File.Exists("statystyki.txt"))
                    {
                        File.Delete("statystyki.txt");
                        Console.WriteLine("Deleted file: statystyki.txt");
                    }
                    Console.WriteLine("Exiting the program...");
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong option!\nPlease select correct option(1-8)");
                    continue;
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);

        }
    }
}
