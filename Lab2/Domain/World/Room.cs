using TMPSLabs.Lab2.Domain.Characters;

namespace TMPSLabs.Lab2.Domain.World;

public sealed class Room
{
    private readonly Dictionary<Direction, Room> _exits = new();

    public Room(string name, string description, Enemy? enemy = null)
    {
        Name = name;
        Description = description;
        Enemy = enemy;
    }

    public string Name { get; }

    public string Description { get; }

    public Enemy? Enemy { get; }

    public IEnumerable<Direction> AvailableDirections => _exits.Keys;

    public void AddExit(Direction direction, Room destination)
    {
        _exits[direction] = destination;
    }

    public bool TryGetExit(Direction direction, out Room? destination)
    {
        return _exits.TryGetValue(direction, out destination);
    }
}
