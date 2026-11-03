namespace TMPSLabs.Lab2.Domain.World;

public enum Direction
{
    North,
    East,
    South,
    West,
}

public static class DirectionExtensions
{
    public static Direction Opposite(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.South,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            _ => Direction.East
        };
    }

    public static GridPosition Move(this GridPosition position, Direction direction)
    {
        return direction switch
        {
            Direction.North => position with { Y = position.Y - 1 },
            Direction.East => position with { X = position.X + 1 },
            Direction.South => position with { Y = position.Y + 1 },
            _ => position with { X = position.X - 1 }
        };
    }
}
