using System;

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
    static bool debugMessagesOn = false;

    private string name;
    private string color;
    int money;

    public string Name { get; set; }
    public string Color { get; set; }

    public Player(string name, string color = "DEFAULT")
    {
        this.Name = name;
        this.Color = color;
        this.money = 0;
    }

    public void ReceiveMoney(int amount)
    {
        money += amount;
        if (debugMessagesOn)
        {
            Decor.DebugLine();
            Decor.Highlight($"{name} has {money:C} (+{amount:C})\n");
        }
    }

    public void Bankrupt()
    {
        int amountLost = money;
        money = 0;
        if (debugMessagesOn)
        {
            Decor.DebugLine();
            Decor.Highlight($"{name} has {money:C} (-{amountLost:C})\n");
        }
    }

    public void ShowTurn(int playingFor = 0)
    {
        Decor.TextColor(Color);
        Console.WriteLine($"{Name}'s turn");
        Console.WriteLine($"{money:C0}");
        if (playingFor != 0)
        {
            Decor.TextColor("GRAY");
            Console.WriteLine($"playing for {playingFor:C}");
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