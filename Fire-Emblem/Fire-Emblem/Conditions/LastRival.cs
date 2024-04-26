namespace Fire_Emblem;

public class LastRival : Condition
{
    public LastRival(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return _unit.LastRival == _unit.Rival;
    }
}