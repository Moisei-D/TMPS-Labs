namespace TMPSLabs.Lab1.Application;

public enum CommandType
{
    Look,
    Move,
    Attack,
    Stats,
    Help,
    Quit,
    Unknown
}

public sealed record GameCommand(CommandType Type, string? Argument = null);
