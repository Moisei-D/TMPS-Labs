namespace TMPSLabs.Lab2.Domain.World;

public sealed class Dungeon
{
    public Dungeon(Room startRoom, IReadOnlyList<Room> rooms)
    {
        StartRoom = startRoom;
        Rooms = rooms;
    }

    public Room StartRoom { get; }

    public IReadOnlyList<Room> Rooms { get; }
}
