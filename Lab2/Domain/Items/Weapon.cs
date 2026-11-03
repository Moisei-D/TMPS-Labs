namespace TMPSLabs.Lab2.Domain.Items;

public sealed class Weapon : IWeapon
{
    public Weapon(string name, int minimumDamage, int maximumDamage)
    {
        Name = name;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }

    public string Name { get; }
    public int MinimumDamage { get; }
    public int MaximumDamage { get; }

    public IWeapon Clone()
    {
        return new Weapon(Name, MinimumDamage, MaximumDamage);
    }
}
