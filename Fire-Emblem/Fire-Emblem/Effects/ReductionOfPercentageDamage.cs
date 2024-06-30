namespace Fire_Emblem;

public class ReductionOfPercentageDamage : Effect
{
    public ReductionOfPercentageDamage(Unit unit) : base(unit) {}
    
    public override void Apply()
    {
        Unit.ReductionOfPercentageDamage *= 2;
    }
}