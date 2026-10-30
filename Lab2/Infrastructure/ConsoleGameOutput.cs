using TMPSLabs.Lab2.Abstractions;

namespace TMPSLabs.Lab2.Infrastructure;

public sealed class ConsoleGameOutput : IGameOutput
{
    public void Write(string message, OutputColor color = OutputColor.Default)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = GetConsoleColor(color, previousColor);
        Console.Write(message);
        Console.ForegroundColor = previousColor;
    }

    public void WriteLine(string message = "", OutputColor color = OutputColor.Default)
    {
        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = GetConsoleColor(color, previousColor);
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    private static ConsoleColor GetConsoleColor(
        OutputColor color,
        ConsoleColor defaultColor)
    {
        return color switch
        {
            OutputColor.Red => ConsoleColor.Red,
            OutputColor.Green => ConsoleColor.Green,
            OutputColor.Yellow => ConsoleColor.Yellow,
            OutputColor.Blue => ConsoleColor.Blue,
            OutputColor.Cyan => ConsoleColor.Cyan,
            OutputColor.DarkGray => ConsoleColor.DarkGray,
            _ => defaultColor
        };
    }
}
