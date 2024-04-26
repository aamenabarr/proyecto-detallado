namespace Fire_Emblem;

public class InFirstRound : Condition
{
    public InFirstRound(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return _unit.InFirstRound;
    }
}