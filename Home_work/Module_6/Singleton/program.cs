/*i hardly understand how i even coded this*/

using System;
using System.Collections.Generic;
public class program
{
    public static void Main(string[] args)
    {
        ConfigurationManager configManager = ConfigurationManager.GetInstance();
        string filePath = @"C:\Users\Bekal\Documents\my_own_documents\vs studio lol\uni\pattern\lecture\6\settings.txt";
        configManager.LoadSettingsFromFile(filePath);
        Console.WriteLine("hasAccount: " + configManager.GetSetting("hasAccount"));
        Console.WriteLine("noAccount: " + configManager.GetSetting("noAccount"));
        Console.WriteLine("logOut: " + configManager.GetSetting("logOut"));

        configManager.ChangeSetting("hasAccount", "Greetings");
        Console.WriteLine("hasAccount (after change): " + configManager.GetSetting("hasAccount"));
    }
}
public class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, string> _settings;
    private ConfigurationManager()
    {
        _settings = new Dictionary<string, string>();
    }
    public static ConfigurationManager GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
            }
        }
        return _instance;
    }
    public void LoadSettings()
    {
        _settings["hasAccount"] = "Welcome";
        _settings["noAccount"] = "Reister";
    }
    public string GetSetting(string key)
    {
        if (_settings.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            throw new KeyNotFoundException($"setting are '{key}' not found.");
        }
    }
    public void ChangeSetting(string key, string value)
    {
        _settings[key] = value;
    }
    public void LoadSettingsFromFile(string filePath)
    {
        try
        {
            _settings.Clear();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    _settings[key] = value;
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"error reading from file: {ex.Message}");
        }
    }
}
