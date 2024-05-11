namespace Fire_Emblem;

public class PenaltyInFollowUp : Effect
{
    public PenaltyInFollowUp(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
    {
        AlterStat();
    }
}