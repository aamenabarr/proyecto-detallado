namespace Fire_Emblem;

public class Skill
{
    private string _name;
    private Unit _unit;
    private List<Condition> _conditions = new();
    private List<Effect> _effects = new();
    private SkillsManager _skillsManager;

    public Skill(string name, Unit unit, SkillsManager skillsManager)
    {
        _name = name;
        _unit = unit;
        _skillsManager = skillsManager;
        SetSkill();
    }

    private void SetSkill()
    {
        SetAlterBaseStats();
        SetBonus();
        SetPenalty();
        SetNeutralizeBonus();
        SetNeutralizePenalty();
        SetExtraDamage();
        SetPercentageDamageReduction();
        SetAbsolutDamageReduction();
        SetHybrid();
        AddSkillToSkillsManager();
    }

    private void SetAlterBaseStats()
    {
        switch (_name)
        {
            case "HP +15":
                _conditions.Add(new InFirstCombat(_unit));
                _effects.Add(new AlterBaseStats(_unit, "Hp", 15));
                break;
        }
    }
    
    private void SetBonus()
    {
        switch (_name)
        {
            case "Fair Fight":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit.Rival, "Atk", 6));
                break;
            case "Will to Win":
                _conditions.Add(new HpRange(_unit, "<=", 50, "%"));
                _effects.Add(new Bonus(_unit, "Atk", 8));
                break;
            case "Single-Minded":
                _conditions.Add(new LastRival(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 8));
                break;
            case "Ignis":
                var effect = new BonusInFirstAttack(_unit, "Atk", _unit.Atk / 2);
                _effects.Add(effect);
                break;
            case "Perceptive":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Spd", 12 + _unit.Spd / 4));
                break;
            case "Tome Precision":
                _conditions.Add(new TypeOfAttack(_unit, "Magic"));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                break;
            case "Attack +6":
                _effects.Add(new Bonus(_unit, "Atk", 6));
                break;
            case "Speed +5":
                _effects.Add(new Bonus(_unit, "Spd", 5));
                break;
            case "Defense +5":
                _effects.Add(new Bonus(_unit, "Def", 5));
                break;
            case "Wrath":
                _effects.Add(new Bonus(_unit, "Atk", Math.Min(30, _unit.InitialStats["Hp"] - _unit.Hp)));
                _effects.Add(new Bonus(_unit, "Spd", Math.Min(30, _unit.InitialStats["Hp"] - _unit.Hp)));
                break;
            case "Resolve":
                _conditions.Add(new HpRange(_unit, "<=", 75, "%"));
                _effects.Add(new Bonus(_unit, "Def", 7));
                _effects.Add(new Bonus(_unit, "Res", 7));
                break;
            case "Resistance +5":
                _effects.Add(new Bonus(_unit, "Res", 5));
                break;
            case "Atk/Def +5":
                _effects.Add(new Bonus(_unit, "Atk", 5));
                _effects.Add(new Bonus(_unit, "Def", 5));
                break;
            case "Atk/Res +5":
                _effects.Add(new Bonus(_unit, "Atk", 5));
                _effects.Add(new Bonus(_unit, "Res", 5));
                break;
            case "Spd/Res +5":
                _effects.Add(new Bonus(_unit, "Spd", 5));
                _effects.Add(new Bonus(_unit, "Res", 5));
                break;
            case "Deadly Blade":
                _conditions.Add(new StartsAttack(_unit));
                _conditions.Add(new UseWeapon(_unit, "Sword"));
                _effects.Add(new Bonus(_unit, "Atk", 8));
                _effects.Add(new Bonus(_unit, "Spd", 8));
                break;
            case "Death Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 8));
                break;
            case "Armored Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Def", 8));
                break;
            case "Darting Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Spd", 8));
                break;
            case "Warding Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Res", 8));
                break;
            case "Swift Sparrow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                break;
            case "Sturdy Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                break;
            case "Mirror Strike":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                break;
            case "Steady Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                break;
            case "Swift Strike":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                break;
            case "Bracing Blow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                break;
            case "Brazen Atk/Spd":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Bonus(_unit, "Spd", 10));
                break;
            case "Brazen Atk/Def":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Bonus(_unit, "Def", 10));
                break;
            case "Brazen Atk/Res":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Bonus(_unit, "Res", 10));
                break;
            case "Brazen Spd/Def":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Spd", 10));
                _effects.Add(new Bonus(_unit, "Def", 10));
                break;
            case "Brazen Spd/Res":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Spd", 10));
                _effects.Add(new Bonus(_unit, "Res", 10));
                break;
            case "Brazen Def/Res":
                _conditions.Add(new HpRange(_unit, "<=", 80, "%"));
                _effects.Add(new Bonus(_unit, "Def", 10));
                _effects.Add(new Bonus(_unit, "Res", 10));
                break;
            case "Fire Boost":
                _conditions.Add(new HpRange(_unit, ">=", _unit.Rival.Hp + 3));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                break;
            case "Wind Boost":
                _conditions.Add(new HpRange(_unit, ">=", _unit.Rival.Hp + 3));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                break;
            case "Earth Boost":
                _conditions.Add(new HpRange(_unit, ">=", _unit.Rival.Hp + 3));
                _effects.Add(new Bonus(_unit, "Def", 6));
                break;
            case "Water Boost":
                _conditions.Add(new HpRange(_unit, ">=", _unit.Rival.Hp + 3));
                _effects.Add(new Bonus(_unit, "Res", 6));
                break;
            case "Chaos Style":
                _conditions.Add(new StartsAttack(_unit));
                _conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(_unit, "Physical"),
                        new TypeOfAttack(_unit.Rival, "Magic")
                    }),
                    new HybridAndCondition(new List<Condition>()
                    {
                        new TypeOfAttack(_unit, "Magic"),
                        new TypeOfAttack(_unit.Rival, "Physical")
                    }),
                }));
                _effects.Add(new Bonus(_unit, "Spd", 3));
                break;
        }
    }

    private void SetPenalty()
    {
        switch (_name)
        {
            case "Blinding Flash":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Penalty(_unit.Rival, "Spd", -4));
                break;
            case "Not *Quite*":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Penalty(_unit.Rival, "Atk", -4));
                break;
            case "Stunning Smile":
                _conditions.Add(new IsMale(_unit.Rival));
                _effects.Add(new Penalty(_unit.Rival, "Spd", -8));
                break;
            case "Disarming Sigh":
                _conditions.Add(new IsMale(_unit.Rival));
                _effects.Add(new Penalty(_unit.Rival, "Atk", -8));
                break;
            case "Charmer":
                _conditions.Add(new LastRival(_unit));
                _effects.Add(new Penalty(_unit.Rival, "Atk", -3));
                _effects.Add(new Penalty(_unit.Rival, "Spd", -3));
                break;
            case "Luna":
                var effect1 = new PenaltyInFirstAttack(_unit.Rival, "Def", -_unit.Rival.Def / 2);
                var effect2 = new PenaltyInFirstAttack(_unit.Rival, "Res", -_unit.Rival.Res / 2);
                _effects.Add(effect1);
                _effects.Add(effect2);
                break;
            case "Belief in Love":
                _conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(_unit.Rival),
                    new HpRange(_unit.Rival, ">=", 100, mode: "%")
                }));
                _effects.Add(new Penalty(_unit.Rival, "Atk", -5));
                _effects.Add(new Penalty(_unit.Rival, "Def", -5));
                break;
        }
    }

    private void SetNeutralizeBonus()
    {
        switch (_name)
        {
            case "Beorc's Blessing":
                _effects.Add(new NeutralizeBonus(_unit.Rival));
                break;
        }
    }
    
    private void SetNeutralizePenalty()
    {
        switch (_name)
        {
            case "Agnea's Arrow":
                _effects.Add(new NeutralizePenalty(_unit));
                break;
        }
    }
    
    private void SetExtraDamage()
    {
        switch (_name)
        {
            case "Back at You":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new ExtraDamage(_unit, (_unit.InitialStats["Hp"] - _unit.Hp) / 2));
                break;
            case "Lunar Brace":
                _conditions.Add(new StartsAttack(_unit));
                _conditions.Add(new TypeOfAttack(_unit, "Physical"));
                _effects.Add(new ExtraDamage(_unit, "Def", 30));
                break;
            case "Bravery":
                _effects.Add(new ExtraDamage(_unit, 5));
                break;
        }
    }

    private void SetPercentageDamageReduction()
    {
        switch (_name)
        {
            case "Dragon Wall":
                _conditions.Add(new StatsComparison(_unit, "Res", ">", _unit.Rival, "Res"));
                _effects.Add(new PercentageDamageReduction(_unit, "Res"));
                break;
            case "Dodge":
                _conditions.Add(new StatsComparison(_unit, "Spd", ">", _unit.Rival, "Spd"));
                _effects.Add(new PercentageDamageReduction(_unit, "Spd"));
                break;
            case "Golden Lotus":
                _conditions.Add(new TypeOfAttack(_unit.Rival, "Physical"));
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 50));
                break;
        }
    }
    
    private void SetAbsolutDamageReduction()
    {
        switch (_name)
        {
            case "Gentility":
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
            case "Bow Guard":
                _conditions.Add(new UseWeapon(_unit.Rival, "Bow"));
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
            case "Arms Shield":
                _conditions.Add(new HasWeaponAdvantage(_unit));
                _effects.Add(new AbsolutDamageReduction(_unit, -7));
                break;
            case "Axe Guard":
                _conditions.Add(new UseWeapon(_unit.Rival, "Axe"));
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
            case "Magic Guard":
                _conditions.Add(new UseWeapon(_unit.Rival, "Magic"));
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
            case "Lance Guard":
                _conditions.Add(new UseWeapon(_unit.Rival, "Lance"));
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
            case "Sympathetic":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _conditions.Add(new HpRange(_unit, "<=", 50, "%"));
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                break;
        }
    }
    
    private void SetHybrid()
    {
        switch (_name)
        {
            case "Soulblade":
                _conditions.Add(new UseWeapon(_unit, "Sword"));
                var z = (_unit.Rival.Def + _unit.Rival.Res) / 2;
                var x = z - _unit.Rival.Def;
                var y = z - _unit.Rival.Res;
                if (x > 0) _effects.Add(new Bonus(_unit.Rival, "Def", x));
                else _effects.Add(new Penalty(_unit.Rival, "Def", x));
                if (y > 0) _effects.Add(new Bonus(_unit.Rival, "Res", y));
                else _effects.Add(new Penalty(_unit.Rival, "Res", y));
                break;
            case "Sandstorm":
                var w = _unit.Def / 2 + _unit.Def - _unit.Atk;
                Effect effect = (w > 0) ? new BonusInFollowUp(_unit, "Atk", w) : new PenaltyInFollowUp(_unit, "Atk", w);
                _effects.Add(effect);
                break;
            case "Sword Agility":
                _conditions.Add(new UseWeapon(_unit, "Sword"));
                _effects.Add(new Bonus(_unit, "Spd", 12));
                _effects.Add(new Penalty(_unit, "Atk", -6));
                break;
            case "Lance Power":
                _conditions.Add(new UseWeapon(_unit, "Lance"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Penalty(_unit, "Def", -10));
                break;
            case "Sword Power":
                _conditions.Add(new UseWeapon(_unit, "Sword"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Penalty(_unit, "Def", -10));
                break;
            case "Bow Focus":
                _conditions.Add(new UseWeapon(_unit, "Bow"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Penalty(_unit, "Res", -10));
                break;
            case "Lance Agility":
                _conditions.Add(new UseWeapon(_unit, "Lance"));
                _effects.Add(new Bonus(_unit, "Spd", 12));
                _effects.Add(new Penalty(_unit, "Atk", -6));
                break;
            case "Axe Power":
                _conditions.Add(new UseWeapon(_unit, "Axe"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Penalty(_unit, "Def", -10));
                break;
            case "Bow Agility":
                _conditions.Add(new UseWeapon(_unit, "Bow"));
                _effects.Add(new Bonus(_unit, "Spd", 12));
                _effects.Add(new Penalty(_unit, "Atk", -6));
                break;
            case "Sword Focus":
                _conditions.Add(new UseWeapon(_unit, "Sword"));
                _effects.Add(new Bonus(_unit, "Atk", 10));
                _effects.Add(new Penalty(_unit, "Res", -10));
                break;
            case "Close Def":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(_unit.Rival, "Sword"),
                    new UseWeapon(_unit.Rival, "Lance"),
                    new UseWeapon(_unit.Rival, "Axe")
                }));
                _effects.Add(new Bonus(_unit, "Def", 8));
                _effects.Add(new Bonus(_unit, "Res", 8));
                _effects.Add(new NeutralizeBonus(_unit.Rival));
                break;
            case "Distant Def":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new UseWeapon(_unit.Rival, "Magic"),
                    new UseWeapon(_unit.Rival, "Bow")
                }));
                _effects.Add(new Bonus(_unit, "Def", 8));
                _effects.Add(new Bonus(_unit, "Res", 8));
                _effects.Add(new NeutralizeBonus(_unit.Rival));
                break;
            case "Lull Atk/Spd":
                _effects.Add(new Penalty(_unit.Rival, "Atk", -3));
                _effects.Add(new Penalty(_unit.Rival, "Spd", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Atk"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Spd"));
                break;
            case "Lull Atk/Def":
                _effects.Add(new Penalty(_unit.Rival, "Atk", -3));
                _effects.Add(new Penalty(_unit.Rival, "Def", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Atk"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Def"));
                break;
            case "Lull Atk/Res":
                _effects.Add(new Penalty(_unit.Rival, "Atk", -3));
                _effects.Add(new Penalty(_unit.Rival, "Res", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Atk"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Res"));
                break;
            case "Lull Spd/Def":
                _effects.Add(new Penalty(_unit.Rival, "Spd", -3));
                _effects.Add(new Penalty(_unit.Rival, "Def", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Spd"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Def"));
                break;
            case "Lull Spd/Res":
                _effects.Add(new Penalty(_unit.Rival, "Spd", -3));
                _effects.Add(new Penalty(_unit.Rival, "Res", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Spd"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Res"));
                break;
            case "Lull Def/Res":
                _effects.Add(new Penalty(_unit.Rival, "Def", -3));
                _effects.Add(new Penalty(_unit.Rival, "Res", -3));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Def"));
                _effects.Add(new NeutralizeBonus(_unit.Rival, "Res"));
                break;
            case "Fort. Def/Res":
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new Penalty(_unit, "Atk", -2));
                break;
            case "Life and Death":
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Penalty(_unit, "Def", -5));
                _effects.Add(new Penalty(_unit, "Res", -5));
                break;
            case "Solid Ground":
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Penalty(_unit, "Res", -5));
                break;
            case "Still Water":
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new Penalty(_unit, "Def", -5));
                break;
            case "Dragonskin":
                _conditions.Add(new HybridOrCondition(new List<Condition>()
                {
                    new StartsAttack(_unit.Rival),
                    new HpRange(_unit.Rival, ">=", 75, "%")
                }));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new NeutralizeBonus(_unit.Rival));
                break;
            case "Light and Dark":
                _effects.Add(new Penalty(_unit.Rival, "Atk", -5));
                _effects.Add(new Penalty(_unit.Rival, "Spd", -5));
                _effects.Add(new Penalty(_unit.Rival, "Def", -5));
                _effects.Add(new Penalty(_unit.Rival, "Res", -5));
                _effects.Add(new NeutralizePenalty(_unit));
                _effects.Add(new NeutralizeBonus(_unit.Rival));
                break;
            case "Bushido":
                _effects.Add(new ExtraDamage(_unit, 7));
                _effects.Add(new ConditionalEffect(
                    new StatsComparison(_unit, "Spd", ">", _unit.Rival, "Spd"), 
                    new List<Effect>{ new PercentageDamageReduction(_unit, "Spd") }));
                break;
            case "Moon-Twin Wing":
                _effects.Add(new ConditionalEffect(
                    new HpRange(_unit, ">=", 25, "%"), 
                    new List<Effect>
                    {
                        new Penalty(_unit.Rival, "Atk", -5),
                        new Penalty(_unit.Rival, "Spd", -5)
                    }));
                _effects.Add(new ConditionalEffect(
                    new HybridAndCondition(new List<Condition>()
                    {
                        new HpRange(_unit, ">=", 25, "%"),
                        new StatsComparison(_unit, "Spd", ">", _unit.Rival, "Spd")
                    }),
                    new List<Effect>{ new PercentageDamageReduction(_unit, "Spd") }));
                break;
            case "Blue Skies":
                _effects.Add(new AbsolutDamageReduction(_unit, -5));
                _effects.Add(new ExtraDamage(_unit, 5));
                break;
            case "Aegis Shield":
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Bonus(_unit, "Res", 3));
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 50));
                break;
            case "Remote Sparrow":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 7));
                _effects.Add(new Bonus(_unit, "Spd", 7));
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 30));
                break;
            case "Remote Mirror":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 7));
                _effects.Add(new Bonus(_unit, "Res", 10));
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 30));
                break;
            case "Remote Sturdy":
                _conditions.Add(new StartsAttack(_unit));
                _effects.Add(new Bonus(_unit, "Atk", 7));
                _effects.Add(new Bonus(_unit, "Def", 10));
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 30));
                break;
            case "Fierce Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Atk", 8));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Darting Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Spd", 8));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Steady Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Def", 8));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Warding Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Res", 8));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Kestrel Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Sturdy Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Mirror Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Atk", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Steady Posture":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Swift Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Spd", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Bracing Stance":
                _conditions.Add(new StartsAttack(_unit.Rival));
                _effects.Add(new Bonus(_unit, "Def", 6));
                _effects.Add(new Bonus(_unit, "Res", 6));
                _effects.Add(new PercentageDamageReductionInFollowUp(_unit, 10));
                break;
            case "Poetic Justice":
                _effects.Add(new Penalty(_unit.Rival, "Spd", -4));
                _effects.Add(new ExtraDamage(_unit, "Atk", 15));
                break;
            case "Laguz Friend":
                _effects.Add(new PercentageDamageReduction(_unit, 50));
                _effects.Add(new NeutralizeBonus(_unit, "Def"));
                _effects.Add(new NeutralizeBonus(_unit, "Res"));
                _effects.Add(new Penalty(_unit, "Def", -(_unit.Def / 2)));
                _effects.Add(new Penalty(_unit, "Res", -(_unit.Res / 2)));
                break;
            case "Chivalry":
                _conditions.Add(new StartsAttack(_unit));
                _conditions.Add(new HpRange(_unit.Rival, ">=", 100, "%"));
                _effects.Add(new ExtraDamage(_unit, 2));
                _effects.Add(new AbsolutDamageReduction(_unit, -2));
                break;
            case "Dragon's Wrath":
                _effects.Add(new PercentageDamageReductionInFirstAttack(_unit, 25));
                _effects.Add(new ConditionalEffect(
                    new StatsComparison(_unit, "Atk", ">", _unit.Rival, "Res"), 
                    new List<Effect>{ new ExtraDamageInFirstAttack(_unit, "Atk", "Res", 25) }));
                break;
            case "Prescience":
                _effects.Add(new Penalty(_unit.Rival, "Atk", -5));
                _effects.Add(new Penalty(_unit.Rival, "Res", -5));
                _effects.Add(new ConditionalEffect(
                    new HybridOrCondition(new List<Condition>()
                    { 
                        new StartsAttack(_unit), 
                        new UseWeapon(_unit.Rival, "Magic"), 
                        new UseWeapon(_unit.Rival, "Bow")
                    }), 
                    new List<Effect>{ new PercentageDamageReductionInFirstAttack(_unit, 30) }));
                
                break;
            case "Extra Chivalry":
                _effects.Add(new ConditionalEffect(
                    new HpRange(_unit.Rival, ">=", 50, "%"),
                    new List<Effect>
                    {
                        new Penalty(_unit.Rival, "Atk", -5),
                        new Penalty(_unit.Rival, "Spd", -5),
                        new Penalty(_unit.Rival, "Def", -5)
                        
                    }));
                _effects.Add(new PercentageDamageReduction(_unit, (_unit.Rival.Hp * 50) / _unit.Rival.InitialStats["Hp"])); 
                break;
            case "Guard Bearing":
                _effects.Add(new Penalty(_unit.Rival, "Spd", -4));
                _effects.Add(new Penalty(_unit.Rival, "Def", -4));
                _effects.Add(new ConditionalElseEffect(
                    new HybridOrCondition(new List<Condition>
                    {
                        new HybridAndCondition(new List<Condition>
                        {
                            new InFirstCombat(_unit, "Attacker")
                        }),
                        new HybridAndCondition(new List<Condition>()
                        {
                            new InFirstCombat(_unit, "Defender")
                        }),
                    }),
                    new List<Effect>{ new PercentageDamageReduction(_unit, 60) },
                    new List<Effect>{ new PercentageDamageReduction(_unit, 30) }
                    ));
                break;
            case "Divine Recreation":
                _effects.Add(new ConditionalEffect(
                    new HpRange(_unit.Rival, ">=", 50, "%"),
                    new List<Effect>
                    {
                        new Penalty(_unit.Rival, "Atk", -4),
                        new Penalty(_unit.Rival, "Spd", -4),
                        new Penalty(_unit.Rival, "Def", -4),
                        new Penalty(_unit.Rival, "Res", -4)
                    }
                ));
                _effects.Add(new ConditionalEffect(
                    new HpRange(_unit.Rival, ">=", 50, "%"),
                    new List<Effect> { new PercentageDamageReductionInFirstAttack(_unit, 30) }
                ));
                _effects.Add(new ConditionalEffect(
                    new HpRange(_unit.Rival, ">=", 50, "%"),
                    _unit.IsAttacker ? 
                        new List<Effect> { new ExtraDamageInFollowUp(_unit, "Dmg") } :
                        new List<Effect> { new ExtraDamageInFirstAttack(_unit, "Dmg") }
                ));
                break;
        }
    }

    private void AddSkillToSkillsManager()
    {
        foreach (var effect in _effects)
            _skillsManager.Add(_conditions, effect);
    }
}