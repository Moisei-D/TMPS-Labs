using TMPSLabs.Lab1.Abstractions;


namespace TMPSLabs.Lab1.Domain.Items;

public interface IWeapon
{
    string Name { get; }
    int CalculateDamage(IRandomGenerator random);
}
