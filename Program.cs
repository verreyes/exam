using System;
using System.IO;
using System.Linq;

class Program
{
    static readonly string[] FeatureNames =
    {
        "SMS Notifications Enabled",
        "Push Notifications Enabled",
        "Bio-metrics Enabled",
        "Camera Enabled",
        "Location Enabled",
        "NFC Enabled",
        "Vouchers Enabled",
        "Loyalty Enabled"
    };

    static void Main()
    {
        var settings = string.Empty;

        while (true)
        {
            try
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Enter new settings");
                Console.WriteLine("2 - Load settings from file");
                Console.WriteLine("3 - Save settings to file");
                Console.WriteLine("4 - Check if a specific feature is enabled");
                Console.WriteLine("5 - Exit");

                var choice = Console.ReadLine()?.Trim();

                if (choice == "5") break; // Exit the loop

                switch (choice)
                {
                    case "1":
                        settings = EnterNewSettings();
                        break;
                    case "2":
                        settings = LoadSettings();
                        if (!string.IsNullOrEmpty(settings))
                        {
                            Console.WriteLine($"Loaded settings: {settings}");
                        }
                        break;

                    case "3":
                        if (string.IsNullOrEmpty(settings))
                        {
                            Console.WriteLine("No settings to save. Please enter or load settings first.");
                        }
                        else
                        {
                            SaveSettings(settings);
                            Console.WriteLine("Settings saved successfully.");
                        }
                        break;

                    case "4":
                        if (string.IsNullOrEmpty(settings))
                        {
                            Console.WriteLine("No settings available. Please enter or load settings first.");
                            continue;
                        }
                        CheckFeatureStatus(settings);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 5.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    private static string EnterNewSettings()
    {
        Console.WriteLine("Enter 8 boolean settings (as a string of 8 characters where each character is '0' or '1'):");
        var settings = Console.ReadLine();

        if (settings == null || !IsBinaryString(settings))
        {
            Console.WriteLine("Invalid settings string. Please enter exactly 8 characters where each character is '0' or '1'.");
            return string.Empty;
        }

        return settings;
    }

    private static void ShowFeatureOptions()
    {
        Console.WriteLine("Available features:");
        for (var i = 0; i < FeatureNames.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {FeatureNames[i]}");
        }
    }

    private static int GetFeatureIndex()
    {
        Console.WriteLine("Enter the feature index (1-8):");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= 8)
        {
            return index;
        }

        Console.WriteLine("Invalid feature index. Please enter a number between 1 and 8.");
        return -1;
    }

    private static void DisplayFeatureStatus(string settings, int index)
    {
        var featureName = FeatureNames[index - 1];
        var isEnabled = settings[index - 1] == '1';
        Console.WriteLine($"Feature '{featureName}' enabled: {isEnabled}");
    }

    private static void CheckFeatureStatus(string settings)
    {
        ShowFeatureOptions();
        var featureIndex = GetFeatureIndex();
        if (featureIndex == -1) return;
        DisplayFeatureStatus(settings, featureIndex);
    }

    private static string LoadSettings()
    {
        const string filePath = "settings.bin";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Settings file not found.");
            return string.Empty;
        }

        try
        {
            using var reader = new BinaryReader(File.OpenRead(filePath));
            var settings = reader.ReadString();

            if (!IsBinaryString(settings))
            {
                Console.WriteLine("Loaded settings are invalid. The file may be corrupted.");
                return string.Empty;
            }

            return settings;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading settings: {ex.Message}");
            return string.Empty;
        }
    }

    private static void SaveSettings(string settings)
    {
        const string filePath = "settings.bin";

        try
        {
            using var writer = new BinaryWriter(File.OpenWrite(filePath));
            writer.Write(settings);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving settings: {ex.Message}");
        }
    }

    private static bool IsBinaryString(string str) => str.Length == 8 && str.All(c => c == '0' || c == '1');
}
