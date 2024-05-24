namespace Fire_Emblem;

public class InFirstCombat : Condition
{
    public InFirstCombat(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return _unit.InFirstCombat;
    }
}