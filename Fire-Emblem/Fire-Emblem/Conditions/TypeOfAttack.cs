namespace Fire_Emblem;

public class TypeOfAttack : Condition
{
    private Unit _unit;
    private string _attack;

    public TypeOfAttack(Unit unit, string attack)
    {
        _unit = unit;
        _attack = attack;
    }
    
    public bool IsMet()
    {
        return _attack == "Physical" ? _unit.Weapon != "Magic" : _unit.Weapon == "Magic";
    }
}