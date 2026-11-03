using TMPSLabs.Lab2.Abstractions;
using TMPSLabs.Lab2.Application;
using TMPSLabs.Lab2.Configuration;
using TMPSLabs.Lab2.Domain.Characters;
using TMPSLabs.Lab2.Infrastructure;
using TMPSLabs.Lab2.Services;

IGameInput input = new ConsoleGameInput();
IGameOutput output = new ConsoleGameOutput();
IRandomGenerator random = new SystemRandomGenerator();
IGameConfig config = GameConfig.Instance;

var weapons = WeaponPrototypes.CreateRegistry();
var enemies = EnemyPrototypes.CreateRegistry(weapons);
var dungeon = InitialDungeon.Create(new DungeonBuilder(enemies));

var player = new Player(
    "Adventurer",
    maximumHealth: 40,
    weapons.Create("rusty-sword"),
    dungeon.StartRoom);
var commandParser = new CommandParser();
var combatService = new CombatService(random, output, config);
var mapRenderer = new MapRenderer(dungeon);
var game = new Game(
    player,
    commandParser,
    combatService,
    mapRenderer,
    config,
    input,
    output);

game.Run();
