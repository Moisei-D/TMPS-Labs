using TMPSLabs.Lab1.Abstractions;
using TMPSLabs.Lab1.Domain.Characters;

namespace TMPSLabs.Lab1.Services;

public sealed class CombatService
{
    private readonly IRandomGenerator _random;
    private readonly IGameOutput _output;

    public CombatService(IRandomGenerator random, IGameOutput output)
    {
        _random = random;
        _output = output;
    }

    public void ResolveRound(Player player, Enemy enemy)
    {
        int playerDamage = player.EquippedWeapon.CalculateDamage(_random);
        enemy.TakeDamage(playerDamage);

        _output.WriteLine(
            $"You strike the {enemy.Name} with your {player.EquippedWeapon.Name} " +
            $"for {playerDamage} damage.",
            OutputColor.Yellow);

        if (!enemy.IsAlive)
        {
            _output.WriteLine($"The {enemy.Name} is defeated.", OutputColor.Green);
            return;
        }

        int enemyDamage = _random.Next(
            enemy.MinimumDamage,
            enemy.MaximumDamage + 1);

        player.TakeDamage(enemyDamage);
        _output.WriteLine(
            $"The {enemy.Name} hits you for {enemyDamage} damage.",
            OutputColor.Red);
    }
}
