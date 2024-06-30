namespace Fire_Emblem;

public class BonusInFollowUp : Effect
{
    public BonusInFollowUp(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
        => AlterStat();
}