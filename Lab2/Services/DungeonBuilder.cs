using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.Prototypes;
using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Services;

public sealed class DungeonBuilder : IDungeonBuilder
{
    private readonly PrototypeRegistry<Enemy> _enemies;
    private readonly Dictionary<string, Room> _rooms = new();
    private Room? _start;

    public DungeonBuilder(PrototypeRegistry<Enemy> enemies)
    {
        _enemies = enemies;
    }

    public IDungeonBuilder AddRoom(
        string id,
        string name,
        string description,
        int x,
        int y,
        string? enemyKey = null)
    {
        Enemy? enemy = enemyKey is null ? null : _enemies.Create(enemyKey);
        _rooms[id] = new Room(name, description, new GridPosition(x, y), enemy);
        return this;
    }

    public IDungeonBuilder Connect(string fromId, Direction direction, string toId)
    {
        _rooms[fromId].AddExit(direction, _rooms[toId]);
        _rooms[toId].AddExit(direction.Opposite(), _rooms[fromId]);
        return this;
    }

    public IDungeonBuilder StartAt(string id)
    {
        _start = _rooms[id];
        return this;
    }

    public Dungeon Build()
    {
        if (_start is null)
        {
            throw new InvalidOperationException("The dungeon needs a start room.");
        }

        return new Dungeon(_start, _rooms.Values.ToList());
    }
}
