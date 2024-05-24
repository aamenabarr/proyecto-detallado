namespace Fire_Emblem;

public class PercentageDamageReductionInFollowUp : Effect
{
    public PercentageDamageReductionInFollowUp(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}