namespace Fire_Emblem;

public class ExtraDamageInFollowUp : Effect
{
    private int _percentage = 0;

    public ExtraDamageInFollowUp(Unit unit, string stat, int percentage = 0) : base(unit, stat)
    {
        _percentage = percentage;
    }
    
    public override void Apply()
    {
        AlterDamage(GetExtraDamage());
    }

    private int GetExtraDamage()
    {
        if (_percentage != 0) return Utils.GetUnitStat(Unit, Stat);
        return Utils.GetUnitStat(Unit, Stat) * _percentage / 100;
    }
}