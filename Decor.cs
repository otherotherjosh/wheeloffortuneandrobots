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

    public static void TextColor(string color)
    {
        switch (color)
        {
            case "BLUE":
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case "CYAN":
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case "GREEN":
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case "MAGENTA":
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case "RED":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "YELLOW":
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case "RESET":
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }
}
