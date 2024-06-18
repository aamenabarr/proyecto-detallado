using Fire_Emblem_GUI;

namespace Fire_Emblem;

/*
 * Esta es una clase auxiliar. Ocurre que los métodos de la vista GUI reciben objetos
 * que implementen la interfaz IUnit. Por eso, para crear el ejemplo, era necesario
 * crear un objeto que implemente esa interfaz.
 */
class MyUnit(string name, string weapon, int hp, int atk, int spd, int def, int res) : IUnit
{
    public string Name { get; } = name;
    public string Weapon { get; } = weapon;
    public int Hp { get; set; } = hp;
    public int Atk { get; } = atk;
    public int Spd { get; } = spd;
    public int Def { get; } = def;
    public int Res { get; } = res;
    public string[] Skills { get; } = [];
}