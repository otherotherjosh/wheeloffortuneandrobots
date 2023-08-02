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
        static bool debugMessagesOn = true;

        // per game variables
        static int numOfRounds;
        static int numOfPlayers;
        static Player[] players;
        static string[] categories;
        static string[] phrases;

        // per round variables
        static int spinValue;
        static int currentPlayerID;
        static Player currentPlayer;
        static WordPuzzle currentPuzzle;

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
                Decor.StandBy();
            }
        }

        public static void Play()
        {
            InitGame();
            currentPlayerID = 0;
            currentPlayer = players[currentPlayerID];
            for (int round = 0; round < numOfRounds; round++)
            {
                PlayRound(round);
            }
        }

        static void PlayRound(int round)
        {
            currentPuzzle = new WordPuzzle(categories[round], phrases[round]);
            while (!currentPuzzle.IsSolved())
            {
                Console.Clear();
                currentPlayer.ShowTurn();
                currentPuzzle.ShowWordPuzzle();
                RoundMenu();
            }
        }

        static void RoundMenu()
        {
            string color = currentPlayer.Color;
            bool canBuyVowel = currentPuzzle.VowelsRemaining() && currentPlayer.Money >= 250;

            if (currentPuzzle.ConsonantsRemaining())
            {
                Decor.TextColor(color);
                Console.Write("\n\n\t[1] ");
                Decor.TextColor();
                Console.Write("Spin Wheel");
            }

            Decor.TextColor(color);
            Console.Write("\n\t[2] ");
            Decor.TextColor();
            Console.Write("Solve");

            if (canBuyVowel)
            {
                Decor.TextColor(color);
                Console.Write("\n\t[3] ");
                Decor.TextColor();
                Console.Write("Buy Vowel ($250)");
            }

            Console.WriteLine("\n");

            string userInput = currentPlayer.RoundMenu();

            switch (userInput)
            {
                case "1":
                    // spin wheel
                    // then guess consonant if there are any left
                    SpinWheel();
                    break;
                case "2":
                    // solve
                    // (always available)
                    break;
                case "3":
                    // buy vowel
                    // (if player has enough money)
                    // can buy more than one per turn
                    if (canBuyVowel)
                        currentPuzzle.BuyVowel(currentPlayer);
                    break;
            }
        }

        static void SpinWheel()
        {
            Console.Clear();
            spinValue = Wheel.Spin(currentPlayer);

            if (currentPuzzle.ConsonantsRemaining())
            {
                currentPuzzle.GuessConsonant(currentPlayer, spinValue);
            }
        }

        static void SolvePuzzle()
        {
            // WORK ON ThIS !!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        public static void NextPlayerTurn()
        {
            currentPlayerID = (currentPlayerID + 1) % numOfPlayers;
            currentPlayer = players[currentPlayerID];
        }
    }

    public class WordPuzzle
    {
        static string vowels = "AEIOU";

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

        public void GuessConsonant(Player currentPlayer, int spinValue)
        {
            Console.Clear();
            currentPlayer.ShowTurn(spinValue);
            ShowWordPuzzle();
            ShowKeyboard("consonants", currentPlayer.Color);
            char playerGuess = currentPlayer.GuessConsonant(lettersCorrect+lettersWrong);
            int letterCount = GuessedLetterCount(playerGuess);
            if (letterCount > 0)
            {
                int reward = letterCount * spinValue;
                currentPlayer.ReceiveMoney(reward);
                lettersCorrect += playerGuess;
            }
            else
            {
                lettersWrong += playerGuess;
                Game.NextPlayerTurn();
            }
        }

        public void BuyVowel(Player currentPlayer)
        {
            Console.Clear();
            currentPlayer.ShowTurn(-250);
            ShowWordPuzzle();
            ShowKeyboard("vowels", currentPlayer.Color);
            char buyingVowel = currentPlayer.BuyVowel(lettersCorrect + lettersWrong);
            if (phrase.Contains(buyingVowel))
            {
                lettersCorrect += buyingVowel;
                if (!VowelsRemaining())
                {
                    Console.WriteLine("There are no vowels remaining!");
                    Console.ReadLine();
                }

            }
            else
            {
                lettersWrong += buyingVowel;
            }
        }

        public void ShowWordPuzzle()
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

        public void ShowKeyboard(string keyboardMode, string playerColor)
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

        int GuessedLetterCount(char guessedLetter)
        {
            int count = 0;
            foreach (char letter in phrase)
            {
                count = (letter == guessedLetter) ? count + 1 : count;
            }
            return count;
        }

        public bool ConsonantsRemaining()
        {
            for (char consonantCheck = 'B'; consonantCheck <= 'Z'; consonantCheck++)
            {
                if (!(lettersCorrect + lettersWrong).Contains(consonantCheck)
                    && !IsVowel(consonantCheck.ToString()))
                    return true;
            }
            return false;
        }

        public bool VowelsRemaining()
        {
            foreach (char vowel in vowels)
            {
                if (!(lettersCorrect + lettersWrong).Contains(vowel)
                    && phrase.Contains(vowel))
                    return true;
            }
            return false;
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

    public static class Gameplay
    {
        // i literally forgot what this class is for
    }
}