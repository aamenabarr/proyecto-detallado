using System.Text.Json;

namespace Fire_Emblem;

public class Utils
{
    public List<T> LoadFromJsonFile<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json);
    }
    
    public string[] GetFiles(string folder)
    {
        return Directory.GetFiles(folder);
    }
    
    public string[] ReadFile(string path)
    {
        return File.ReadAllLines(path);
    }
    
    public int Int(string value)
    {
        return Convert.ToInt32(value);
    }
}