using TMPSLabs.Lab2.Abstractions;

namespace TMPSLabs.Lab2.Domain.Items;

public sealed class RustySword : IWeapon
{
    public string Name => "Rusty Sword";

    public int CalculateDamage(IRandomGenerator random)
    {
        return random.Next(3, 8);
    }
}
