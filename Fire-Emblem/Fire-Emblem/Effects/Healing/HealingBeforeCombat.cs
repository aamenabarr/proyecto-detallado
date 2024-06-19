namespace Fire_Emblem;

public class HealingBeforeCombat : Effect
{
    public HealingBeforeCombat(Unit unit, int value, string mode = "") : base(unit, Stats.Hp, value) {}
    
    public override void Apply()
    {
        AlterStat();
    }
}