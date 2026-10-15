namespace TMPSLabs.Lab1.Domain.Characters;

public sealed class Enemy
{
    public Enemy(
        string name,
        int health,
        int minimumDamage,
        int maximumDamage)
    {
        Name = name;
        Health = health;
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }

    public string Name { get; }
    public int Health { get; private set; }
    public int MinimumDamage { get; }
    public int MaximumDamage { get; }
    public bool IsAlive => Health > 0;

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }
}
