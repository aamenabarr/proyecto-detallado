namespace Fire_Emblem;

public class UseWeapon : Condition
{
    private Unit _unit;
    private string _weapon;

    public UseWeapon(Unit unit, string weapon)
    {
        _unit = unit;
        _weapon = weapon;
    }
    
    public bool IsMet()
    {
        return _unit.Weapon == _weapon;
    }
}