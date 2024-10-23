/*Console output of the code:
10/23/2024 5:33:38 PM: executing macro command (turn on all devices)

executing macro command (turn on all devices):
svet vkluchen.
televisor vkluchen.
conditioner is on.
10/23/2024 5:33:39 PM: executing macro command (turn off all devices)

executing macro command (turn off all devices):
svet vykluchen.
televisor vykluchen.
conditioner is off.
10/23/2024 5:33:39 PM: switching to energy-saving mode

switching to energy-saving mode:
conditioner is in energy-saving mode.
conditioner is in regular mode.

C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lab\8\LAB_8_Module_8\bin\Debug\net6.0\LAB_8_Module_8_Command.exe (process 28712) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .*/

using System;
using System.Runtime.InteropServices;

public class porgram
{
    private static string _logFilePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lab\8\logs\log.txt";
    public static void Main(string[] args)
    {
        // Создаем устройства
        Light livingRoomLight = new Light();
        Television tv = new Television();
        /*my code*/
        Conditioner conditioner = new Conditioner();
        /*
        // Создаем команды для управления светом
        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand lightOff = new LightOffCommand(livingRoomLight);

        // Создаем команды для управления телевизором
        ICommand tvOn = new TelevisionOnCommand(tv);
        ICommand tvOff = new TelevisionOffCommand(tv);

        *//*my code*//*
        ICommand conditionerOn = new ConditionerOnCommand(conditioner);
        ICommand conditionerOff = new ConditionerOffCommand(conditioner);

        // Создаем пульт и привязываем команды к кнопкам
        RemoteControl remote = new RemoteControl();

        // Управляем светом
        remote.SetCommands(lightOn, lightOff);
        Console.WriteLine("upravlenye svetom:");
        remote.PressOnButton();
        remote.PressOffButton();
        remote.PressUndoButton();

        // Управляем телевизором
        remote.SetCommands(tvOn, tvOff);
        Console.WriteLine("\nupravlenye televisora:");
        remote.PressOnButton();
        remote.PressOffButton();
        remote.PressUndoButton();

        *//*my code*//*
        remote.SetCommands(conditionerOn, conditionerOff);
        Console.WriteLine("\nconditioner controls:");
        remote.PressOnButton();
        remote.PressOffButton();
        remote.PressUndoButton();*/
        void LogAction(string message)
        {   /*i created a simple .txt loggin , i did not create a separate class for logging and just
             made it a method, i learnt from module 6 practice singleton pattern task where i had to 
            log output to various formats and show the log in console*/
            string logMessage = $"{DateTime.Now}: {message}";
            Console.WriteLine(logMessage);
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        ICommand lightOn = new LightOnCommand(livingRoomLight);
        ICommand tvOn = new TelevisionOnCommand(tv);
        ICommand conditionerOn = new ConditionerOnCommand(conditioner);
        ICommand allDevicesOn = new MacroCommand(new ICommand[] { lightOn, tvOn, conditionerOn });
        RemoteControl remoteon = new RemoteControl();
        remoteon.SetCommands(allDevicesOn, null);
        LogAction("executing macro command (turn on all devices)");
        Console.WriteLine("\nexecuting macro command (turn on all devices):");
        remoteon.PressOnButton();

        ICommand lightOff = new LightOffCommand(livingRoomLight);
        ICommand tvOff = new TelevisionOffCommand(tv);
        ICommand conditionerOff = new ConditionerOffCommand(conditioner);
        ICommand allDevicesOff = new MacroCommand(new ICommand[] { lightOff, tvOff, conditionerOff });
        RemoteControl remoteoff = new RemoteControl();
        remoteoff.SetCommands(allDevicesOff, null);
        LogAction("executing macro command (turn off all devices)");
        Console.WriteLine("\nexecuting macro command (turn off all devices):");
        remoteoff.PressOnButton();

        ICommand energySavingMode = new ConditionerEnergySavingModeCommand(conditioner);
        ICommand regularMode = new ConditionerRegularModeCommand(conditioner);
        RemoteControl remote = new RemoteControl();
        remote.SetCommands(energySavingMode, regularMode);
        LogAction("switching to energy-saving mode");
        Console.WriteLine("\nswitching to energy-saving mode:");
        remote.PressOnButton();
        remote.PressUndoButton();
    }
}
public interface ICommand
{
    void Execute();
    void Undo(); // Метод для отмены команды
}
public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.On();
    }

    public void Undo()
    {
        _light.Off();
    }
}
public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Off();
    }

    public void Undo()
    {
        _light.On();
    }
}
public class Light
{
    public void On()
    {
        Console.WriteLine("svet vkluchen.");
    }

    public void Off()
    {
        Console.WriteLine("svet vykluchen.");
    }
}
/*you kinda forgot to include this into code so i had to create this*/
public class TelevisionOnCommand : ICommand
{
    private Television _tv;
    public TelevisionOnCommand(Television tv)
    {
        _tv = tv;
    }
    public void Execute()
    {
        _tv.On();
    }
    public void Undo()
    {
        _tv.Off();
    }
}
public class TelevisionOffCommand : ICommand
{
    private Television _tv;
    public TelevisionOffCommand(Television tv)
    {
        _tv = tv;
    }
    public void Execute()
    {
        _tv.Off();
    }
    public void Undo()
    {
        _tv.On();
    }
}

public class Television
{
    public void On()
    {
        Console.WriteLine("televisor vkluchen.");
    }

    public void Off()
    {
        Console.WriteLine("televisor vykluchen.");
    }
}
/*my code*/
public class ConditionerOnCommand : ICommand
{
    private Conditioner _conditioner;
    public ConditionerOnCommand(Conditioner conditioner)
    {
        _conditioner = conditioner;
    }
    public void Execute()
    {
        _conditioner.On();
    }
    public void Undo()
    {
        _conditioner.Off();
    }
}
public class ConditionerOffCommand : ICommand
{
    private Conditioner _conditioner;
    public ConditionerOffCommand(Conditioner conditioner)
    {
        _conditioner = conditioner;
    }
    public void Execute()
    {
        _conditioner.Off();
    }
    public void Undo()
    {
        _conditioner.On();
    }
}
public class Conditioner
{
    public void On()
    {
        Console.WriteLine("conditioner is on.");
    }
    public void Off()
    {
        Console.WriteLine("conditioner is off.");
    }
    public void SetEnergySavingMode()
    {
        Console.WriteLine("conditioner is in energy-saving mode.");
    }
    public void SetRegularMode()
    {
        Console.WriteLine("conditioner is in regular mode.");
    }
}
public class ConditionerEnergySavingModeCommand : ICommand
{
    private Conditioner _conditioner;
    public ConditionerEnergySavingModeCommand(Conditioner conditioner)
    {
        _conditioner = conditioner;
    }
    public void Execute()
    {
        _conditioner.SetEnergySavingMode();
    }
    public void Undo()
    {
        _conditioner.SetRegularMode();
    }
}
public class ConditionerRegularModeCommand : ICommand
{
    private Conditioner _conditioner;
    public ConditionerRegularModeCommand(Conditioner conditioner)
    {
        _conditioner = conditioner;
    }
    public void Execute()
    {
        _conditioner.SetRegularMode();
    }
    public void Undo()
    {
        _conditioner.SetEnergySavingMode();
    }
}
public class MacroCommand : ICommand
{
    private ICommand[] _commands;
    public MacroCommand(ICommand[] commands)
    {
        _commands = commands;
    }
    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
    public void Undo()
    {
        foreach (var command in _commands)
        {
            command.Undo();
        }
    }
}


public class RemoteControl
{
    private ICommand _onCommand;
    private ICommand _offCommand;

    public void SetCommands(ICommand onCommand, ICommand offCommand)
    {
        _onCommand = onCommand;
        _offCommand = offCommand;
    }

    public void PressOnButton()
    {
        _onCommand.Execute();
    }

    public void PressOffButton()
    {
        _offCommand.Execute();
    }

    public void PressUndoButton()
    {
        _onCommand.Undo();
    }
}
