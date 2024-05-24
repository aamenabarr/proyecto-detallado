namespace Fire_Emblem;

public class PercentageDamageReduction : Effect
{
    public PercentageDamageReduction(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}