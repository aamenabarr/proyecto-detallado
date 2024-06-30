namespace Fire_Emblem;

public class SkillsManager
{
    private List<Tuple<List<Condition>, Effect>> _skills = new();

    public void Add(List<Condition> conditions , Effect effect)
    {
        _skills.Add(new Tuple<List<Condition>, Effect>(conditions, effect));
    }

    public void ApplySkills()
    {
        SortSkills();
        ApplyEffects();
    }

    private void SortSkills()
    {
        _skills = _skills.OrderBy(skill => Array.IndexOf(_effectOrder, skill.Item2.GetTypeName())).ToList();
    }
    
    private string[] _effectOrder = 
    {
        "AlterBaseStats",
        "Bonus", 
        "BonusInFirstAttack", 
        "BonusInFollowUp", 
        "Penalty", 
        "PenaltyInFirstAttack",
        "PenaltyInFollowUp",
        "ExtraDamage", 
        "ReductionOfPercentageDamage",
        "PercentageDamageReduction", 
        "PercentageDamageReductionInFirstAttack", 
        "PercentageDamageReductionInFollowUp",
        "AbsolutDamageReduction",
        "ExtraDamageInFirstAttack",
        "ExtraDamageInFollowUp",
        "HealingBeforeCombat",
        "Healing",
        "CounterAttackDenial",
        "DenialOfCounterAttackDenial",
        "FollowUpGuarantee",
        "DenialOfFollowUp",
        "DenialOfFollowUpGuarantee",
        "DenialOfFollowUpDenial",
        "HealingAfterCombat"
    };
    
    private void ApplyEffects()
    {
        foreach (var skill in _skills)
            if (ConditionsAreMet(skill.Item1))
                skill.Item2.Apply();
    }

    private bool ConditionsAreMet(List<Condition> conditions)
    {
        foreach (var condition in conditions)
            if (!condition.IsMet())
                return false;
        return true;
    }

    public void Reset()
    {
        _skills.Clear();
    }
}