using System;
using wheeloffortuneandrobots;

/// <summary>
/// where players game data is stored
/// </summary>
public struct PlayerBase
{
    public string name;
    public string color;
}

public class Player
{
    static bool debugMessagesOn = true;

    private string name;
    private string color;
    private int money;

    public string Name { get; set; }
    public string Color { get; set; }
    public int Money { get; set; }

    public Player(string name, string color = "DEFAULT")
    {
        this.Name = name;
        this.Color = color;
        this.Money = 0;
    }

    public string RoundMenu()
    {
        Decor.InputLine(Color);
        string userInput = Console.ReadLine();
        return userInput;
    }

    public void ReceiveMoney(int amount)
    {
        Money += amount;
        if (debugMessagesOn)
        {
            Decor.DebugLine();
            Decor.Highlight($"{Name} has ${Money} (+${amount})\n");
            Decor.StandBy();
        }
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
        if (debugMessagesOn)
        {
            Decor.DebugLine();
            Decor.Highlight($"{Name} has ${Money} (-${amount})\n");
            Decor.StandBy();
        }
    }

    public void Bankrupt()
    {
        int amountLost = Money;
        Money = 0;
        if (debugMessagesOn)
        {
            Decor.DebugLine();
            Decor.Highlight($"{name} has ${Money} (-${amountLost:C})\n");
            Decor.StandBy();
        }
    }

    public char GuessConsonant(string lettersUsed)
    {
        bool validGuess = false;
        string userInput;
        char guess;
        do
        {
            Decor.InputLine(Color);
            userInput = Console.ReadLine()
                .ToUpper()
                .Replace(" ", "");
            validGuess = (WordPuzzle.IsLetter(userInput)
                && !lettersUsed.Contains(userInput)
                && !WordPuzzle.IsVowel(userInput));
            if (!validGuess)
            {
                Decor.TextColor("GRAY");
                Console.Write("    ");
                if (userInput.Length == 0)
                    Console.WriteLine("please input a letter");
                else if (userInput.Length > 1)
                    Console.WriteLine("cannot input more than 1 letter");
                else if (!WordPuzzle.IsLetter(userInput))
                    Console.WriteLine("cannot enter that symbol");
                else if (WordPuzzle.IsVowel(userInput))
                    Console.WriteLine("cannot input a vowel");
                else if (lettersUsed.Contains(userInput))
                    Console.WriteLine("that letter has already been guessed");
                else
                    Console.WriteLine("error: womp (unknown error)");
            }
            Console.WriteLine();
            Decor.TextColor();
        } while (!validGuess);
        guess = Convert.ToChar(userInput);
        return guess;
    }

    public char BuyVowel(string lettersUsed)
    {
        bool validVowel = false;
        string userInput;
        char buyingVowel;
        do
        {
            Decor.InputLine(Color);
            userInput = Console.ReadLine()
                .ToUpper()
                .Replace(" ", "");
            validVowel = (WordPuzzle.IsLetter(userInput)
                && !lettersUsed.Contains(userInput)
                && WordPuzzle.IsVowel(userInput));
            if (!validVowel)
            {
                Decor.TextColor("GRAY");
                Console.Write("    ");
                if (userInput.Length == 0)
                    Console.WriteLine("please input a letter");
                else if (userInput.Length > 1)
                    Console.WriteLine("cannot input more than 1 letter");
                else if (!WordPuzzle.IsLetter(userInput))
                    Console.WriteLine("cannot enter that symbol");
                else if (!WordPuzzle.IsVowel(userInput))
                    Console.WriteLine("cannot input a consonant");
                else if (lettersUsed.Contains(userInput))
                    Console.WriteLine("that letter has already been guessed");
                else
                    Console.WriteLine("error: wump (unknown error)");
            }
            Console.WriteLine();
            Decor.TextColor();
        } while (!validVowel);
        buyingVowel = Convert.ToChar(userInput);
        SpendMoney(250);
        return buyingVowel;
    }

    public void ShowTurn(int value = 0)
    {
        Decor.TextColor(Color);
        Console.WriteLine($"{Name}'s turn");
        Console.WriteLine($"${Money}");
        if (value > 0)
        {
            Decor.TextColor("GRAY");
            Console.WriteLine($"playing for ${value}");
        }
        else if (value < 0)
        {
            Decor.TextColor("GRAY");
            Console.WriteLine($"buying for -${0-value}");
        }
        Decor.TextColor();
    }

    public static Player[] Select()
    {
        int numOfPlayers = 4;  // range from 2 - 4
        Player[] players = new Player[numOfPlayers];
        players[0] = new Player("Josh", "MAGENTA");
        players[1] = new Player("Keyra", "GREEN");
        players[2] = new Player("Jenna", "PURPLE");
        players[3] = new Player("Deon", "CYAN");
        return players;
    }
}


//public class AI : Player
//{
    
//}