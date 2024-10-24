/*output:
light is on
door is open
temperature is up
the alarm is ringing
light is off
door is closed
temperature is down
the alarm is off

this command have not been executed yet.

no commands to undo.*/

using System;
public class program
{
    public static void Main(string[] args)
    {
        Light light1 = new Light();
        ICommand turnOnLight = new TurnOnLightCommand(light1);

        Door door1 = new Door();
        ICommand openDoor = new OpenDoorCommand(door1);

        Thermostat homeThermostat = new Thermostat();
        ICommand tempUp = new IncreaseTemperatureCommand(homeThermostat);

        AlarmActivation homeAlarm = new AlarmActivation();
        ICommand avtivateAlarm = new AlarmActivationCommand(homeAlarm);

        Invoker remoteControl = new Invoker(5);

        remoteControl.SetCommand(turnOnLight);
        remoteControl.ExecuteCommand();

        remoteControl.SetCommand(openDoor);
        remoteControl.ExecuteCommand();

        remoteControl.SetCommand(tempUp);
        remoteControl.ExecuteCommand();

        remoteControl.SetCommand(avtivateAlarm);
        remoteControl.ExecuteCommand();
        
        remoteControl.SetCommand(avtivateAlarm);
        remoteControl.ExecuteCommand();/*to test if command is not executed*/

        remoteControl.UndoCommand();
        remoteControl.UndoCommand();
        remoteControl.UndoCommand();
        remoteControl.UndoCommand();
        
        remoteControl.UndoCommand();
        remoteControl.UndoCommand();/*to test if there is no command to undo*/
    }
}
public interface ICommand
{
    void Execute();
    void Undo();
}

public class Light
{
    public void On()
    {
        Console.WriteLine("light is on");
    }
    public void Off()
    {
        Console.WriteLine("light is off");
    }
}
public class TurnOnLightCommand : ICommand
{
    private Light _light;
    private bool _isExecuted = false;
    public TurnOnLightCommand(Light light)
    {
        _light = light;
    }
    public void Execute()
    {
        if (!_isExecuted)
        {
            _light.On();
            _isExecuted = true;
        }
    }
    public void Undo()
    {
        if (_isExecuted)
        {
            _light.Off();
            _isExecuted = false;
        }
        else
        {
            Console.WriteLine("\nthis command have not been executed yet.");
        }
    }
}

public class Door
{
    public void Open()
    {
        Console.WriteLine("door is open");
    }
    public void Close()
    {
        Console.WriteLine("door is closed");
    }
}
public class OpenDoorCommand : ICommand
{
    private Door _door;
    private bool _isExecuted = false;
    public OpenDoorCommand(Door door)
    {
        _door = door;
    }
    public void Execute()
    {
        if (!_isExecuted)
        {
            _door.Open();
            _isExecuted = true;
        }
    }
    public void Undo()
    {
        if (_isExecuted)
        {
            _door.Close();
            _isExecuted = false;
        }
        else
        {
            Console.WriteLine("\nthis command have not been executed yet.");
        }
    }
}

public class Thermostat
{
    public void TempUp()
    {
        Console.WriteLine("temperature is up");
    }
    public void TempDown()
    {
        Console.WriteLine("temperature is down");
    }
}
public class IncreaseTemperatureCommand : ICommand
{
    private Thermostat _thermostat;
    private bool _isExecuted = false;
    public IncreaseTemperatureCommand(Thermostat thermostat)
    {
        _thermostat = thermostat;
    }
    public void Execute()
    {
        if (!_isExecuted)
        {
            _thermostat.TempUp();
            _isExecuted = true;
        }
    }
    public void Undo()
    {
        if (_isExecuted)
        {
            _thermostat.TempDown();
            _isExecuted = false;
        }
        else
        {
            Console.WriteLine("\nthis command have not been executed yet.");
        }
    }
}

public class AlarmActivation
{
    public void AlarmOn()
    {
        Console.WriteLine("the alarm is ringing");
    }
    public void AlarmOff()
    {
        Console.WriteLine("the alarm is off");
    }
}
public class AlarmActivationCommand : ICommand
{
    private AlarmActivation _alarmActivation;
    private bool _isExecuted = false;
    public AlarmActivationCommand(AlarmActivation alarmActivation)
    {
        _alarmActivation = alarmActivation;
    }
    public void Execute()
    {
        if (!_isExecuted)
        {
            _alarmActivation.AlarmOn();
            _isExecuted = true;
        }
    }
    public void Undo()
    {
        if (_isExecuted)
        {
            _alarmActivation.AlarmOff();
            _isExecuted = false;
        }
        else
        {
            Console.WriteLine("\nthis command have not been executed yet.");
        }
    }
}

public class Invoker
{
    private ICommand _currentCommand;
    private Queue<ICommand> _commandHistory;
    private int _historySize;
    /*i coulnt figure out a way to undo the latest commands so i 
     * just make it remove the oldest by history command to be removed*/
    public Invoker(int historySize)
    {
        _commandHistory = new Queue<ICommand>();
        _historySize = historySize;
    }
    public void SetCommand(ICommand command)
    {
        _currentCommand = command;
    }
    public void ExecuteCommand()
    {
        if (_currentCommand != null)
        {
            _currentCommand.Execute();
            _commandHistory.Enqueue(_currentCommand);
            if (_commandHistory.Count > _historySize)
            {
                _commandHistory.Dequeue();
            }
        }
    }
    public void UndoCommand()
    {
        if (_commandHistory.Count > 0)
        {
            ICommand lastCommand = _commandHistory.Dequeue();
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("\nno commands to undo.");
        }
    }
}
