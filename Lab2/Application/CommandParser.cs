namespace TMPSLabs.Lab2.Application;

public sealed class CommandParser
{
    private static readonly HashSet<string> Directions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "north",
            "east",
            "south",
            "west"
        };

    public GameCommand Parse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return new GameCommand(CommandType.Unknown);
        }

        string[] parts = input.Trim().Split(
            ' ',
            2,
            StringSplitOptions.RemoveEmptyEntries);

        string verb = parts[0].ToLowerInvariant();
        string? argument = parts.Length == 2 ? parts[1].Trim() : null;

        if (Directions.Contains(verb))
        {
            return new GameCommand(CommandType.Move, verb);
        }

        return verb switch
        {
            "look" => new GameCommand(CommandType.Look),
            "move" or "go" => new GameCommand(CommandType.Move, argument),
            "attack" or "fight" => new GameCommand(CommandType.Attack),
            "stats" => new GameCommand(CommandType.Stats),
            "help" => new GameCommand(CommandType.Help),
            "quit" or "exit" => new GameCommand(CommandType.Quit),
            _ => new GameCommand(CommandType.Unknown)
        };
    }
}
