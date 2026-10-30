using TMPSLabs.Lab2.Abstractions;


namespace TMPSLabs.Lab2.Domain.Items;

public interface IWeapon
{
    string Name { get; }
    int CalculateDamage(IRandomGenerator random);
}
