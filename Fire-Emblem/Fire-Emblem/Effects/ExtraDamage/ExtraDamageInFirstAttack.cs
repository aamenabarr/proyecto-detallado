namespace Fire_Emblem;

public class ExtraDamageInFirstAttack : Effect
{
    public ExtraDamageInFirstAttack(Unit unit, int value) : base(unit, value) {}
    
    public override void Apply()
    {
        AlterDamage();
    }
}