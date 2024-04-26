namespace Fire_Emblem;

public class Effect
{
    public Unit Unit;
    private string _stat;
    private int _value;
    public bool IsActive;
    public bool InFirstAttack = false;
    public bool InFollowUp = false;
    private string _condition = "";
    public List<string> Messages = new List<string>();
    
    protected Effect(Unit unit, string stat, int value)
    {
        Unit = unit;
        _stat = stat;
        _value = value;
    }
    
    protected Effect(Unit unit, string stat) : this(unit, stat, 0) { }

    public virtual void Apply()
    {
        IsActive = true;
    }
    
    public virtual void Reset() {}

    private void SetCondition()
    {
        if (InFirstAttack) _condition = " en su primer ataque";
        if (InFollowUp) _condition = " en su Follow-Up";
    }

    protected void AlterStat()
    {
        switch (_stat)
        {
            case "Hp":
                Unit.Hp += _value;
                break;
            case "Atk":
                Unit.Atk += _value;
                break;
            case "Spd":
                Unit.Spd += _value;
                break;
            case "Def":
                Unit.Def += _value;
                break;
            case "Res":
                Unit.Res += _value;
                break;
        }
        if (!InFollowUp) AddAlterStatMessages();
    }

    public void AddAlterStatMessages()
    {
        var sign = (_value > 0) ? "+" : "";
        if (_value != 0 && IsActive && _stat != "Hp")
        {
            SetCondition();
            Messages.Add($"{Unit.Name} obtiene {_stat}{sign}{_value}{_condition}");
        }
    }

    protected void ResetStat()
    {
        if (IsActive && !InFollowUp)
        {
            IsActive = false;
            _value *= -1;
            AlterStat();
            _value *= -1;
        }
        else if (InFollowUp) IsActive = false;
    }

    public void ResetFollowUpEffect()
    {
        IsActive = false;
        _value *= -1;
        AlterStat();
        _value *= -1;
    }

    public void AddEffect()
    {
        Unit.EffectManager.Effects.Add(this);
    }

    protected void Neutralize(string type)
    {
        switch (type)
        {
            case "bonus":
                var bonusEffects = Unit.Skills
                    .Concat(Unit.Rival.Skills)
                    .SelectMany(skill => skill.Effects)
                    .OfType<Bonus>()
                    .Where(effect => effect.Unit == Unit);
                ResetEffects(bonusEffects);
                AddNeutralizeMessages(type);
                break;

            case "penalty":
                var penaltyEffects = Unit.Skills
                    .Concat(Unit.Rival.Skills)
                    .SelectMany(skill => skill.Effects)
                    .OfType<Penalty>()
                    .Where(effect => effect.Unit == Unit);
                ResetEffects(penaltyEffects);
                AddNeutralizeMessages(type);
                break;
        }
    }
    
    private void ResetEffects(IEnumerable<Effect> effects)
    {
        foreach (var effect in effects)
        {
            if (_stat == null | effect._stat == _stat)
                effect.Reset();
        }
    }

    private void AddNeutralizeMessages(string type)
    {
        if (_stat == null)
        {
            foreach (var stat in new string[] { "Atk", "Spd", "Def", "Res" })
                Messages.Add($"Los {type} de {stat} de {Unit.Name} fueron neutralizados");
        }
        else
            Messages.Add($"Los {type} de {_stat} de {Unit.Name} fueron neutralizados");
    }
}