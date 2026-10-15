using TMPSLabs.Lab1.Abstractions;
using TMPSLabs.Lab1.Application;
using TMPSLabs.Lab1.Infrastructure;
using TMPSLabs.Lab1.Services;

IGameInput input = new ConsoleGameInput();
IGameOutput output = new ConsoleGameOutput();
IRandomGenerator random = new SystemRandomGenerator();

var player = InitialDungeon.CreatePlayer();
var commandParser = new CommandParser();
var combatService = new CombatService(random, output);
var game = new Game(
    player,
    commandParser,
    combatService,
    input,
    output);

game.Run();
