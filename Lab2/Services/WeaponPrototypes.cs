using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.Prototypes;

namespace TMPSLabs.Lab2.Services;

public static class WeaponPrototypes
{
    public static PrototypeRegistry<IWeapon> CreateRegistry()
    {
        return new PrototypeRegistry<IWeapon>()
            .Register("rusty-sword", new Weapon("Rusty Sword", 3, 7))
            .Register("iron-sword", new Weapon("Iron Sword", 4, 8))
            .Register("battle-axe", new Weapon("Battle Axe", 5, 10));
    }
}
