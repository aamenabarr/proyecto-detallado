namespace Fire_Emblem;

public class HealingAfterCombat : Effect
{
    public bool HasAttackedCondition = false;
    public HealingAfterCombat(Unit unit, int value, string mode = "") : base(unit, Stats.Hp, value) {}

    public HealingAfterCombat(Unit unit, int value, bool hasAttackedCondition) : base(unit, Stats.Hp, value)
    {
        HasAttackedCondition = hasAttackedCondition;
    }
    
    public override void Apply()
    {
        if (HasAttackedCondition)
            NeutralizeEffect(GetType().Name);
        AlterStat();
    }
}