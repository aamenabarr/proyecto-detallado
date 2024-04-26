namespace Fire_Emblem;

public class Effect
{
    public string Stat;
    public int Value;
    public bool IsActive = false;
    public bool InFirstAttack = false;
    public bool InFollowUp = false;
    public Unit Unit;
    public List<string> Messages = new List<string>();
    
    protected Effect(Unit unit, string stat, int value)
    {
        Unit = unit;
        Stat = stat;
        Value = value;
    }
    
    protected Effect(Unit unit, string stat) : this(unit, stat, 0) {}
    
    public void AddToManager()
    {
        Unit.EffectManager.Effects.Add(this);
    }

    public virtual void Apply()
    {
        IsActive = true;
    }
    
    public virtual void Reset() {}

    protected void AlterStat()
    {
        switch (Stat)
        {
            case "Hp":
                Unit.Hp += Value;
                break;
            case "Atk":
                Unit.Atk += Value;
                break;
            case "Spd":
                Unit.Spd += Value;
                break;
            case "Def":
                Unit.Def += Value;
                break;
            case "Res":
                Unit.Res += Value;
                break;
        }
    }
    
    protected void Neutralize(IEnumerable<Effect> effects)
    {
        foreach (var effect in effects)
        {
            if (Stat == null | effect.Stat == Stat)
                effect.Reset();
        }
    }

    protected void ResetStat()
    {
        if (IsActive && !InFollowUp)
        {
            IsActive = false;
            Value *= -1;
            AlterStat();
            Value *= -1;
        }
        else if (InFollowUp) IsActive = false;
    }

    public void ResetFollowUpEffect()
    {
        IsActive = false;
        Value *= -1;
        AlterStat();
        Value *= -1;
    }

    protected List<T> GetEffects<T>() where T : Effect
    {
        return Unit.Skills
            .Concat(Unit.Rival.Skills)
            .SelectMany(skill => skill.Effects)
            .OfType<T>()
            .Where(effect => effect.Unit == Unit).ToList();
    }
    
    public void AddAlterStatMessage()
    {
        var sign = (Value > 0) ? "+" : "";
        if (Value != 0 && IsActive &&  Stat != "Hp")
        {
            var condition = "";
            if (InFirstAttack) condition = " en su primer ataque";
            if (InFollowUp) condition = " en su Follow-Up";
            Messages.Add($"{Unit.Name} obtiene {Stat}{sign}{Value}{condition}");
        }
    }

    protected void AddNeutralizeMessages(string type)
    {
        if (Stat == null)
        {
            foreach (var stat in new string[] { "Atk", "Spd", "Def", "Res" })
                Messages.Add($"Los {type} de {stat} de {Unit.Name} fueron neutralizados");
        }
        else
            Messages.Add($"Los {type} de {Stat} de {Unit.Name} fueron neutralizados");
    }
}