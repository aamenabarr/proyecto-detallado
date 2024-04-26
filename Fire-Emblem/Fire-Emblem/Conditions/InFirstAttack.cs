namespace Fire_Emblem;

public class InFirstAttack : Condition
{
    private List<Effect> _effects;

    public InFirstAttack(List<Effect> effects)
    {
        _effects = effects;
    }
    
    public bool IsMet()
    {
        ApplyCondition();
        return true;
    }

    public void ApplyCondition()
    {
        foreach (var effect in _effects)
            effect.InFirstAttack = true;
    }
}