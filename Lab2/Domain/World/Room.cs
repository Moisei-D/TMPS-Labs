using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.Items;

namespace TMPSLabs.Lab2.Domain.World;

public sealed class Room
{
    private readonly Dictionary<Direction, Room> _exits = new();

    public Room(
        string name,
        string description,
        GridPosition position,
        Enemy? enemy = null)
    {
        Name = name;
        Description = description;
        Position = position;
        Enemy = enemy;
    }

    public string Name { get; }

    public string Description { get; }

    public GridPosition Position { get; }

    public Enemy? Enemy { get; }

    public bool Visited { get; private set; }

    public IWeapon? Loot { get; private set; }

    public IEnumerable<Direction> AvailableDirections => _exits.Keys;

    public void AddExit(Direction direction, Room destination)
    {
        _exits[direction] = destination;
    }

    public bool TryGetExit(Direction direction, out Room? destination)
    {
        return _exits.TryGetValue(direction, out destination);
    }

    public void MarkVisited()
    {
        Visited = true;
    }

    public void PlaceLoot(IWeapon weapon)
    {
        Loot = weapon;
    }

    public IWeapon? TakeLoot()
    {
        IWeapon? weapon = Loot;
        Loot = null;
        return weapon;
    }
}
