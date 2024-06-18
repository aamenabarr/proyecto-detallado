namespace Fire_Emblem;

public class Skill
{
    public string Name;
    public Unit Unit;
    public List<Condition> Conditions = new();
    public List<Effect> Effects = new();

    public Skill(string name, Unit unit)
    {
        Name = name;
        Unit = unit;
        SkillFactory.SetSkill(this);
    }
}