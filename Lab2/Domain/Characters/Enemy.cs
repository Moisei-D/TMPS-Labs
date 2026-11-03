using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.Prototypes;

namespace TMPSLabs.Lab2.Domain.Characters;

public sealed class Enemy : IPrototype<Enemy>
{
    public Enemy(
        string name,
        int health,
        int minimumDamage,
        int maximumDamage,
        IWeapon? drop = null,
        bool isBoss = false)
    {
        Name = name;
        Health = health;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
        Drop = drop;
        IsBoss = isBoss;
    }

    public string Name { get; }
    public int Health { get; private set; }
    public int MinimumDamage { get; }
    public int MaximumDamage { get; }
    public IWeapon? Drop { get; }
    public bool IsBoss { get; }
    public bool IsAlive => Health > 0;

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }

    public Enemy Clone()
    {
        return new Enemy(
            Name,
            Health,
            MinimumDamage,
            MaximumDamage,
            Drop?.Clone(),
            IsBoss);
    }
}
