// https://www.wheeloffortunecheats.com/

namespace wheeloffortuneandrobots
{
    public struct PlayerBase
    {
        public string name;
        public int score;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            WordGame.InitGame();
        }
    }

    /// <summary>
    /// has all the shit where we get a random word
    /// from a long list of words or phrases
    /// and categories
    /// </summary>
    public class WordGame
    {
        static bool debugMessagesOn = true;

        /// <summary>
        /// generates the game categories
        /// </summary>
        public static void InitGame()
        {
            int categoryCount = 3;  // link this to user-configurable game length
            string[] categories = GetGameCategories(categoryCount);
            string[] words = GetGameWords(categories);
        }

        static string[] GetGameCategories(int categoryCount)
        {
            string[] allCategories = ReadFile("../../../gamedata/allcategories.txt");

            categoryCount = allCategories.Length < categoryCount ? allCategories.Length : categoryCount;
            string[] categories = new string[categoryCount];

            Random random = new Random();

            for (int i = 0; i < categoryCount; i++)
            {
                string chosenCategory;
                do
                {
                    chosenCategory = allCategories[random.Next(allCategories.Length)];
                } while (categories.Contains(chosenCategory));
                categories[i] = chosenCategory;
            }

            if (debugMessagesOn)
            {
                Console.WriteLine("(debug) items in full category array");
                foreach (string category in allCategories) Console.WriteLine($"- {category}");
                Console.WriteLine();
                Console.WriteLine("(debug) items in game category array");
                foreach(string category in categories) Console.WriteLine($"- {category}");
            }
            
            return categories;
        }

        static string[] GetGameWords(string[] categories)
        {
            Random random = new Random();
            string[] words = new string[categories.Length];

            foreach (string category in categories)
            {
                int categoryNum = Array.IndexOf(categories, category);
                string[] categoryWords = ReadFile("../../../gamedata/cat_"
                    + category.ToLower().Replace(" ", "") + ".txt");
                string randomWord = categoryWords[random.Next(categoryWords.Length)];
                words[categoryNum] = randomWord;
                if (debugMessagesOn) Console.WriteLine($"(debug) added {randomWord} to game {categoryNum+1} of category {category}");
            }
            return words;
        }

        /// <summary>
        /// makes an array of strings from a text file
        /// </summary>
        /// <param name="filePath"></param>
        static string[] ReadFile(string filePath)
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new(filePath))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }
    }

    /// <summary>
    /// where players game data is generated and stored
    /// </summary>
    public class Player
    {
        // deal with this shit later
    }


    public class AI: Player
    {
        // deal with this shit later
    }
}