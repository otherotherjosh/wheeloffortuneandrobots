using System;

/// <summary>
/// where players game data is generated and stored
/// </summary>
public class Player
{
    public string name;
    public int score;
    public string color;
    public double money = 0;

    // deal with this shit later
    public char GuessLetter()
    {
        return 'A';
    }

    public void DisplayTurn()
    {
        Decor.TextColor(color);
        Console.WriteLine($"{name}'s turn:\n{money:C}");
    }
}


public class AI : Player
{
    //public char GuessLetter()
    //{
    //    Random random = new Random();
    //    int letterShift = random.Next(26);
    //    return (char)('A' + letterShift);
    //}
}
