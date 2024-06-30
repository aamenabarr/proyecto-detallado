namespace Fire_Emblem;

public class UseWeapon : Condition
{
    private string _weapon;

    public UseWeapon(Unit unit, string weapon) : base(unit)
        => _weapon = weapon;
    
    public override bool IsMet()
    {
        return Unit.Weapon == _weapon;
    }
}