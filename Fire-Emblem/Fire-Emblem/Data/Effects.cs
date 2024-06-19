namespace Fire_Emblem;

public class Effects
{
    private List<Effect> _effects = new();

    public void Add(Effect effect)
    {
        _effects.Add(effect);
    }
    
    public List<Effect> Get()
    {
        return _effects;
    }
}