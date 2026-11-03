using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Services;

public interface IDungeonBuilder
{
    IDungeonBuilder AddRoom(
        string id,
        string name,
        string description,
        int x,
        int y,
        string? enemyKey = null);

    IDungeonBuilder Connect(string fromId, Direction direction, string toId);

    IDungeonBuilder StartAt(string id);

    Dungeon Build();
}
