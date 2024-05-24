namespace Fire_Emblem;

public class ExtraDamageInFollowUp : Effect
{
    public ExtraDamageInFollowUp(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}