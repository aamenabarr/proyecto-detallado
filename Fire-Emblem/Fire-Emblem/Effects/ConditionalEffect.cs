namespace Fire_Emblem;

public class ConditionalEffect : Effect
{
    private Condition _condition;
    private List<Effect> _effects;
    
    public ConditionalEffect(Condition condition, List<Effect> effects)
    {
        _condition = condition;
        _effects = effects;
    }
    
    public override void Apply()
    {
        if (_condition.IsMet())
            foreach (var effect in _effects)
                effect.Apply();
    }
}