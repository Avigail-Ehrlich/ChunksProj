using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine(DateTime.Now);
            ClassA ex = new ClassA
            {
                ClientWords = new HashSet<string>() { "mandarine", "apple", "-banana", "cherry", "-orange", "strawberry", "watermelon", "-grapes" }
            };

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            ex.WordsFromDB = new HashSet<string>() { "mandarine", "nnn", "banana" };
                            break;
                        }
                    case 1:
                        {
                            ex.WordsFromDB = new HashSet<string>() { "cherry", "xxx" };
                            break;
                        }
                    case 2:
                        {
                            ex.WordsFromDB = new HashSet<string>() { "lemon", "orange", "apple" };

                            break;
                        }
                }



                ex.SendToProcess();

            }

            ex.WordsToAdd.ExceptWith(ex.WordFromClientExistInDB);

            ex.Print();
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();


        }
    }

    internal class ClassA
    {
        public ClassA()
        {
            WordsToAdd = new HashSet<string>();
            WordsToRemove = new HashSet<string>();
            WordFromClientExistInDB = new HashSet<string>();
        }

        public HashSet<string> WordsFromDB { get; set; }
        public HashSet<string> WordsToAdd { get; set; }
        public HashSet<string> WordsToRemove { get; set; }
        public HashSet<string> ClientWords { get; set; }
        public HashSet<string> WordFromClientExistInDB { get; set; }


        public void SendToProcess()
        {

            ClientWords.ExceptWith(WordFromClientExistInDB);

            foreach (string word in ClientWords)
            {
                ProcessList(word);
            }
        }

        public void ProcessList(string clientWord)
        {
            //אם המילה נמצאת בDB תשמור אותה.
            if (WordsFromDB.Contains(clientWord))
            {
                WordFromClientExistInDB.Add(clientWord);
                return;
            }

            //עבור מחיקת פירות - אם הערך שהמשתמש הכניס מתחיל ב- בדיקה האם הערך בלעדיו נמצא בDB ואם כן הסרה שלו
            if (clientWord.StartsWith("-"))
            {
                if (WordsFromDB.Contains(clientWord.Substring(1)))
                {
                    WordsToRemove.Add(clientWord.Substring(1));
                    return;
                }
            }
            else
            {
                if (WordFromClientExistInDB.Contains(clientWord))
                    return;
                WordsToAdd.Add(clientWord);
            }
        }
        public void Print()
        {
            Console.WriteLine("fruits to add: ");
            Console.WriteLine("------------------");
            foreach (var word in WordsToAdd)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("\n");

            Console.WriteLine("fruits to remove: ");
            Console.WriteLine("------------------");
            foreach (var word in WordsToRemove)
            {
                Console.WriteLine(word);
            }

        }
    }
}