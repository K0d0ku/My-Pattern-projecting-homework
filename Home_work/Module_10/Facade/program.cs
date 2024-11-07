/*output:
Setting up to watch a movie...
TV is turned on.
Audio system is turned on.
Audio volume set to 10.
DVD is playing - Openheimer
Movie is now playing.

Setting up to start a game...
TV is turned on.
Audio system is turned on.
Audio volume set to 8.
Game console is turned on.
Starting game: Riders Republic
Game has started.

Setting up to listen to music...
TV is turned on.
Audio system is turned on.
Audio volume set to 6.
Music is now playing through TV and audio system.

Shutting down the home theater system...
DVD is stopped.
TV is turned off.
Audio system is turned off.
Game console is turned on.
System is now off.
*/
using System;
/*Facade*/
public class program
{
    public static void Main(string[] args)
    {
        TV tv = new TV();
        AudioSystem audio = new AudioSystem();
        DVDPlayer dvdPlayer = new DVDPlayer();
        GameConsole gameConsole = new GameConsole();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audio, dvdPlayer, gameConsole);

        homeTheater.WatchMovie();
        Console.WriteLine();

        homeTheater.StartGame();
        Console.WriteLine();

        homeTheater.ListenToMusic();
        Console.WriteLine();

        homeTheater.TurnOffSystem();
    }
}
public class TV
{
    public void TurnOn()
    {
        Console.WriteLine("TV is turned on.");
    }
    public void TurnOff()
    {
        Console.WriteLine("TV is turned off.");
    }
    public void SelectChannel(int channel)
    {
        Console.WriteLine($"TV channel set to {channel}.");
    }
}
public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Audio system is turned on.");
    }
    public void SetVolume(int level)
    {
        Console.WriteLine($"Audio volume set to {level}.");
    }
    public void TurnOff()
    {
        Console.WriteLine("Audio system is turned off.");
    }
}
public class DVDPlayer
{
    public void Play(string name)
    {
        Console.WriteLine($"DVD is playing - {name}");
    }
    public void Pause()
    {
        Console.WriteLine("DVD is paused.");
    }
    public void Stop()
    {
        Console.WriteLine("DVD is stopped.");
    }
}
public class GameConsole
{
    public void TurnOn()
    {
        Console.WriteLine("Game console is turned on.");
    }
    public void StartGame(string game)
    {
        Console.WriteLine($"Starting game: {game}");
    }
}
public class HomeTheaterFacade
{
    private TV _tv;
    private AudioSystem _audioSystem;
    private DVDPlayer _dvdPlayer;
    private GameConsole _gameConsole;
    public HomeTheaterFacade(TV tv, AudioSystem audioSystem, DVDPlayer dvdPlayer, GameConsole gameConsole)
    {
        _tv = tv;
        _audioSystem = audioSystem;
        _dvdPlayer = dvdPlayer;
        _gameConsole = gameConsole;
    }
    public void WatchMovie()
    {
        Console.WriteLine("Setting up to watch a movie...");
        _tv.TurnOn();
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(10);
        _dvdPlayer.Play("Openheimer");
        Console.WriteLine("Movie is now playing.");
    }
    public void StartGame()
    {
        Console.WriteLine("Setting up to start a game...");
        _tv.TurnOn();
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(8);
        _gameConsole.TurnOn();
        _gameConsole.StartGame("Riders Republic");
        Console.WriteLine("Game has started.");
    }
    public void ListenToMusic()
    {
        Console.WriteLine("Setting up to listen to music...");
        _tv.TurnOn();
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(6);
        Console.WriteLine("Music is now playing through TV and audio system.");
    }
    public void TurnOffSystem()
    {
        Console.WriteLine("Shutting down the home theater system...");
        _dvdPlayer.Stop();
        _tv.TurnOff();
        _audioSystem.TurnOff();
        _gameConsole.TurnOn();
        Console.WriteLine("System is now off.");
    }
}
