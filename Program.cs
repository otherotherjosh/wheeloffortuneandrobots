// https://www.wheeloffortunecheats.com/

namespace wheeloffortuneandrobots
{
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
        /// <summary>
        /// generates the game categories
        /// </summary>
        public static void InitGame()
        {
            //int categoryCount = 3;
            //string[] categories = new string[categoryCount];
            string[] categories = ReadFileCategories();
            Console.WriteLine("(debug) items in category array");
            foreach (string category in categories)
            {
                Console.WriteLine($"- {category}");
            }
        }

        static string[] ReadFileCategories()
        {
            List<string> categories = new List<string>();
            using (StreamReader reader = new("../../../gamedata/categories.txt"))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    categories.Add(line);
                }
            }
            return categories.ToArray();
        }
    }

    /// <summary>
    /// where players game data is generated and stored
    /// </summary>
    public class Player
    {
        public struct PlayerBase
        {
            public string name;
            public int score;
        }
    }


    public class AI: Player
    {
        // deal with this shit later
    }
}