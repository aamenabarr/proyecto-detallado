namespace Fire_Emblem;

public class AbsolutDamageReduction : Effect
{
    public AbsolutDamageReduction(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}