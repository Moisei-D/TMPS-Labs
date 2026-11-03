using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Domain.Items;
using TMPSLabs.Lab2.Domain.Prototypes;

namespace TMPSLabs.Lab2.Services;

public static class EnemyPrototypes
{
    public static PrototypeRegistry<Enemy> CreateRegistry(
        PrototypeRegistry<IWeapon> weapons)
    {
        return new PrototypeRegistry<Enemy>()
            .Register("skeleton-guard", new Enemy(
                "skeleton guard", 12, 2, 5, weapons.Create("iron-sword")))
            .Register("troll", new Enemy(
                "troll", 20, 4, 7, weapons.Create("battle-axe")))
            .Register("bone-drake", new Enemy(
                "bone drake", 30, 5, 9, isBoss: true));
    }
}
