namespace Fire_Emblem;

public static class SkillFactory
{
    public static void SetSkill(Skill skill)
    {
        SetAlterBaseStats(skill);
        SetBonus(skill);
        SetPenalty(skill);
        SetNeutralizeBonus(skill);
        SetNeutralizePenalty(skill);
        SetExtraDamage(skill);
        SetPercentageDamageReduction(skill);
        SetAbsolutDamageReduction(skill);
        SetHybrid(skill);
    }

    public static void SetAlterBaseStats(Skill skill)
    {
        switch (skill.Name)
        {
            case "HP +15":
                skill.Conditions.Add(new InFirstCombat(skill.Unit));
                skill.Effects.Add(new AlterBaseStats(skill.Unit, "Hp", 15));
                break;
        }
    }
    
    public static void SetBonus(Skill skill)
    {
        switch (skill.Name)
        {
            case "Fair Fight":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit.Rival, "Atk", 6));
                break;
            case "Will to Win":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 50, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 8));
                break;
            case "Single-Minded":
                skill.Conditions.Add(new LastRival(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 8));
                break;
            case "Ignis":
                var effect = new BonusInFirstAttack(skill.Unit, "Atk", skill.Unit.Atk / 2);
                skill.Effects.Add(effect);
                break;
            case "Perceptive":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 12 + skill.Unit.Spd / 4));
                break;
            case "Tome Precision":
                skill.Conditions.Add(new TypeOfAttack(skill.Unit, "Magic"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                break;
            case "Attack +6":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                break;
            case "Speed +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 5));
                break;
            case "Defense +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 5));
                break;
            case "Wrath":
                var max_bonus = 30;
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", Math.Min(max_bonus, skill.Unit.InitialStats["Hp"] - skill.Unit.Hp)));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", Math.Min(max_bonus, skill.Unit.InitialStats["Hp"] - skill.Unit.Hp)));
                break;
            case "Resolve":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 75, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 7));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 7));
                break;
            case "Resistance +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 5));
                break;
            case "Atk/Def +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 5));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 5));
                break;
            case "Atk/Res +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 5));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 5));
                break;
            case "Spd/Res +5":
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 5));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 5));
                break;
            case "Deadly Blade":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Sword"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 8));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 8));
                break;
            case "Death Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 8));
                break;
            case "Armored Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 8));
                break;
            case "Darting Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 8));
                break;
            case "Warding Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 8));
                break;
            case "Swift Sparrow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                break;
            case "Sturdy Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                break;
            case "Mirror Strike":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                break;
            case "Steady Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                break;
            case "Swift Strike":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                break;
            case "Bracing Blow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                break;
            case "Brazen Atk/Spd":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 10));
                break;
            case "Brazen Atk/Def":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 10));
                break;
            case "Brazen Atk/Res":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 10));
                break;
            case "Brazen Spd/Def":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 10));
                break;
            case "Brazen Spd/Res":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 10));
                break;
            case "Brazen Def/Res":
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 80, "%"));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 10));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 10));
                break;
            case "Fire Boost":
                skill.Conditions.Add(new HpRange(skill.Unit, ">=", skill.Unit.Rival.Hp + 3));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                break;
            case "Wind Boost":
                skill.Conditions.Add(new HpRange(skill.Unit, ">=", skill.Unit.Rival.Hp + 3));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                break;
            case "Earth Boost":
                skill.Conditions.Add(new HpRange(skill.Unit, ">=", skill.Unit.Rival.Hp + 3));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                break;
            case "Water Boost":
                skill.Conditions.Add(new HpRange(skill.Unit, ">=", skill.Unit.Rival.Hp + 3));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                break;
            case "Chaos Style":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(skill.Unit, "Physical"),
                        new TypeOfAttack(skill.Unit.Rival, "Magic")
                    }),
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(skill.Unit, "Magic"),
                        new TypeOfAttack(skill.Unit.Rival, "Physical")
                    }),
                }));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 3));
                break;
        }
    }

    public static void SetPenalty(Skill skill)
    {
        switch (skill.Name)
        {
            case "Blinding Flash":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -4));
                break;
            case "Not *Quite*":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -4));
                break;
            case "Stunning Smile":
                skill.Conditions.Add(new IsMale(skill.Unit.Rival));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -8));
                break;
            case "Disarming Sigh":
                skill.Conditions.Add(new IsMale(skill.Unit.Rival));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -8));
                break;
            case "Charmer":
                skill.Conditions.Add(new LastRival(skill.Unit));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -3));
                break;
            case "Luna":
                var effect1 = new PenaltyInFirstAttack(skill.Unit.Rival, "Def", -skill.Unit.Rival.Def / 2);
                var effect2 = new PenaltyInFirstAttack(skill.Unit.Rival, "Res", -skill.Unit.Rival.Res / 2);
                skill.Effects.Add(effect1);
                skill.Effects.Add(effect2);
                break;
            case "Belief in Love":
                skill.Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(skill.Unit.Rival),
                    new HpRange(skill.Unit.Rival, ">=", 100, mode: "%")
                }));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -5));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -5));
                break;
        }
    }

    public static void SetNeutralizeBonus(Skill skill)
    {
        switch (skill.Name)
        {
            case "Beorc's Blessing":
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival));
                break;
        }
    }
    
    public static void SetNeutralizePenalty(Skill skill)
    {
        switch (skill.Name)
        {
            case "Agnea's Arrow":
                skill.Effects.Add(new NeutralizePenalty(skill.Unit));
                break;
        }
    }
    
    public static void SetExtraDamage(Skill skill)
    {
        switch (skill.Name)
        {
            case "Back at You":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new ExtraDamage(skill.Unit, (skill.Unit.InitialStats["Hp"] - skill.Unit.Hp) / 2));
                break;
            case "Lunar Brace":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Conditions.Add(new TypeOfAttack(skill.Unit, "Physical"));
                skill.Effects.Add(new ExtraDamage(skill.Unit, "Def", 30));
                break;
            case "Bravery":
                skill.Effects.Add(new ExtraDamage(skill.Unit, 5));
                break;
        }
    }

    public static void SetPercentageDamageReduction(Skill skill)
    {
        switch (skill.Name)
        {
            case "Dragon Wall":
                skill.Conditions.Add(new StatsComparison(skill.Unit, "Res", ">", skill.Unit.Rival, "Res"));
                skill.Effects.Add(new PercentageDamageReduction(skill.Unit, "Res"));
                break;
            case "Dodge":
                skill.Conditions.Add(new StatsComparison(skill.Unit, "Spd", ">", skill.Unit.Rival, "Spd"));
                skill.Effects.Add(new PercentageDamageReduction(skill.Unit, "Spd"));
                break;
            case "Golden Lotus":
                skill.Conditions.Add(new TypeOfAttack(skill.Unit.Rival, "Physical"));
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 50));
                break;
        }
    }
    
    public static void SetAbsolutDamageReduction(Skill skill)
    {
        switch (skill.Name)
        {
            case "Gentility":
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
            case "Bow Guard":
                skill.Conditions.Add(new UseWeapon(skill.Unit.Rival, "Bow"));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
            case "Arms Shield":
                skill.Conditions.Add(new HasWeaponAdvantage(skill.Unit));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -7));
                break;
            case "Axe Guard":
                skill.Conditions.Add(new UseWeapon(skill.Unit.Rival, "Axe"));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
            case "Magic Guard":
                skill.Conditions.Add(new UseWeapon(skill.Unit.Rival, "Magic"));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
            case "Lance Guard":
                skill.Conditions.Add(new UseWeapon(skill.Unit.Rival, "Lance"));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
            case "Sympathetic":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Conditions.Add(new HpRange(skill.Unit, "<=", 50, "%"));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                break;
        }
    }
    
    public static void SetHybrid(Skill skill)
    {
        switch (skill.Name)
        {
            case "Soulblade":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Sword"));
                var z = (skill.Unit.Rival.Def + skill.Unit.Rival.Res) / 2;
                var x = z - skill.Unit.Rival.Def;
                var y = z - skill.Unit.Rival.Res;
                if (x > 0) skill.Effects.Add(new Bonus(skill.Unit.Rival, "Def", x));
                else skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", x));
                if (y > 0) skill.Effects.Add(new Bonus(skill.Unit.Rival, "Res", y));
                else skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", y));
                break;
            case "Sandstorm":
                var w = skill.Unit.Def / 2 + skill.Unit.Def - skill.Unit.Atk;
                Effect effect = (w > 0) ? new BonusInFollowUp(skill.Unit, "Atk", w) : 
                    new PenaltyInFollowUp(skill.Unit, "Atk", w);
                skill.Effects.Add(effect);
                break;
            case "Sword Agility":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Sword"));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 12));
                skill.Effects.Add(new Penalty(skill.Unit, "Atk", -6));
                break;
            case "Lance Power":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Lance"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -10));
                break;
            case "Sword Power":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Sword"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -10));
                break;
            case "Bow Focus":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Bow"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Penalty(skill.Unit, "Res", -10));
                break;
            case "Lance Agility":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Lance"));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 12));
                skill.Effects.Add(new Penalty(skill.Unit, "Atk", -6));
                break;
            case "Axe Power":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Axe"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -10));
                break;
            case "Bow Agility":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Bow"));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 12));
                skill.Effects.Add(new Penalty(skill.Unit, "Atk", -6));
                break;
            case "Sword Focus":
                skill.Conditions.Add(new UseWeapon(skill.Unit, "Sword"));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 10));
                skill.Effects.Add(new Penalty(skill.Unit, "Res", -10));
                break;
            case "Close Def":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(skill.Unit.Rival, "Sword"),
                    new UseWeapon(skill.Unit.Rival, "Lance"),
                    new UseWeapon(skill.Unit.Rival, "Axe")
                }));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 8));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 8));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival));
                break;
            case "Distant Def":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(skill.Unit.Rival, "Magic"),
                    new UseWeapon(skill.Unit.Rival, "Bow")
                }));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 8));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 8));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival));
                break;
            case "Lull Atk/Spd":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Atk"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Spd"));
                break;
            case "Lull Atk/Def":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Atk"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Def"));
                break;
            case "Lull Atk/Res":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Atk"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Res"));
                break;
            case "Lull Spd/Def":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Spd"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Def"));
                break;
            case "Lull Spd/Res":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Spd"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Res"));
                break;
            case "Lull Def/Res":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -3));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", -3));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Def"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival, "Res"));
                break;
            case "Fort. Def/Res":
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new Penalty(skill.Unit, "Atk", -2));
                break;
            case "Life and Death":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -5));
                skill.Effects.Add(new Penalty(skill.Unit, "Res", -5));
                break;
            case "Solid Ground":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Penalty(skill.Unit, "Res", -5));
                break;
            case "Still Water":
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -5));
                break;
            case "Dragonskin":
                skill.Conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(skill.Unit.Rival),
                    new HpRange(skill.Unit.Rival, ">=", 75, "%")
                }));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival));
                break;
            case "Light and Dark":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -5));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -5));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -5));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", -5));
                skill.Effects.Add(new NeutralizePenalty(skill.Unit));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit.Rival));
                break;
            case "Bushido":
                skill.Effects.Add(new ExtraDamage(skill.Unit, 7));
                skill.Effects.Add(new ConditionalEffect(
                    new StatsComparison(skill.Unit, "Spd", ">", skill.Unit.Rival, "Spd"), 
                    new List<Effect>{ new PercentageDamageReduction(skill.Unit, "Spd") }));
                break;
            case "Moon-Twin Wing":
                skill.Effects.Add(new ConditionalEffect(
                    new HpRange(skill.Unit, ">=", 25, "%"), 
                    new List<Effect>
                    {
                        new Penalty(skill.Unit.Rival, "Atk", -5),
                        new Penalty(skill.Unit.Rival, "Spd", -5)
                    }));
                skill.Effects.Add(new ConditionalEffect(
                    new HybridAndCondition(new List<Condition>()
                    {
                        new HpRange(skill.Unit, ">=", 25, "%"),
                        new StatsComparison(skill.Unit, "Spd", ">", skill.Unit.Rival, "Spd")
                    }),
                    new List<Effect>{ new PercentageDamageReduction(skill.Unit, "Spd") }));
                break;
            case "Blue Skies":
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -5));
                skill.Effects.Add(new ExtraDamage(skill.Unit, 5));
                break;
            case "Aegis Shield":
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 3));
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 50));
                break;
            case "Remote Sparrow":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 7));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 7));
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 30));
                break;
            case "Remote Mirror":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 7));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 10));
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 30));
                break;
            case "Remote Sturdy":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 7));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 10));
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 30));
                break;
            case "Fierce Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 8));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Darting Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 8));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Steady Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 8));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Warding Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 8));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Kestrel Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Sturdy Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Mirror Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Atk", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Steady Posture":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Swift Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Spd", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Bracing Stance":
                skill.Conditions.Add(new StartsAttack(skill.Unit.Rival));
                skill.Effects.Add(new Bonus(skill.Unit, "Def", 6));
                skill.Effects.Add(new Bonus(skill.Unit, "Res", 6));
                skill.Effects.Add(new PercentageDamageReductionInFollowUp(skill.Unit, 10));
                break;
            case "Poetic Justice":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -4));
                skill.Effects.Add(new ExtraDamage(skill.Unit, "Atk", 15));
                break;
            case "Laguz Friend":
                skill.Effects.Add(new PercentageDamageReduction(skill.Unit, 50));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit, "Def"));
                skill.Effects.Add(new NeutralizeBonus(skill.Unit, "Res"));
                skill.Effects.Add(new Penalty(skill.Unit, "Def", -(skill.Unit.Def / 2)));
                skill.Effects.Add(new Penalty(skill.Unit, "Res", -(skill.Unit.Res / 2)));
                break;
            case "Chivalry":
                skill.Conditions.Add(new StartsAttack(skill.Unit));
                skill.Conditions.Add(new HpRange(skill.Unit.Rival, ">=", 100, "%"));
                skill.Effects.Add(new ExtraDamage(skill.Unit, 2));
                skill.Effects.Add(new AbsolutDamageReduction(skill.Unit, -2));
                break;
            case "Dragon's Wrath":
                skill.Effects.Add(new PercentageDamageReductionInFirstAttack(skill.Unit, 25));
                skill.Effects.Add(new ConditionalEffect(
                    new StatsComparison(skill.Unit, "Atk", ">", skill.Unit.Rival, "Res"), 
                    new List<Effect>{ new ExtraDamageInFirstAttack(skill.Unit, "Atk", "Res", 25) }));
                break;
            case "Prescience":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Atk", -5));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Res", -5));
                skill.Effects.Add(new ConditionalEffect(
                    new HybridOrCondition(new List<Condition>()
                    { 
                        new StartsAttack(skill.Unit), 
                        new UseWeapon(skill.Unit.Rival, "Magic"), 
                        new UseWeapon(skill.Unit.Rival, "Bow")
                    }), 
                    new List<Effect>{ new PercentageDamageReductionInFirstAttack(skill.Unit, 30) }));
                
                break;
            case "Extra Chivalry":
                skill.Effects.Add(new ConditionalEffect(
                    new HpRange(skill.Unit.Rival, ">=", 50, "%"),
                    new List<Effect>
                    {
                        new Penalty(skill.Unit.Rival, "Atk", -5),
                        new Penalty(skill.Unit.Rival, "Spd", -5),
                        new Penalty(skill.Unit.Rival, "Def", -5)
                        
                    }));
                skill.Effects.Add(new PercentageDamageReduction(
                    skill.Unit, (skill.Unit.Rival.Hp * 50) / skill.Unit.Rival.InitialStats["Hp"])); 
                break;
            case "Guard Bearing":
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Spd", -4));
                skill.Effects.Add(new Penalty(skill.Unit.Rival, "Def", -4));
                skill.Effects.Add(new ConditionalElseEffect(
                    new HybridOrCondition(new List<Condition>
                    {
                        new HybridAndCondition(new List<Condition>
                        {
                            new InFirstCombat(skill.Unit, "Attacker")
                        }),
                        new HybridAndCondition(new List<Condition>()
                        {
                            new InFirstCombat(skill.Unit, "Defender")
                        }),
                    }),
                    new List<Effect>{ new PercentageDamageReduction(skill.Unit, 60) },
                    new List<Effect>{ new PercentageDamageReduction(skill.Unit, 30) }
                    ));
                break;
            case "Divine Recreation":
                skill.Effects.Add(new ConditionalEffect(
                    new HpRange(skill.Unit.Rival, ">=", 50, "%"),
                    new List<Effect>
                    {
                        new Penalty(skill.Unit.Rival, "Atk", -4),
                        new Penalty(skill.Unit.Rival, "Spd", -4),
                        new Penalty(skill.Unit.Rival, "Def", -4),
                        new Penalty(skill.Unit.Rival, "Res", -4)
                    }
                ));
                skill.Effects.Add(new ConditionalEffect(
                    new HpRange(skill.Unit.Rival, ">=", 50, "%"),
                    new List<Effect> { new PercentageDamageReductionInFirstAttack(skill.Unit, 30) }
                ));
                skill.Effects.Add(new ConditionalEffect(
                    new HpRange(skill.Unit.Rival, ">=", 50, "%"),
                    skill.Unit.IsAttacker ? 
                        new List<Effect> { new ExtraDamageInFollowUp(skill.Unit, "Dmg") } :
                        new List<Effect> { new ExtraDamageInFirstAttack(skill.Unit, "Dmg") }
                ));
                break;
        }
    }
}