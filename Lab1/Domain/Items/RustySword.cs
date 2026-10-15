using TMPSLabs.Lab1.Abstractions;

namespace TMPSLabs.Lab1.Domain.Items;

public sealed class RustySword : IWeapon
{
    public string Name => "Rusty Sword";

    public int CalculateDamage(IRandomGenerator random)
    {
        return random.Next(3, 8);
    }
}
