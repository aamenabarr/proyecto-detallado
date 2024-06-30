namespace Fire_Emblem;

public class ExtraDamageInFollowUp : Effect
{
    private int _percentage = 0;
    private bool _brash;

    public ExtraDamageInFollowUp(Unit unit, string stat, int percentage = 0, bool brash = false) : base(unit, stat)
    {
        _percentage = percentage;
        _brash = brash;
    }
    
    public override void Apply()
    {
        AlterDamage(GetExtraDamage());
    }

    private int GetExtraDamage()
    {
        if (_percentage == 0) return Utils.GetUnitStat(Unit, Stat);
        if (_brash) return Utils.GetDamage(Unit.Rival) * _percentage / 100;
        return Utils.GetUnitStat(Unit, Stat) * _percentage / 100;
    }
}