namespace Fire_Emblem;

public class PenaltyInFirstAttack : Effect
{
    public PenaltyInFirstAttack(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
    {
        AlterStat();
    }
}