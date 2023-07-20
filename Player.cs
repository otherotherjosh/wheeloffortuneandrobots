using System;

/// <summary>
/// where players game data is generated and stored
/// </summary>
public class Player
{
    // deal with this shit later
    public char GuessLetter()
    {
        return 'A';
    }
}


public class AI : Player
{
    // deal with this shit later
    public char GuessLetter()
    {
        Random random = new Random();
        return ' ';
    }
}
