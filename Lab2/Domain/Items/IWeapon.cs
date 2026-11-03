using TMPSLabs.Lab2.Domain.Prototypes;

namespace TMPSLabs.Lab2.Domain.Items;

public interface IWeapon : IPrototype<IWeapon>
{
    string Name { get; }
    int MinimumDamage { get; }
    int MaximumDamage { get; }
}
