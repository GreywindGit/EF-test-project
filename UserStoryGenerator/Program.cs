using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;


namespace UserStoryGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // User input
            Console.Write("Source file: ");
            var inputFile = Console.ReadLine();
            var fileContent = new List<string>();

            Console.Write("Destination file: ");
            var outputFile = Console.ReadLine();
            if (File.Exists(outputFile))
            {
                try
                {
                    File.Delete(outputFile);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.Write("Number of user stories: ");
            var userStories = Int32.Parse(Console.ReadLine());

            // Read input file, formatting its content to use.
            try
            {
                using (var reader = new StreamReader(inputFile))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        fileContent.Add(Regex.Replace(line, @"\p{P}", ""));
                    }
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            var wordList = string.Join(" ", fileContent).Split(' ').ToList();
            var words = new Queue<string>(wordList);
            var record = new List<string>();


            // Create user stories, write them to the output file.
            for (var i = 0; i < userStories; i++)
            {
                record = CreateRecord(words);
                try
                {
                    File.AppendAllLines(outputFile, record);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.ReadKey();
        }

        public static List<string> CreateRecord(Queue<string> words)
        {
            var record = new List<string>();
            var rnd = new Random();
            var criteriaNum = rnd.Next(0, 7);

            record.Add($"AS A {CreateRecordLine(words, 1, 5)}");
            record.Add($"I WANT TO {CreateRecordLine(words, 8, 13)}");
            record.Add($"SO THAT {CreateRecordLine(words, 10, 17)}");
            record.Add("\n");

            for (var i = 0; i < criteriaNum; i++)
            {
                record.Add(CreateRecordLine(words, 2, 7));
                record.Add(CreateRecordLine(words, 15, 32));
            }

            record.Add("\n");
            record.Add("\n");

            return record;
        }

        public static string CreateRecordLine(Queue<string> words, int low, int high)
        {
            var rnd = new Random();
            var times = rnd.Next(low, high);
            var temp = "";
            for (var j = 0; j < times; j++)
            {
                temp += $" {words.Dequeue()}";
            }

            return temp;
        }
    }
}
