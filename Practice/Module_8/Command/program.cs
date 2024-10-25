/*output:
exec rec macro commands:
lights are on.
tv is on.
curtains are open.*/

using System;
using System.Runtime.InteropServices;

public class porgram
{
    public static void Main(string[] args)
    {
        Light Light1 = new Light();
        Television tv = new Television();
        Conditioner conditioner = new Conditioner();
        Curtains curtains = new Curtains();

        ICommand lightOn = new LightOnCommand(Light1);
        ICommand tvOn = new TelevisionOnCommand(tv);
        ICommand conditionerOn = new ConditionerOnCommand(conditioner);
        ICommand curtainsOpen = new CurtainsOnCommand(curtains);

        ICommand lightOff = new LightOffCommand(Light1);
        ICommand tvOff = new TelevisionOffCommand(tv);
        ICommand conditionerOff = new ConditionerOffCommand(conditioner);
        ICommand curtainsClose = new CurtainsOffCommand(curtains);

        ICommand allDevicesOn = new MacroCommand(new ICommand[] { lightOn, tvOn, conditionerOn, curtainsOpen });
        ICommand allDevicesOff = new MacroCommand(new ICommand[] { lightOff, tvOff, conditionerOff, curtainsClose });

        RemoteControl remote = new RemoteControl();

        remote.RecordCommand(lightOn);
        remote.RecordCommand(tvOn);
        remote.RecordCommand(curtainsOpen);

        Console.WriteLine("exec rec macro commands:\n");/*BTW it only outputs recorded commands*/
        remote.ExecuteRecordedCommands();
    }
}
public interface ICommand
{
    void Execute();
    void Undo();
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
        Console.WriteLine("lights are on.");
    }
    public void Off()
    {
        Console.WriteLine("lights are off.");
    }
}
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
        Console.WriteLine("tv is on.");
    }
    public void Off()
    {
        Console.WriteLine("tv is off.");
    }
}
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
}
public class CurtainsOnCommand : ICommand
{
    private Curtains _curtains;
    public CurtainsOnCommand(Curtains curtains)
    {
        _curtains = curtains;
    }
    public void Execute()
    {
        _curtains.Open();
    }
    public void Undo()
    {
        _curtains.Close();
    }
}
public class CurtainsOffCommand : ICommand
{
    private Curtains _curtains;
    public CurtainsOffCommand(Curtains curtains)
    {
        _curtains = curtains;
    }
    public void Execute()
    {
        _curtains.Close();
    }
    public void Undo()
    {
        _curtains.Open();
    }
}
public class Curtains
{
    public void Open()
    {
        Console.WriteLine("curtains are open.");
    }
    public void Close()
    {
        Console.WriteLine("curtains are closed.");
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
    private List<ICommand> _recordedCommands = new List<ICommand>();
    public void RecordCommand(ICommand command)
    {
        _recordedCommands.Add(command);
    }
    public void ExecuteRecordedCommands()
    {
        foreach (var command in _recordedCommands)
        {
            command.Execute();
        }
    }
}
