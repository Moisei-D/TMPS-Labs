using TMPSLabs.Lab2.Abstractions;

namespace TMPSLabs.Lab2.Infrastructure;

public sealed class ConsoleGameInput : IGameInput
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}
