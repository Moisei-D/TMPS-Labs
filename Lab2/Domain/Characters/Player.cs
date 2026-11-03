using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.World;

namespace TMPSLabs.Lab2.Domain.Characters;

public sealed class Player
{
    private readonly List<IWeapon> _inventory = new();

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
        _inventory.Add(equippedWeapon);
        currentRoom.MarkVisited();
    }

    public string Name { get; }

    public int MaximumHealth { get; }

    public int Health { get; private set; }

    public IWeapon EquippedWeapon { get; private set; }

    public IReadOnlyList<IWeapon> Inventory => _inventory;

    public Room CurrentRoom { get; private set; }

    public bool IsAlive => Health > 0;

    public void MoveTo(Room room)
    {
        CurrentRoom = room;
        room.MarkVisited();
    }

    public void AddWeapon(IWeapon weapon)
    {
        _inventory.Add(weapon);
    }

    public bool TryEquip(string weaponName)
    {
        IWeapon? weapon = _inventory.FirstOrDefault(
            item => item.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));

        if (weapon is null)
        {
            return false;
        }

        EquippedWeapon = weapon;
        return true;
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }
}
