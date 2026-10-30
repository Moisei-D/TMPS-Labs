using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Services;

public static class InitialDungeon
{
    public static Player CreatePlayer()
    {
        var entrance = new Room(
            "Old Gate",
            "Cold air flows through a cracked stone archway.");

        var crypt = new Room(
            "Dusty Crypt",
            "Faded names cover the walls and loose stones crunch underfoot.",
            new Enemy(
                "skeleton guard",
                health: 12,
                minimumDamage: 2,
                maximumDamage: 5));

        var shrine = new Room(
            "Silent Shrine",
            "A pale beam of light falls across an empty pedestal.");

        entrance.AddExit(Direction.North, crypt);
        crypt.AddExit(Direction.South, entrance);
        crypt.AddExit(Direction.East, shrine);
        shrine.AddExit(Direction.West, crypt);

        return new Player(
            "Adventurer",
            maximumHealth: 25,
            new RustySword(),
            entrance);
    }
}
