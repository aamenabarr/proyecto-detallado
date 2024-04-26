namespace Fire_Emblem;

public class Skill
{
    public string Name;
    public string Description;
    public Unit Unit;
    public List<Condition> Conditions = new List<Condition>();
    public List<Effect> Effects = new List<Effect>();

    public Skill(AuxSkill skill)
    {
        Name = skill.Name;
        Description = skill.Description;
    }

    public void SetSkill()
    {
        SetAlterBaseStats();
        SetBonus();
        SetPenalty();
        SetNeutralizeBonus();
        SetNeutralizePenalty();
        SetHybrid();
        AddEffectsToManager();
    }

    private void SetAlterBaseStats()
    {
        switch (Name)
        {
            case "HP +15":
                Conditions.Add(new InFirstRound(Unit));
                Effects.Add(new AlterBaseStats(Unit, "Hp", 15));
                break;  
        }
    }
    
    private void SetBonus()
    {
        switch (Name)
        {
            case "Fair Fight":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit.Rival, "Atk", 6));
                break;
            case "Will to Win":
                Conditions.Add(new HpRange(Unit, "<=", 50, "%"));
                Effects.Add(new Bonus(Unit, "Atk", 8));
                break;
            case "Single-Minded":
                Conditions.Add(new LastRival(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 8));
                break;
            case "Ignis":
                var effect = new Bonus(Unit, "Atk", Unit.Atk / 2);
                effect.InFirstAttack = true;
                Effects.Add(effect);
                break;
            case "Perceptive":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Spd", 12 + Unit.Spd / 4));
                break;
            case "Tome Precision":
                Conditions.Add(new TypeOfAttack(Unit, "Magic"));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                break;
            case "Attack +6":
                Effects.Add(new Bonus(Unit, "Atk", 6));
                break;
            case "Speed +5":
                Effects.Add(new Bonus(Unit, "Spd", 5));
                break;
            case "Defense +5":
                Effects.Add(new Bonus(Unit, "Def", 5));
                break;
            case "Wrath":
                Effects.Add(new Bonus(Unit, "Atk", Math.Min(30, Unit.InitialHp - Unit.Hp)));
                Effects.Add(new Bonus(Unit, "Spd", Math.Min(30, Unit.InitialHp - Unit.Hp)));
                break;
            case "Resolve":
                Conditions.Add(new HpRange(Unit, "<=", 75, "%"));
                Effects.Add(new Bonus(Unit, "Def", 7));
                Effects.Add(new Bonus(Unit, "Res", 7));
                break;
            case "Resistance +5":
                Effects.Add(new Bonus(Unit, "Res", 5));
                break;
            case "Atk/Def +5":
                Effects.Add(new Bonus(Unit, "Atk", 5));
                Effects.Add(new Bonus(Unit, "Def", 5));
                break;
            case "Atk/Res +5":
                Effects.Add(new Bonus(Unit, "Atk", 5));
                Effects.Add(new Bonus(Unit, "Res", 5));
                break;
            case "Spd/Res +5":
                Effects.Add(new Bonus(Unit, "Spd", 5));
                Effects.Add(new Bonus(Unit, "Res", 5));
                break;
            case "Deadly Blade":
                Conditions.Add(new StartsAttack(Unit));
                Conditions.Add(new UseWeapon(Unit, "Sword"));
                Effects.Add(new Bonus(Unit, "Atk", 8));
                Effects.Add(new Bonus(Unit, "Spd", 8));
                break;
            case "Death Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 8));
                break;
            case "Armored Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Def", 8));
                break;
            case "Darting Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Spd", 8));
                break;
            case "Warding Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Res", 8));
                break;
            case "Swift Sparrow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                break;
            case "Sturdy Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Def", 6));
                break;
            case "Mirror Strike":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                break;
            case "Steady Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                Effects.Add(new Bonus(Unit, "Def", 6));
                break;
            case "Swift Strike":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                break;
            case "Bracing Blow":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Bonus(Unit, "Def", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                break;
            case "Brazen Atk/Spd":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Bonus(Unit, "Spd", 10));
                break;
            case "Brazen Atk/Def":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Bonus(Unit, "Def", 10));
                break;
            case "Brazen Atk/Res":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Bonus(Unit, "Res", 10));
                break;
            case "Brazen Spd/Def":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Spd", 10));
                Effects.Add(new Bonus(Unit, "Def", 10));
                break;
            case "Brazen Spd/Res":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Spd", 10));
                Effects.Add(new Bonus(Unit, "Res", 10));
                break;
            case "Brazen Def/Res":
                Conditions.Add(new HpRange(Unit, "<=", 80, "%"));
                Effects.Add(new Bonus(Unit, "Def", 10));
                Effects.Add(new Bonus(Unit, "Res", 10));
                break;
            case "Fire Boost":
                Conditions.Add(new HpRange(Unit, ">=", Unit.Rival.Hp + 3));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                break;
            case "Wind Boost":
                Conditions.Add(new HpRange(Unit, ">=", Unit.Rival.Hp + 3));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                break;
            case "Earth Boost":
                Conditions.Add(new HpRange(Unit, ">=", Unit.Rival.Hp + 3));
                Effects.Add(new Bonus(Unit, "Def", 6));
                break;
            case "Water Boost":
                Conditions.Add(new HpRange(Unit, ">=", Unit.Rival.Hp + 3));
                Effects.Add(new Bonus(Unit, "Res", 6));
                break;
            case "Chaos Style":
                Conditions.Add(new StartsAttack(Unit));
                Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(Unit, "Physical"),
                        new TypeOfAttack(Unit.Rival, "Magic")
                    }),
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(Unit, "Magic"),
                        new TypeOfAttack(Unit.Rival, "Physical")
                    }),
                }));
                Effects.Add(new Bonus(Unit, "Spd", 3));
                break;
        }
    }

    private void SetPenalty()
    {
        switch (Name)
        {
            case "Blinding Flash":
                Conditions.Add(new StartsAttack(Unit));
                Effects.Add(new Penalty(Unit.Rival, "Spd", -4));
                break;
            case "Not *Quite*":
                Conditions.Add(new StartsAttack(Unit.Rival));
                Effects.Add(new Penalty(Unit.Rival, "Atk", -4));
                break;
            case "Stunning Smile":
                Conditions.Add(new IsMale(Unit.Rival));
                Effects.Add(new Penalty(Unit.Rival, "Spd", -8));
                break;
            case "Disarming Sigh":
                Conditions.Add(new IsMale(Unit.Rival));
                Effects.Add(new Penalty(Unit.Rival, "Atk", -8));
                break;
            case "Charmer":
                Conditions.Add(new LastRival(Unit));
                Effects.Add(new Penalty(Unit.Rival, "Atk", -3));
                Effects.Add(new Penalty(Unit.Rival, "Spd", -3));
                break;
            case "Luna":
                var effect1 = new Penalty(Unit.Rival, "Def", -Unit.Rival.Def / 2);
                var effect2 = new Penalty(Unit.Rival, "Res", -Unit.Rival.Res / 2);
                effect1.InFirstAttack = true;
                effect2.InFirstAttack = true;
                Effects.Add(effect1);
                Effects.Add(effect2);
                break;
            case "Belief in Love":
                Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(Unit.Rival),
                    new HpRange(Unit.Rival, ">=", 100, mode: "%")
                }));
                Effects.Add(new Penalty(Unit.Rival, "Atk", -5));
                Effects.Add(new Penalty(Unit.Rival, "Def", -5));
                break;
        }
    }

    private void SetNeutralizeBonus()
    {
        switch (Name)
        {
            case "Beorc's Blessing":
                Effects.Add(new NeutralizeBonus(Unit.Rival));
                break;
        }
    }
    
    private void SetNeutralizePenalty()
    {
        switch (Name)
        {
            case "Agnea's Arrow":
                Effects.Add(new NeutralizePenalty(Unit));
                break;
        }
    }
    
    private void SetHybrid()
    {
        switch (Name)
        {
            case "Soulblade":
                Conditions.Add(new UseWeapon(Unit, "Sword"));
                var z = (Unit.Rival.InitialDef + Unit.Rival.InitialRes) / 2;
                var x = z - Unit.Rival.InitialDef;
                var y = z - Unit.Rival.InitialRes;
                if (x > 0) Effects.Add(new Bonus(Unit.Rival, "Def", x));
                else Effects.Add(new Penalty(Unit.Rival, "Def", x));
                if (y > 0) Effects.Add(new Bonus(Unit.Rival, "Res", y));
                else Effects.Add(new Penalty(Unit.Rival, "Res", y));
                break;
            case "Sandstorm":
                var w = Unit.InitialDef / 2 + Unit.InitialDef - Unit.InitialAtk;
                Effect effect = (w > 0) ? new Bonus(Unit, "Atk", w) : new Penalty(Unit, "Atk", w);
                effect.InFollowUp = true;
                Effects.Add(effect);
                break;
            case "Sword Agility":
                Conditions.Add(new UseWeapon(Unit, "Sword"));
                Effects.Add(new Bonus(Unit, "Spd", 12));
                Effects.Add(new Penalty(Unit, "Atk", -6));
                break;
            case "Lance Power":
                Conditions.Add(new UseWeapon(Unit, "Lance"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Penalty(Unit, "Def", -10));
                break;
            case "Sword Power":
                Conditions.Add(new UseWeapon(Unit, "Sword"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Penalty(Unit, "Def", -10));
                break;
            case "Bow Focus":
                Conditions.Add(new UseWeapon(Unit, "Bow"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Penalty(Unit, "Res", -10));
                break;
            case "Lance Agility":
                Conditions.Add(new UseWeapon(Unit, "Lance"));
                Effects.Add(new Bonus(Unit, "Spd", 12));
                Effects.Add(new Penalty(Unit, "Atk", -6));
                break;
            case "Axe Power":
                Conditions.Add(new UseWeapon(Unit, "Axe"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Penalty(Unit, "Def", -10));
                break;
            case "Bow Agility":
                Conditions.Add(new UseWeapon(Unit, "Bow"));
                Effects.Add(new Bonus(Unit, "Spd", 12));
                Effects.Add(new Penalty(Unit, "Atk", -6));
                break;
            case "Sword Focus":
                Conditions.Add(new UseWeapon(Unit, "Sword"));
                Effects.Add(new Bonus(Unit, "Atk", 10));
                Effects.Add(new Penalty(Unit, "Res", -10));
                break;
            case "Close Def":
                Conditions.Add(new StartsAttack(Unit.Rival));
                Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(Unit.Rival, "Sword"),
                    new UseWeapon(Unit.Rival, "Lance"),
                    new UseWeapon(Unit.Rival, "Axe")
                }));
                Effects.Add(new Bonus(Unit, "Def", 8));
                Effects.Add(new Bonus(Unit, "Res", 8));
                Effects.Add(new NeutralizeBonus(Unit.Rival));
                break;
            case "Distant Def":
                Conditions.Add(new StartsAttack(Unit.Rival));
                Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(Unit.Rival, "Magic"),
                    new UseWeapon(Unit.Rival, "Bow")
                }));
                Effects.Add(new Bonus(Unit, "Def", 8));
                Effects.Add(new Bonus(Unit, "Res", 8));
                Effects.Add(new NeutralizeBonus(Unit.Rival));
                break;
            case "Lull Atk/Spd":
                Effects.Add(new Penalty(Unit.Rival, "Atk", -3));
                Effects.Add(new Penalty(Unit.Rival, "Spd", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Atk"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Spd"));
                break;
            case "Lull Atk/Def":
                Effects.Add(new Penalty(Unit.Rival, "Atk", -3));
                Effects.Add(new Penalty(Unit.Rival, "Def", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Atk"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Def"));
                break;
            case "Lull Atk/Res":
                Effects.Add(new Penalty(Unit.Rival, "Atk", -3));
                Effects.Add(new Penalty(Unit.Rival, "Res", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Atk"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Res"));
                break;
            case "Lull Spd/Def":
                Effects.Add(new Penalty(Unit.Rival, "Spd", -3));
                Effects.Add(new Penalty(Unit.Rival, "Def", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Spd"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Def"));
                break;
            case "Lull Spd/Res":
                Effects.Add(new Penalty(Unit.Rival, "Spd", -3));
                Effects.Add(new Penalty(Unit.Rival, "Res", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Spd"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Res"));
                break;
            case "Lull Def/Res":
                Effects.Add(new Penalty(Unit.Rival, "Def", -3));
                Effects.Add(new Penalty(Unit.Rival, "Res", -3));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Def"));
                Effects.Add(new NeutralizeBonus(Unit.Rival, "Res"));
                break;
            case "Fort. Def/Res":
                Effects.Add(new Bonus(Unit, "Def", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                Effects.Add(new Penalty(Unit, "Atk", -2));
                break;
            case "Life and Death":
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                Effects.Add(new Penalty(Unit, "Def", -5));
                Effects.Add(new Penalty(Unit, "Res", -5));
                break;
            case "Solid Ground":
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Def", 6));
                Effects.Add(new Penalty(Unit, "Res", -5));
                break;
            case "Still Water":
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                Effects.Add(new Penalty(Unit, "Def", -5));
                break;
            case "Dragonskin":
                Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(Unit.Rival),
                    new HpRange(Unit.Rival, ">=", 75, "%")

                }));
                Effects.Add(new Bonus(Unit, "Atk", 6));
                Effects.Add(new Bonus(Unit, "Spd", 6));
                Effects.Add(new Bonus(Unit, "Def", 6));
                Effects.Add(new Bonus(Unit, "Res", 6));
                Effects.Add(new NeutralizeBonus(Unit.Rival));
                break;
            case "Light and Dark":
                Effects.Add(new Penalty(Unit.Rival, "Atk", -5));
                Effects.Add(new Penalty(Unit.Rival, "Spd", -5));
                Effects.Add(new Penalty(Unit.Rival, "Def", -5));
                Effects.Add(new Penalty(Unit.Rival, "Res", -5));
                Effects.Add(new NeutralizePenalty(Unit));
                Effects.Add(new NeutralizeBonus(Unit.Rival));
                break;
        }
    }

    private void AddEffectsToManager()
    {
        if (ConditionsAreMet())
        {
            foreach (var effect in Effects)
                effect.AddToManager();
        }
    }

    private bool ConditionsAreMet()
    {
        foreach (var condition in Conditions)
            if (!condition.IsMet())
                return false;
        return true;
    }

    public void Reset()
    {
        foreach (var effect in Effects)
            effect.Reset();
        Effects.Clear();
        Conditions.Clear();
    }
}