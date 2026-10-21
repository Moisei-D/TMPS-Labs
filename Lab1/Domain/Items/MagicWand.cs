using TMPSLabs.Lab1.Abstractions;

namespace TMPSLabs.Lab1.Domain.Items;

public class MagicWand : IWeapon
{
    public string Name => "Magic Wand";

    public int CalculateDamage(IRandomGenerator random)
    {
        return random.Next(1, 6);
    }
}