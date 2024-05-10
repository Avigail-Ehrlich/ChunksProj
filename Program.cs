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

            ex.WordsToAdd.ExceptWith(ex.WordsFromClientExistInDB);

            ex.Print();
            Console.WriteLine("\n" + DateTime.Now);
            Console.ReadLine();


        }
    }

    internal class ClassA
    {
        public ClassA()
        {
            WordsToAdd = new HashSet<string>();
            WordsToRemove = new HashSet<string>();
            WordsFromClientExistInDB = new HashSet<string>();
        }

        public HashSet<string> WordsFromDB { get; set; }
        public HashSet<string> WordsToAdd { get; set; }
        public HashSet<string> WordsToRemove { get; set; }
        public HashSet<string> ClientWords { get; set; }
        public HashSet<string> WordsFromClientExistInDB { get; set; }


        public void SendToProcess()
        {

            ClientWords.ExceptWith(WordsFromClientExistInDB);

            foreach (string word in ClientWords)
            {
                ProcessList(word);
            }
        }

        public void ProcessList(string clientWord)
        {
            if (WordsFromDB.Contains(clientWord))
            {
                WordsFromClientExistInDB.Add(clientWord);
                return;
            }

            if (clientWord.StartsWith("-") && WordsFromDB.Contains(clientWord.Substring(1)))
            {
                WordsFromClientExistInDB.Add(clientWord);
                WordsToRemove.Add(clientWord.Substring(1));
                return;
            }

    
            _ = !WordsFromClientExistInDB.Contains(clientWord) && !clientWord.StartsWith("-") && WordsToAdd.Add(clientWord);

        }

        public void Print()
        {

            Console.WriteLine("\nfruits to add: ");
            string wordsToAddString = string.Join(", ", WordsToAdd);
            Console.WriteLine(wordsToAddString);

            Console.WriteLine("\nfruits to remove: ");
            string wordsToRemoveString = string.Join(", ", WordsToRemove);
            Console.WriteLine(wordsToRemoveString);


        }

    }
}
