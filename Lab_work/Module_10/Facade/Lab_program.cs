/*output:
Preparing to start the movie...
Lights are turned on.
Lights brightness is set to 5.
Audio system is turned on.
Audio volume is set to 8.
Video projector is turned on.
Video resolution is set to HD.
Movie started.

Shutting down movie...
Video projector is turned off.
Audio system is turned off.
Lights are turned off.
Movie ended.*/

using System;
/*Facade - Lab*/
public class program
{
    public static void Main(string[] args)
    {
        // Create subsystems
        AudioSystem audio = new AudioSystem();
        VideoProjector video = new VideoProjector();
        LightingSystem lights = new LightingSystem();
        // Create facade
        HomeTheaterFacade homeTheater = new HomeTheaterFacade(audio, video, lights);
        // Start the movie
        homeTheater.StartMovie();
        Console.WriteLine();
        // End the movie
        homeTheater.EndMovie();
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
        Console.WriteLine($"Audio volume is set to {level}.");
    }
    public void TurnOff()
    {
        Console.WriteLine("Audio system is turned off.");
    }
}
public class VideoProjector
{
    public void TurnOn()
    {
        Console.WriteLine("Video projector is turned on.");
    }
    public void SetResolution(string resolution)
    {
        Console.WriteLine($"Video resolution is set to {resolution}.");
    }
    public void TurnOff()
    {
        Console.WriteLine("Video projector is turned off.");
    }
}
public class LightingSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Lights are turned on.");
    }
    public void SetBrightness(int level)
    {
        Console.WriteLine($"Lights brightness is set to {level}.");
    }
    public void TurnOff()
    {
        Console.WriteLine("Lights are turned off.");
    }
}
public class HomeTheaterFacade
{
    private AudioSystem _audioSystem;
    private VideoProjector _videoProjector;
    private LightingSystem _lightingSystem;
    public HomeTheaterFacade(AudioSystem audioSystem, VideoProjector videoProjector, LightingSystem lightingSystem)
    {
        _audioSystem = audioSystem;
        _videoProjector = videoProjector;
        _lightingSystem = lightingSystem;
    }
    public void StartMovie()
    {
        Console.WriteLine("Preparing to start the movie...");
        _lightingSystem.TurnOn();
        _lightingSystem.SetBrightness(5);
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(8);
        _videoProjector.TurnOn();
        _videoProjector.SetResolution("HD");
        Console.WriteLine("Movie started.");
    }
    public void EndMovie()
    {
        Console.WriteLine("Shutting down movie...");
        _videoProjector.TurnOff();
        _audioSystem.TurnOff();
        _lightingSystem.TurnOff();
        Console.WriteLine("Movie ended.");
    }
}
