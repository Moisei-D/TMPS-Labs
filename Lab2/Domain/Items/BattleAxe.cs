using TMPSLabs.Lab2.Abstractions;

namespace TMPSLabs.Lab2.Domain.Items;

public class BattleAxe : IWeapon
{
    public string Name => "Battle Axe";

    public int CalculateDamage(IRandomGenerator random)
    {
        return random.Next(5, 11);
    }
}
