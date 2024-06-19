namespace Fire_Emblem;

public class LastRival : Condition
{
    public LastRival(Unit unit) : base(unit){}
    
    public override bool IsMet()
    {
        return Unit.LastRival == Unit.Rival;
    }
}