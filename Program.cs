// https://www.wheeloffortunecheats.com/

using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace wheeloffortuneandrobots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game.Play();
        }

        /// <summary>
        /// makes an array of strings from a text file
        /// </summary>
        /// <param name="filePath"></param>
        public static string[] ReadFile(string filePath)
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

        public static void ShuffleArray(ref string[] inputArray, int length)
        {
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                int randIndex = rand.Next(i, inputArray.Length - 1);
                string temp = inputArray[i];
                inputArray[i] = inputArray[randIndex];
                inputArray[randIndex] = temp;
            }
            Array.Resize(ref inputArray, length);
        }
    }

    public static class Game
    {
        static bool debugMessagesOn = false;

        static int numOfRounds;
        static int numOfPlayers;
        static int playerID;
        static Player[] players;
        static string[] categories;
        static string[] phrases;

        static void InitGame()
        {
            players = Player.Select();
            numOfPlayers = players.Length;
            numOfRounds = 3;  // range from 3 to 7
            phrases = WordPuzzle.GetWords(numOfRounds, out categories);

            if (debugMessagesOn)
            {
                int playerCount = 0;
                foreach (Player player in players)
                {
                    playerCount++;
                    Decor.DebugLine();
                    Decor.Highlight($"Player {playerCount}: ");
                    Decor.TextColor(player.Color);
                    Console.WriteLine(player.Name);
                    Decor.TextColor();
                }
                for (int i = 0; i < categories.Length; i++)
                {
                    int round = i + 1;
                    // show off words and categories
                    Decor.DebugLine();
                    Console.WriteLine($"Round {round}: ");
                    Console.WriteLine($"         Category: {categories[i]}");
                    Console.WriteLine($"           Phrase: {phrases[i]}");
                    Console.WriteLine($"          Letters: {WordPuzzle.PhraseLetters(phrases[i])}");
                    Decor.TextColor();
                }
            }
        }

        public static void Play()
        {
            InitGame();
            playerID = 0;
            for (int round = 0; round < numOfRounds; round++)
            {
                PlayRound(round);
                NextPlayerTurn();
            }
        }

        static void PlayRound(int round)
        {
            WordPuzzle wordPuzzle = new WordPuzzle(categories[round], phrases[round]);
            while (!wordPuzzle.IsSolved())
            {
                wordPuzzle.PlayRound(players[playerID]);
            }
        }

        public static void NextPlayerTurn()
            => playerID = (playerID + 1) % numOfPlayers;
    }

    public class WordPuzzle
    {
        static string vowels = "AEIOU";
        static Player currentPlayer;
        static int roundWorth;

        string category;
        string phrase;
        string lettersWrong;
        string lettersCorrect;

        public WordPuzzle(string category, string phrase)
        {
            this.category = category;
            this.phrase = phrase;
            this.lettersWrong = "";
            this.lettersCorrect = "";
        }

        public void PlayRound(Player player)
        {
            Random rand = new Random();
            currentPlayer = player;
            // random amount of money for testing

            RoundMenu();
        }

        void RoundMenu()
        {
            string color = currentPlayer.Color;
            Console.Clear();
            currentPlayer.ShowTurn(roundWorth);
            ShowWordPuzzle();
            // ask player to buy vowel, choose consonant or solve
            if (NoConsonantsLeft())
            {
                Console.WriteLine("Oops no consonants left");
                Console.ReadLine();
            }
            else
            {
                roundWorth = Wheel.Spin();
                GuessConsonant();
            }
        }

        void GuessConsonant()
        {
            ShowKeyboard("consonants", currentPlayer.Color);
            char playerGuess = currentPlayer.GuessConsonant(lettersCorrect+lettersWrong);
            if (PhraseLetters(phrase).Contains(playerGuess))
            {
                currentPlayer.ReceiveMoney(roundWorth);
                lettersCorrect += playerGuess;
            }
            else
            {
                lettersWrong += playerGuess;
                Game.NextPlayerTurn();
            }
        }

        void ShowWordPuzzle()
        {
            Console.WriteLine($"\n\t{category}:");
            int charLimit = 13;
            int charsDisplayed = 0;
            Console.Write("\n\t");
            foreach (string word in phrase.Split(" "))
            {
                if (charsDisplayed + word.Length > charLimit)
                {
                    charsDisplayed = 0;
                    Console.Write("\n\t");
                }
                charsDisplayed += word.Length + 1;
                foreach (char letter in word)
                {
                    if (lettersCorrect.Contains(letter))
                        Console.Write(letter);
                    else
                        Console.Write("_");
                    Console.Write(" ");
                }
                Console.Write("  ");
            }
            Console.WriteLine();
        }

        void ShowKeyboard(string keyboardMode, string playerColor)
        {
            Console.Write("\n\n\t");
            string color;
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                if ((lettersWrong+lettersCorrect).Contains(letter))
                    Decor.TextColor("GRAY");
                else if (vowels.Contains(letter))
                {
                    color = keyboardMode == "vowels" ? playerColor : "WHITE";
                    Decor.TextColor(color);
                }
                else
                {
                    color = keyboardMode == "consonants" ? playerColor : "WHITE";
                    Decor.TextColor(color);
                }
                if (letter == 'N') Console.Write("\n\t");
                Console.Write(letter + " ");
            }
            Decor.TextColor();
            Console.WriteLine("\n");
        }

        bool NoConsonantsLeft()
        {
            for (char consonantCheck = 'B'; consonantCheck <= 'Z'; consonantCheck++)
            {
                if (!(lettersCorrect + lettersWrong).Contains(consonantCheck)
                    && !IsVowel(consonantCheck.ToString()))
                    return false;
            }
            return true;
        }

        public bool IsSolved()
            => lettersCorrect.Length == PhraseLetters(phrase).Length;

        public static bool IsVowel(string letter)
            => vowels.Contains(letter);

        public static bool IsLetter(string letter)
        {
            if (letter.Length != 1) return false;

            char letterToCheck = Convert.ToChar(letter);
            for (char letterCompare = 'A'; letterCompare <= 'Z'; letterCompare++)
            {
                if (letterToCheck == letterCompare) return true;
            }

            return false;
        }

        public static string[] GetWords(int numOfRounds, out string[] categories)
        {
            categories = Program.ReadFile("../../../gamedata/allcategories.txt");
            Program.ShuffleArray(ref categories, numOfRounds);
            string[] phrases = new string[numOfRounds];
            for (int i = 0; i < numOfRounds; i++)
            {
                string category = categories[i]
                    .Replace(" ", "")
                    .ToLower();
                string filePath = "../../../gamedata/cat_" + category + ".txt";
                string[] categoryWords = Program.ReadFile(filePath);
                Program.ShuffleArray(ref categoryWords, 1);
                phrases[i] = categoryWords[0];
            }
            return phrases;
        }

        public static string PhraseLetters(string inputPhrase)
        {
            string letters = "";
            inputPhrase = inputPhrase.Replace(" ", "");
            foreach (char letter in inputPhrase)
            {
                if ( ! letters.Contains(letter)) letters += letter;
            }
            return letters;
        }
    }
}