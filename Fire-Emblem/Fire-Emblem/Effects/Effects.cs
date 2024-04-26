namespace Fire_Emblem;


public class Bonus : Effect
{
    public Bonus(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public void Apply()
    {
        AlterStat();
    }
    
    public void Reset()
    {
        ResetStat();
    }
}

public class Penalty : Effect
{
    public Penalty(Unit unit, string stat, int value) : base(unit, stat, value) {}
    
    public void Apply()
    {
        AlterStat();
    }
    
    public void Reset()
    {
        ResetStat();
    }
}

public class NeutralizeBonus : Effect
{
    public NeutralizeBonus(Unit unit) : base(unit) {}

    public void Apply()
    {
        Neutralize("Bonus");
    }
}

public class NeutralizePenalty : Effect
{
    public NeutralizePenalty(Unit unit) : base(unit) {}
    
    public void Apply()
    {
        Neutralize("Penalty");
    }
}