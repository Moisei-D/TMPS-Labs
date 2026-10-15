using TMPSLabs.Lab1.Domain.Items;
using TMPSLabs.Lab1.Domain.World;

namespace TMPSLabs.Lab1.Domain.Characters;

public sealed class Player
{
    public Player(
        string name,
        int maximumHealth,
        IWeapon equippedWeapon,
        Room currentRoom)
    {
        Name = name;
        MaximumHealth = maximumHealth;
        Health = maximumHealth;
        EquippedWeapon = equippedWeapon;
        CurrentRoom = currentRoom;
    }

    public string Name { get; }

    public int MaximumHealth { get; }

    public int Health { get; private set; }

    public IWeapon EquippedWeapon { get; private set; }

    public Room CurrentRoom { get; private set; }

    public bool IsAlive => Health > 0;

    public void MoveTo(Room room)
    {
        CurrentRoom = room;
    }

    public void Equip(IWeapon weapon)
    {
        EquippedWeapon = weapon;
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }
}