using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Services;

public sealed class MapRenderer
{
    private readonly Dungeon _dungeon;

    public MapRenderer(Dungeon dungeon)
    {
        _dungeon = dungeon;
    }

    public IEnumerable<string> Render(Player player)
    {
        var explored = _dungeon.Rooms.Where(room => room.Visited).ToList();

        for (int y = explored.Min(r => r.Position.Y); y <= explored.Max(r => r.Position.Y); y++)
        {
            string line = "";

            for (int x = explored.Min(r => r.Position.X); x <= explored.Max(r => r.Position.X); x++)
            {
                Room? room = explored.FirstOrDefault(r => r.Position == new GridPosition(x, y));
                line += room is null ? "   " : room == player.CurrentRoom ? "[@]" : "[ ]";
            }

            yield return line.TrimEnd();
        }
    }
}
