namespace Fire_Emblem;

public class ExtraDamage : Effect
{
    public ExtraDamage(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}