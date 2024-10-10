using System;
class Program
{
    static void Main(string[] args)
    {
        // Создаем офисный компьютер
        Console.WriteLine("Office Computer:");
        IComputerBuilder officeBuilder = new OfficeComputerBuilder();
        ComputerDirector director = new ComputerDirector(officeBuilder);
        director.ConstructComputer();
        Computer officeComputer = director.GetComputer();
        Console.WriteLine(officeComputer);

        // Создаем игровой компьютер
        Console.WriteLine("Gaming Computer:");
        IComputerBuilder gamingBuilder = new GamingComputerBuilder();
        director = new ComputerDirector(gamingBuilder);
        director.ConstructComputer();
        Computer gamingComputer = director.GetComputer();
        Console.WriteLine(gamingComputer);

        /*creating a server pc*/
        Console.WriteLine("Server Computer");
        IComputerBuilder servercomputerbuilder = new ServerComputerBuilder();
        director = new ComputerDirector(servercomputerbuilder);
        director.ConstructComputer();
        Computer servercomputer = director.GetComputer();
        Console.WriteLine(servercomputer);

        /*creating faulty graphics pc for compatability check*/
        Console.WriteLine("Faulty Graphics Computer");
        IComputerBuilder faultyartistcomputerbuilder = new FaultyArtistComputerBuilder();
        director = new ComputerDirector(faultyartistcomputerbuilder);
        director.ConstructComputer();
        Computer graphicscomputer = director.GetComputer();
        Console.WriteLine(graphicscomputer);
    }
}
public class Computer
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string GPU { get; set; }
    public string OS { get; set; }
    public string Cooler { get; set; }
    public string PSU { get; set; }

    public override string ToString()
    {
        return $"computer: CPU - {CPU}, \nRAM - {RAM}, \nstorage - {Storage}, \nGPU - {GPU}, \nOS - {OS}, \ncooler type - {Cooler}, \npower supply unit - {PSU}\n";
    }/*i changed the text to eng cuz the russian and kz letters would appear as ? in console, when the code is executed*/
    public bool CompatabilityCheck()
    {
        if (CPU == "Amd Theadripper Pro 5995WX" && Cooler == "Air cooler")
        {
            Console.WriteLine("the cpu - Amd Theadripper Pro 5995WX requieres liquid cooling");
            return false;
        }
        if (GPU == "Amd Radeon RX 7900 XTX" && PSU == "400W")
        {
            Console.WriteLine("the gpu - Amd Radeon RX 7900 XTX requieres 2000W power supply unit");
            return false;
        }
        return true;
    }
}
public interface IComputerBuilder
{
    void SetCPU();
    void SetRAM();
    void SetStorage();
    void SetGPU();
    void SetOS();
    void SetCooler();
    void SetPSU();
    Computer GetComputer();
}
public class OfficeComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();
    public void SetCPU() => _computer.CPU = "Intel i3";
    public void SetRAM() => _computer.RAM = "8GB";
    public void SetStorage() => _computer.Storage = "1TB HDD";
    public void SetGPU() => _computer.GPU = "Integrated";
    public void SetOS() => _computer.OS = "Windows 10";
    public void SetCooler() => _computer.Cooler = "Air cooler";
    public void SetPSU() => _computer.PSU = "500W";
    public Computer GetComputer() => _computer;
}
public class GamingComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();
    public void SetCPU() => _computer.CPU = "Intel i9";
    public void SetRAM() => _computer.RAM = "32GB";
    public void SetStorage() => _computer.Storage = "1TB SSD";
    public void SetGPU() => _computer.GPU = "NVIDIA RTX 3080";
    public void SetOS() => _computer.OS = "Windows 11";
    public void SetCooler() => _computer.Cooler = "Liquid cooler";
    public void SetPSU() => _computer.PSU = "1500W";
    public Computer GetComputer() => _computer;
}
public class ServerComputerBuilder : IComputerBuilder
{   /*server pc*/
    private Computer _computer = new Computer();
    public void SetCPU() => _computer.CPU = "NVIDIA Grace Cpu Superchip";
    public void SetRAM() => _computer.RAM = "2TB";
    public void SetStorage() => _computer.Storage = "4PB";
    public void SetGPU() => _computer.GPU = "NVIDIA RTX A 1000";
    public void SetOS() => _computer.OS = "Linux Arch";
    public void SetCooler() => _computer.Cooler = "Liquid cooler";
    public void SetPSU() => _computer.PSU = "4000W";
    public Computer GetComputer() => _computer;
}
public class FaultyArtistComputerBuilder : IComputerBuilder
{   /*pc to work with graphics*/
    private Computer _computer = new Computer();
    public void SetCPU() => _computer.CPU = "Amd Theadripper Pro 5995WX";
    public void SetRAM() => _computer.RAM = "128GB";
    public void SetStorage() => _computer.Storage = "4TB";
    public void SetGPU() => _computer.GPU = "Amd Radeon RX 7900 XTX";
    public void SetOS() => _computer.OS = "Windows 10 pro";
    public void SetCooler() => _computer.Cooler = "Air cooler";
    public void SetPSU() => _computer.PSU = "400W";
    public Computer GetComputer() => _computer;
}
public class ComputerDirector
{
    private IComputerBuilder _builder;

    public ComputerDirector(IComputerBuilder builder)
    {
        _builder = builder;
    }

    public void ConstructComputer()
    {
        _builder.SetCPU();
        _builder.SetRAM();
        _builder.SetStorage();
        _builder.SetGPU();
        _builder.SetOS();
        _builder.SetCooler();
        _builder.SetPSU();
        Computer computer = _builder.GetComputer();
        if (!computer.CompatabilityCheck())
        {
            Console.WriteLine("the computer has incompatable parts");
        }
    }

    public Computer GetComputer()
    {
        return _builder.GetComputer();
    }
}
