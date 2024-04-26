namespace Fire_Emblem;

public class InFollowUp : Condition
{
    private List<Effect> _effects;

    public InFollowUp(List<Effect> effects)
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
            effect.InFollowUp = true;
    }
}