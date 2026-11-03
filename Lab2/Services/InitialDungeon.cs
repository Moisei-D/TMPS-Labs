using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Services;

public static class InitialDungeon
{
    public static Dungeon Create(IDungeonBuilder builder)
    {
        return builder
            .AddRoom("gate", "Old Gate",
                "Cold air flows through a cracked stone archway.", 0, 2)
            .AddRoom("crypt", "Dusty Crypt",
                "Faded names cover the walls and loose stones crunch underfoot.", 0, 1,
                "skeleton-guard")
            .AddRoom("shrine", "Silent Shrine",
                "A pale beam of light falls across an empty pedestal.", 1, 1)
            .AddRoom("ossuary", "Ossuary",
                "Skulls are stacked floor to ceiling in neat rows.", 1, 2)
            .AddRoom("armory", "Broken Armory",
                "Rusted racks and shattered shields litter the floor.", 1, 0,
                "troll")
            .AddRoom("hall", "Torch Hall",
                "Burnt-out torches line a long, soot-stained corridor.", 0, 0)
            .AddRoom("lair", "Drake Lair",
                "A mountain of bones rises in the dark. It is breathing.", 2, 0,
                "bone-drake")
            .Connect("gate", Direction.North, "crypt")
            .Connect("crypt", Direction.East, "shrine")
            .Connect("crypt", Direction.North, "hall")
            .Connect("shrine", Direction.South, "ossuary")
            .Connect("shrine", Direction.North, "armory")
            .Connect("hall", Direction.East, "armory")
            .Connect("armory", Direction.East, "lair")
            .StartAt("gate")
            .Build();
    }
}
