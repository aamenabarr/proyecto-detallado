using System.Text.Json;

namespace Fire_Emblem;

public static class Utils
{
    public static List<T>? LoadFromJsonFile<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json);
    }
    
    public static string[] GetFiles(string folder)
    {
        return Directory.GetFiles(folder);
    }
    
    public static string[] ReadFile(string path)
    {
        return File.ReadAllLines(path);
    }
    
    public static int Int(string value)
    {
        return Convert.ToInt32(value);
    }
    
    public static int GetUnitStat(Unit unit, string stat)
    {
        switch (stat)
        {
            case "Atk": return unit.Atk + unit.StatsManager.Get(Stats.Atk);
            case "Spd": return unit.Spd + unit.StatsManager.Get(Stats.Spd);
            case "Def": return unit.Def + unit.StatsManager.Get(Stats.Def);
            case "Res": return unit.Res + unit.StatsManager.Get(Stats.Res);
            case "Dmg": return unit.DamageManager.GetDamageReduction();
        }
        return 0;
    }
    
    public static int GetDamage(Unit unit)
    {
        return unit.DamageManager.GetDamage();
    }
}