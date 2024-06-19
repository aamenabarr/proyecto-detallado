namespace Fire_Emblem;

public class CounterAttackDenial : Effect
{
    public CounterAttackDenial(Unit unit) : base(unit) {}
    
    public override void Apply()
    {
        Unit.CounterAttackDenial = true;
    }
}