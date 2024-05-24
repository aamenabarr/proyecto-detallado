namespace Fire_Emblem;

public class HasWeaponAdvantage : Condition
{
    public HasWeaponAdvantage(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return _unit.HasWeaponAdvantage;
    }
}