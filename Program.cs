// https://www.wheeloffortunecheats.com/

using static System.Net.Mime.MediaTypeNames;

namespace wheeloffortuneandrobots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gameplay.Play();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Gameplay
    {
        static bool debugMessagesOn = true;
        static Game game;

        public static void Play()
        {
            int playerNum = 0;
            InitGame();
            game.player[playerNum].DisplayTurn();
        }

        static void InitGame()
        {
            game = new Game();
            
            game.player[0] = new Player();
            game.player[1] = new Player();
            game.player[2] = new Player();
            game.player[3] = new Player();

            game.player[0].name = "Josh";
            game.player[0].color = "MAGENTA";

            game.player[1].name = "Jenna";
            game.player[1].color = "BLUE";

            game.player[2].name = "Keyra";
            game.player[2].color = "GREEN";

            game.player[3].name = "ChrissyMC";
            game.player[3].color = "YELLOW";
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
        static char[] letters = new char[26]; // not needed
        static string[] categories;
        static string[] words;
        //static List<char> lettersUsed = new List<char>();
        static bool roundOver;

        /*
        // some of this logic needs to be taken care of 
        // by a the gameplay class
        public static void PlayWordGameRound(int gameNumber)
        {
            string category = categories[gameNumber];
            string word = words[gameNumber];
            Game.lettersUsed = new List<char>();
            roundOver = false;

            do // incorrect conditional for ending the round
            {
                Console.Clear();
                PWGVisualizer(category, word);
                if (!roundOver)
                {
                    string userInput = Console.ReadLine()
                        .ToUpper().Replace(" ", "");
                    if (userInput.Length == 1)
                    {
                        char letter = Convert.ToChar(userInput);
                        if (letters.Contains(letter)
                            && ! Game.lettersUsed.Contains(letter)) Game.lettersUsed.Add(letter);
                    }
                }
                else
                {
                    Console.ReadLine();
                }
            } while (!roundOver);
        }

        // split this up into separate methods
        // game logic will be used to call them conditionally
        static void PWGVisualizer(string category, string word)
        {
            Decor.Highlight($"\n\t^w^Category: ^e^{category}\n\n");

            int wordCount = 0;
            char[] wordLetters = word.ToCharArray();
            Console.Write("\t");
            roundOver = true;
            foreach (char wordLetter in wordLetters)
            {
                if (Game.lettersUsed.Contains(wordLetter)) Decor.Highlight($"^w^{wordLetter} ");
                else if (letters.Contains(wordLetter))
                {
                    roundOver = false;
                    Decor.Highlight("^w^_ ");
                }
                else
                {
                    wordCount++;
                    if (wordCount % 3 == 0) Console.Write("\n\t");
                    else Console.Write("  ");
                }
            }
            Console.Write("\n\n\n\t");
            if (!roundOver)
            {
                foreach (char letter in letters)
                {
                    if (Game.lettersUsed.Contains(letter)) Decor.Highlight($"^g^{letter} ^w^");
                    else Decor.Highlight($"^b^{letter} ^w^");
                }
            }
            else
            {
                Console.WriteLine("Congrats! :D");
            }
        }
        */

        /// <summary>
        /// generates the game categories
        /// </summary>
        public static void InitGame()
        {
            int categoryCount = 3;  // link this to user-configurable game length
            categories = GetGameCategories(categoryCount);
            words = GetGameWords(categories);
            int letterNum = 0;
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                letters[letterNum] = letter;
                letterNum++;
                if (debugMessagesOn) Decor.Highlight($"^r^(debug)^g^ added {letter} to letters array\n^w^");
            }
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
                Decor.Highlight("^r^(debug)^g^ items in full category array\n");
                foreach (string category in allCategories) Decor.Highlight($"\t- {category}\n");
                Decor.Highlight("^r^(debug)^g^ items in game category array\n");
                foreach(string category in categories) Decor.Highlight($"\t- {category}\n");
                Decor.Highlight("^w^");
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
                if (debugMessagesOn) Decor.Highlight($"^r^(debug)^g^ added {randomWord} to game {categoryNum+1} of category {category}\n");
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
}