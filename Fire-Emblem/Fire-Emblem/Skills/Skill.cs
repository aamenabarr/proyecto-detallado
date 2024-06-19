namespace Fire_Emblem;

public class Skill
{
    public string Name;
    public Unit Unit;
    public Conditions Conditions = new();
    public Effects Effects = new();

    public Skill(string name, Unit unit)
    {
        Name = name;
        Unit = unit;
        SkillFactory.SetSkill(this);
    }
}