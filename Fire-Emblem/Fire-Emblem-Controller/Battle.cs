using Fire_Emblem_View;
using Fire_Emblem;

namespace Fire_Emblem_Controller;

public class Battle
{
    private int _round;
    private Printer _printer;
    private Player _currentAttacker;
    private Player _currentDefender;
    private Unit _unit;
    private Unit _rival;
    private SkillsManager _skillsManager = new();

    public Battle(Player player1, Player player2, Printer printer)
    {
        _currentAttacker = player1;
        _currentDefender = player2;
        _printer = printer;
    }

    public void Start()
    {
        for (_round = 1;; _round++)
        {
            try
            {
                Round();
            }
            catch (FireEmblemException exception)
            {
                if (exception is EndOfRound)
                    HandleEndOfRound();
                else if (exception is EndOfGame)
                {
                    HandleEndOfGame(exception);
                    return;
                }
            }
        }
    }
    
    private void Round()
    {
        ChooseUnits();
        SetUnits();
        SetRoundInfo();
        PrintStartOfRound();
        WeaponsTriangle();
        ApplySkills();
        StartAttacks();
        FollowUp();
        HandleEndOfRound();
    }
    
    private void ChooseUnits()
    {
        ChooseUnit(_currentAttacker);
        ChooseUnit(_currentDefender);
    }
    
    private void ChooseUnit(Player player)
    {
        _printer.PrintChoiceUnitMessage(player);
        PrintUnitOptions(player);
        var input = _printer.Read();
        player.Unit = player.Team.GetUnit(input);
    }

    private void PrintUnitOptions(Player player)
    {
        for (var i = 0; i < player.Team.Length(); i++)
            _printer.PrintUnitOption(player, i);
    }
    
    private void SetUnits()
    {
        _unit = _currentAttacker.Unit;
        _rival = _currentDefender.Unit;
    }
    
    private void SetRoundInfo()
    {
        SetRivalsInfo();
        ResetUnitInfo(_unit);
        ResetUnitInfo(_rival);
    }
    
    private void SetRivalsInfo()
    {
        _unit.IsAttacker = true;
        _rival.IsAttacker = false;
        _unit.Rival = _rival;
        _rival.Rival = _unit;
    }
    
    private void ResetUnitInfo(Unit unit)
    {
        unit.ResetInfo();
        unit.SetFirstCombatInfo();
    }

    private void PrintStartOfRound()
        => _printer.PrintStartOfRound(_round, _unit, _currentAttacker);
    
    private void WeaponsTriangle()
    {
        if (AttackUtils.HasAdvantage(_unit, _rival))
        {
            _printer.PrintAdvantageMessage(_unit);
            _unit.Wtb = 1.2;
            _rival.Wtb = 0.8;
        }
        else if (AttackUtils.HasAdvantage(_rival, _unit))
        {
            _printer.PrintAdvantageMessage(_rival);
            _unit.Wtb = 0.8;
            _rival.Wtb = 1.2;
        }
        else
        {
            _printer.PrintNoAdvantageMessage();
            _unit.Wtb = 1;
            _rival.Wtb = 1;
        }
    }
    
    private void ApplySkills()
    {
        CreateSkills(_unit);
        CreateSkills(_rival);
        _skillsManager.ApplySkills();
        PrintEffectsMessages();
    }
    
    private void CreateSkills(Unit unit)
    {
        foreach (var skillName in unit.Skills)
        {
            var skill = new Skill(skillName, unit);
            AddSkillToSkillsManager(skill);
        }
    }
    
    private void AddSkillToSkillsManager(Skill skill)
    {
        foreach (var effect in skill.Effects.Get())
            _skillsManager.Add(skill.Conditions.Get(), effect);
    }

    private void PrintEffectsMessages()
    {
        _printer.PrintMessages(_unit);
        _printer.PrintMessages(_rival);
        _printer.PrintHealingBeforeCombatMessages(_unit);
        _printer.PrintHealingBeforeCombatMessages(_rival);
    }
    
    private void StartAttacks()
    {
        SetFirstAttackInfo();
        AlterStats();
        AlterDamage();
        Attack(_currentDefender);
        Attack(_currentAttacker);
        ResetStats();
    }
    
    private void SetFirstAttackInfo()
    {
        _unit.InFirstAttack = true;
        _rival.InFirstAttack = true;
        _unit.InFollowUp = false;
        _rival.InFollowUp = false;
    }

    private void AlterStats()
    {
        _unit.AlterStats();
        _rival.AlterStats();
    }
    
    private void AlterDamage()
    {
        _unit.AlterDamage();
        _rival.AlterDamage();
    }
    
    private void ResetStats()
    {
        _unit.InFirstCombat = false;
        _rival.InFirstCombat = false;
        _unit.ResetStats();
        _rival.ResetStats();
    }

    private void Attack(Player defender)
    {
        if (!_unit.CounterAttackDenial)
        {
            _unit.HasAttacked = true;
            var damage = AttackUtils.Attack(_unit, _rival);
            _printer.PrintAttackMessage(_unit, damage);
            defender.UpdateTeam();
            PrintHpHealing();
        }
        SwitchUnits();
    }

    private void PrintHpHealing(bool endOfRound = false)
    {
        if ((endOfRound && _rival.Hp == 0) || !endOfRound)
            _printer.PrintHpHealingMessages(_unit);
    }
    
    private void SwitchUnits()
        => (_unit, _rival) = (_rival, _unit);
    
    private void FollowUp()
    {
        SetFollowUpInfo();
        AlterStats();
        AlterDamage();
        bool didAttack = false;
        if (AttackUtils.CanDoFollowUp(_unit, _rival))
        {
            Attack(_currentDefender);
            SwitchUnits();
            didAttack = true;
        }
        if (AttackUtils.CanDoFollowUp(_rival, _unit))
        {
            SwitchUnits();
            Attack(_currentAttacker);
            didAttack = true;
        }
        if (!didAttack)
        {
            if (_unit.CounterAttackDenial && !_rival.CounterAttackDenial)
                _printer.PrintNoFollowUpWithCounterDenialMessage(_rival);
            else if (!_unit.CounterAttackDenial && _rival.CounterAttackDenial)
                _printer.PrintNoFollowUpWithCounterDenialMessage(_unit);
            else
                _printer.PrintNoFollowUpMessage();
        }
        ResetStats();
    }


    private void ApplyAfterCombatEffects()
    {
        PrintAfterCombatMessages();
        _unit.StatsManager.AlterUnitStats("HealingAfterCombat", true);
        _rival.StatsManager.AlterUnitStats("HealingAfterCombat",true);
    }

    private void PrintAfterCombatMessages()
    {
        _printer.PrintAfterCombatMessages(_currentAttacker.Unit);
        _printer.PrintAfterCombatMessages(_currentDefender.Unit);
    }

    private void SetFollowUpInfo()
    {
        _unit.InFirstAttack = false;
        _rival.InFirstAttack = false;
        _unit.InFollowUp = true;
        _rival.InFollowUp = true;
    }
    
    private void HandleEndOfRound()
    {
        PrintHpHealing(true);
        ApplyAfterCombatEffects();
        PrintEndOfRoundInfo();
        SaveRoundInfo();
        ResetStats();
        ResetManagers();
        SwitchPlayers();
    }
    
    private void PrintEndOfRoundInfo()
        => _printer.PrintEndOfRoundInfo(_currentAttacker, _currentDefender);
    
    private void SaveRoundInfo()
    {
        _unit.LastRival = _rival;
        _rival.LastRival = _unit;
    }

    private void ResetManagers()
    {
        _skillsManager.Reset();
        _unit.ResetManagers();
        _rival.ResetManagers();
    }

    private void SwitchPlayers()
        => (_currentAttacker, _currentDefender) = (_currentDefender, _currentAttacker);
    
    private void HandleEndOfGame(FireEmblemException exception)
    {
        PrintHpHealing(true);
        ApplyAfterCombatEffects();
        PrintEndOfRoundInfo();
        _printer.PrintEndOfGameMessage(exception);
    }
}