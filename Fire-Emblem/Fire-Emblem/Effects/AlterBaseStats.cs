namespace Fire_Emblem;

public class AlterBaseStats : Effect
{
    public AlterBaseStats(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
    {
        base.Apply();
        AlterStat();
    }
}