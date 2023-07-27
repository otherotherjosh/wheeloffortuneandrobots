public class Decor
{
    public static void Highlight(string line)
    {
        string[] splitLines = line.Split('^');
        foreach (string splitLine in splitLines)
        {
            switch (splitLine)
            {
                case "b":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "e":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "g":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "r":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "y":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "w":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.Write(splitLine);
                    break;
            }
        }
    }

    public static void TextColor(string color = "")
    {
        switch (color)
        {
            case "BLUE":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "CYAN":
            case "DEFAULT":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "GREEN":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "GRAY":
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
            case "MAGENTA":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case "PURPLE":
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                break;
            case "RED":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "YELLOW":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "WHITE":
            default:
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }

    public static void DebugLine()
    {
        Highlight("^r^(debug)^g^ ");
    }

    public static void InputLine(string color)
    {
        TextColor(color);
        Console.Write(">>> ");
    }

    public static void StandBy()
    {
        Highlight("\n" +
            "^g^Press ENTER to continue^w^");
        Console.ReadLine();
        TextColor();
    }
}
