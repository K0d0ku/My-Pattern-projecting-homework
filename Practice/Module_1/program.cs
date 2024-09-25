using System;
using System.Collections.Generic;
/*я еле как понял как я это написал*/
public class Dealer
{
    public static void Main()
    {
        Car car1 = new Car { brand = "Aston Martin", model = "Valkyrie", door_amount = 2, transmission = "Automatic", year = new DateTime(2016, 11, 01) };
        Motorcycle bike1 = new Motorcycle { brand = "Ducati", model = "Panigale V4", body_type = "Sport", box_availability = false, year = new DateTime(2018, 09, 11) };

        Garage garage1 = new Garage();
        garage1.Add_vechicle(car1);
        garage1.Add_vechicle(bike1);

        Fleet fleet = new Fleet();
        fleet.Add_garage(garage1);

        fleet.Search("Aston Martin");
        fleet.Search("Ducati");
    }
}

public abstract class Vechicle
{
    public string brand { get; set; }
    public string model { get; set; }
    public DateTime year { get; set; }
}

public class Car : Vechicle
{
    public int door_amount { get; set; }
    public string transmission { get; set; }
}
public class Motorcycle : Vechicle
{
    public string body_type { get; set; }
    public bool box_availability { get; set; }
}

public class Garage
{
    public List<Vechicle> vechicles { get; set; }

    public Garage()
    {
        vechicles = new List<Vechicle>();
    }
    public void Add_vechicle(Vechicle vechicle)
    {
        vechicles.Add(vechicle);
        Console.WriteLine($"'{vechicle.brand}' have been added to garage");
    }
    public void Remove_vechicle(Vechicle vechicle)
    {
        if (vechicles.Contains(vechicle))
        {
            vechicles.Remove(vechicle);
            Console.WriteLine($"'{vechicle.brand}' has been removed from garage");
        }

        else
        {
            Console.WriteLine($"'{vechicle.brand}' have not been not found");
        }
    }
}

public class Fleet
{
    public List<Garage> garages { get; set; }

    public Fleet()
    {
        garages = new List<Garage>();
    }

    public void Add_garage(Garage garage)
    {
        garages.Add(garage);
        Console.WriteLine($"garage '{garage}' have been added");
    }

    public void Remove_garage(Garage garage)
    {
        if (garages.Contains(garage))
        {
            garages.Remove(garage);
            Console.WriteLine($"garage '{garage}' has been removed");
        }
        else
        {
            Console.WriteLine($"garage '{garage}' have not been found");
        }
    }

    public void Search(string brand)
    {
        foreach (var garage in garages)
        {
            foreach (var vechicle in garage.vechicles)
            {
                if (vechicle.brand == brand)
                {
                    Console.WriteLine($"'{brand}' have been found");
                    return;
                }
            }
        }
        Console.WriteLine($"'{brand}' has not been found");
    }
}
