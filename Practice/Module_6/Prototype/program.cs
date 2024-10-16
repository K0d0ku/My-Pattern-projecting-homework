using System;
using System.Collections.Generic;
public class Program
{
    public static void Main(string[] args)
    {
        Character fighter = new Character
        {
            Name = "\nKnight",
            Health = 150,
            Strength = 80,
            Agility = 70,
            Intellegence = 30,
            Weapon = new Weapon { Name = "greatsword", Damage = 25 },
            Armor = new Armor { Name = "steel armor", Durability = 100 },
            Magic = new List<Skill> 
            {
                new Skill { Type = "no magical skills" }
            },
            Ability = new List<Skill>
            {
                new Skill { Type = "ability", Power = "heavy Slash" },
                new Skill { Type = "ability", Power = "shield Bash" }
            }
        };
        Character mage = new Character
        {
            Name = "\nmage",
            Health = 80,
            Strength = 30,
            Agility = 40,
            Intellegence = 100,
            Weapon = new Weapon { Name = "magic staff", Damage = 10 },
            Armor = new Armor { Name = "magical robe", Durability = 0 },
            Magic = new List<Skill>
            {
                new Skill { Type = "magic", Power = "fireball" },
                new Skill { Type = "magic", Power = "feal" }
            },
            Ability = new List<Skill>
            {
                new Skill { Type = "no physical skills" }
            }
        };
        DisplayCharacterInfo(fighter);
        DisplayCharacterInfo(mage);
        Character clonedFighter = (Character)fighter.Clone();
        Console.WriteLine($"\ncloned the {clonedFighter.Name}:");
        DisplayCharacterInfo(clonedFighter);
    }
    public static void DisplayCharacterInfo(Character character)
    {
        Console.WriteLine($"character name: {character.Name}");
        Console.WriteLine($"health: {character.Health}");
        Console.WriteLine($"strength: {character.Strength}");
        Console.WriteLine($"agility: {character.Agility}");
        Console.WriteLine($"intelligence: {character.Intellegence}");
        Console.WriteLine($"weapon: {character.Weapon.Name} (damage: {character.Weapon.Damage})");
        Console.WriteLine($"armor: {character.Armor.Name} (durability: {character.Armor.Durability})");
        Console.WriteLine($"magic skills: {string.Join(", ", character.Magic.ConvertAll(skill => skill.Power))}");
        Console.WriteLine($"ability skills: {string.Join(", ", character.Ability.ConvertAll(skill => skill.Power))}");
    }
}
public class Character : ICloneable
{
    public string Name { get; set; } /*added this for realization*/
    public int Health { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intellegence { get; set; }
    public Weapon Weapon { get; set; }
    public Armor Armor { get; set; }
    public List<Skill> Magic { get; set; }
    public List<Skill> Ability { get; set; }
    public object Clone()
    {
        return new Character
        {
            Name = this.Name,
            Health = this.Health,
            Strength = this.Strength,
            Agility = this.Agility,
            Intellegence = this.Intellegence,
            Weapon = (Weapon)this.Weapon.Clone(),
            Armor = (Armor)this.Armor.Clone(),
            Magic = this.Magic.ConvertAll(skill => (Skill)skill.Clone()),
            Ability = this.Ability.ConvertAll(skill => (Skill)skill.Clone())
        };
    }
}
public class Weapon : ICloneable
{
    public string Name { get; set; } /*added this for realization*/
    public int Damage { get; set; }
    public object Clone()
    {
        return new Weapon { Name = this.Name, Damage = this.Damage };
    }
}
public class Armor : ICloneable
{
    public string Name { get; set; } /*added this for realization*/
    public int Durability { get; set; }
    public object Clone()
    {
        return new Armor { Name = this.Name, Durability = this.Durability };
    }
}
public class Skill : ICloneable
{
    public string Type { get; set; }
    public string Power { get; set; }
    public object Clone()
    {
        return new Skill { Type = this.Type, Power = this.Power };
    }
}
