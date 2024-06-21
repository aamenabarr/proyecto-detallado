namespace Fire_Emblem;

public class ConditionalEffect : Effect
{
    private Condition _condition;
    private List<Effect> _effects;
    private bool _mastermind;
    
    public ConditionalEffect(Condition condition, List<Effect> effects, bool mastermind = false) : base(effects[0].Unit)
    {
        _condition = condition;
        _effects = effects;
        _mastermind = mastermind;
    }

    public override void Apply()
    {
        if (_condition.IsMet())
            foreach (var effect in _effects)
                effect.Apply();
    }

    public override string GetTypeName()
    {
        if (_mastermind) return _effects[2].GetType().Name;
        return _effects[0].GetType().Name;
    }
}