namespace Fire_Emblem;

public class BonusInFirstAttack : Effect
{
    public BonusInFirstAttack(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
    {
        AlterStat();
    }
}