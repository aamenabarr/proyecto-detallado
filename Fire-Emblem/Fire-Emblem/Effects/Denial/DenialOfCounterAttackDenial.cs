namespace Fire_Emblem;

public class DenialOfCounterAttackDenial : Effect
{
    public DenialOfCounterAttackDenial(Unit unit) : base(unit) {}

    public override void Apply()
    {
        if (Unit.CounterAttackDenial)
        {
            Unit.CounterAttackDenial = false;
            Unit.DenialOfCounterAttackDenial = true;
        }
    }
}