# Lab 2

Lab 1 dungeon game extended with three creational patterns.

| Pattern   | Where                                                              | Purpose                                                        |
|-----------|--------------------------------------------------------------------|----------------------------------------------------------------|
| Singleton | `Configuration/GameConfig.cs`                                      | Global difficulty (easy/medium/hard) that biases damage rolls  |
| Builder   | `Services/DungeonBuilder.cs`, `Services/InitialDungeon.cs`         | Fluent construction of the 7-room dungeon |
| Prototype | `Domain/Prototypes/`, `Services/EnemyPrototypes.cs`, `WeaponPrototypes.cs` | Enemies and weapons are cloned from registered templates |

New gameplay: weapon drops (`take`, `equip`), a boss, and a map of explored
rooms printed together with the room description (`look`).

## Run

```bash
dotnet run --project Lab2
```
