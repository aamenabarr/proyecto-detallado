namespace Fire_Emblem;

public class Effect
{
    protected string Stat;
    protected int Value;
    public Unit Unit;
    
    protected Effect(Unit unit, string stat, int value)
    {
        Unit = unit;
        Stat = stat;
        Value = value;
    }
    
    protected Effect(Unit unit, string stat) : this(unit, stat, 0) {}

    protected Effect(Unit unit, int value) : this(unit, "", value) {}
    
    protected Effect(Unit unit) : this(unit, "", 0) {}
    
    public virtual void Apply() {}

    protected void AlterStat()
    {
        Unit.StatsManager.AlterStatsDictionary(GetType().Name, Stat, Value);
    }

    protected void NeutralizeEffect(string type)
    {
        Unit.StatsManager.NeutralizeEffect(type, Stat);
    }

    protected void AlterDamage(int value)
    {
        Unit.DamageManager.AlterDamageDictionary(GetType().Name, value);
    }

    public virtual string GetTypeName()
    {
        return GetType().Name;
    }
}