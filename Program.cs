﻿// https://www.wheeloffortunecheats.com/

using static System.Net.Mime.MediaTypeNames;

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
            for (int i = 0; i < 3; i++)
            {
                WordGame.PlayWordGame(i);
            }
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
        static char[] letters = new char[26];
        static string[] categories;
        static string[] words;
        static List<char> lettersUsed;

        /// <summary>
        /// plays a round of the word game
        /// </summary>
        /// <param name="gameNumber"></param>
        public static void PlayWordGame(int gameNumber)
        {
            //while ()
        }

        static void PWGVisualizer()
        {
            foreach (char letter in letters)
            {
                if (lettersUsed.Contains(letter)) Decor.Highlight($"^g^{letter}^w^");
                if (lettersUsed.Contains(letter)) Decor.Highlight($"^w^{letter}^w^");
            }
        }

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

    public class Decor
    {
        public static void Highlight(string line)
        {
            string[] splitLines = line.Split('^');
            foreach (string splitLine in splitLines)
            {
                switch (splitLine)
                {
                    case "g":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case "y":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case "b":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "r":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "w":
                    case "/g":
                    case "/y":
                    case "/b":
                    case "/r":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        Console.Write(splitLine);
                        break;
                }
            }
        }
    }
}