using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;


namespace UserStoryGenerator
{
    class Program
    {
        private static readonly Random Rnd = new Random();
        static void Main(string[] args)
        {
            // User input
            Console.Write("Source file: ");
            var inputFile = Console.ReadLine();
            var fileContent = new List<string>();

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
            

            // Create user stories and write them to DB.
            for (var i = 0; i < userStories; i++)
            {
                Console.Write("User story id: ");
                var storyId = Console.ReadLine();
                var newStory = CreateUserStory(storyId, words);
                var criterias = new List<Criteria>();
                var criteriaNum = Rnd.Next(0, 7);
                if (criteriaNum > 0)
                {
                    for (var j = 0; j < criteriaNum; j++)
                    {
                        criterias.Add(CreateCriteria(newStory, words));
                    }
                }
                else
                {
                    criterias = null;
                }
                CreateDbEntry(newStory, criterias);
            }

            //var result = SearchStoryById("US6");
            //Console.WriteLine("AS A " + result.Actor + "\nI WANT TO " + result.Goal + "\nSO THAT " + result.Value);


            Console.WriteLine("Records saved. Press any key to exit...");
            Console.ReadKey();
        }

        public static UserStory CreateUserStory(string uid, Queue<string> words)
        {
            var newStory = new UserStory
            {
                Id = uid,
                Actor = CreateRecordLine(words, 1, 5),
                Goal = CreateRecordLine(words, 8, 13),
                Value = CreateRecordLine(words, 10, 17)
            };

            return newStory;
        }

        public static Criteria CreateCriteria(UserStory userStory, Queue<string> words)
        {
            var newCriteria = new Criteria
            {
                UserStory = userStory,
                Header = CreateRecordLine(words, 2, 7),
                Content = CreateRecordLine(words, 10, 17)
            };
            return newCriteria;
        }

        public static void CreateDbEntry(UserStory userStory, List<Criteria> criterias = null)
        {
            using (var db = new StoryDbContext())
            {
                db.Stories.Add(userStory);
                if (criterias != null)
                {
                    db.Criterias.AddRange(criterias);
                }
                db.SaveChanges();
            }
        }

        public static UserStory SearchStoryById(string uid)
        {
            UserStory result;
            using (var db = new StoryDbContext())
            {
                var query = from s in db.Stories where s.Id == uid select s;
                result = query.ToList()[0];
            }

            return result;
        }

        public static string CreateRecordLine(Queue<string> words, int low, int high)
        {
            var times = Rnd.Next(low, high);
            var temp = "";
            for (var j = 0; j < times; j++)
            {
                temp += $" {words.Dequeue()}";
            }

            return temp;
        }
    }
}
