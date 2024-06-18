namespace Fire_Emblem;

public static class Stats
{
    private static readonly string[] _stats = 
    {
        "Hp",
        "Atk", 
        "Spd", 
        "Def", 
        "Res"
    };

    public static string Hp => _stats[0];
    public static string Atk => _stats[1];
    public static string Spd => _stats[2];
    public static string Def => _stats[3];
    public static string Res => _stats[4];

    public static IEnumerable<string> AllStats => _stats;
}