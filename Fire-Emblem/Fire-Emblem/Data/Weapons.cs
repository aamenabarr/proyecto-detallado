namespace Fire_Emblem;

public static class Weapons
{
    private static readonly string[] _weapons = 
    {
        "Axe",
        "Lance", 
        "Bow", 
        "Sword", 
        "Magic",
        "Physical"
    };

    public static string Axe => _weapons[0];
    public static string Lance => _weapons[1];
    public static string Bow => _weapons[2];
    public static string Sword => _weapons[3];
    public static string Magic => _weapons[4];
    public static string Physical => _weapons[5];
}