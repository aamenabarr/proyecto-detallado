namespace Fire_Emblem;

public class Bonus : Effect
{
    public Bonus(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public override void Apply()
    {
        base.Apply();
        AlterStat();
    }
    
    public override void Reset()
    {
        ResetStat();
    }
}