namespace Fire_Emblem;

public class ConditionalElseEffect : Effect
{
    private Condition _condition;
    private List<Effect> _ifEffects;
    private List<Effect> _elseEffects;
    
    public ConditionalElseEffect(Condition condition, List<Effect> ifEffects, List<Effect> elseEffects) : base(ifEffects[0].Unit)
    {
        _condition = condition;
        _ifEffects = ifEffects;
        _elseEffects = elseEffects;
    }
    
    public override void Apply()
    {
        if (_condition.IsMet())
            foreach (var effect in _ifEffects)
                effect.Apply();
        else
            foreach (var effect in _elseEffects)
                effect.Apply();
    }

    public override string GetTypeName()
    {
        return _ifEffects[0].GetType().Name;
    }
}