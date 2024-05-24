namespace Fire_Emblem;

public class PercentageDamageReductionInFirstAttack : Effect
{
    public PercentageDamageReductionInFirstAttack(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}