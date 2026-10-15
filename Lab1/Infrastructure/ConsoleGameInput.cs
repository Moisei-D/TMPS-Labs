using TMPSLabs.Lab1.Abstractions;

namespace TMPSLabs.Lab1.Infrastructure;

public sealed class ConsoleGameInput : IGameInput
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}
