namespace TMPSLabs.Lab2.Application;

public enum CommandType
{
    Look,
    Move,
    Attack,
    Stats,
    Take,
    Equip,
    Help,
    Quit,
    Unknown
}

public sealed record GameCommand(CommandType Type, string? Argument = null);
