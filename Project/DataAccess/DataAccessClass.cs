using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class DataAccessClass
{
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };

    /// <summary>
    /// Reads a JSON file and deserializes it into a list.
    /// </summary>
    public static List<T> ReadList<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }

    /// <summary>
    /// Serializes and writes a list to a JSON file.
    /// </summary>
    public static void WriteList<T>(string filePath, List<T> data)
    {
        string json = JsonSerializer.Serialize(data, Options);
        File.WriteAllText(filePath, json);
    }
}
