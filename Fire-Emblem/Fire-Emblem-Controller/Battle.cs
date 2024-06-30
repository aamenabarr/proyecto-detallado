using Fire_Emblem_View;
using Fire_Emblem;

namespace Fire_Emblem_Controller;

public class Battle
{
    private int _round;
    private View _view;
    private Player _currentAttacker;
    private Player _currentDefender;
    private Unit _unit;
    private Unit _rival;
    private SkillsManager _skillsManager = new();

    public Battle(Player player1, Player player2, View view)
    {
        _currentAttacker = player1;
        _currentDefender = player2;
        _view = view;
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
        _view.WriteLine($"Player {player.Id} selecciona una opción");
        PrintUnitOptions(player);
        var input = Utils.Int(_view.ReadLine());
        player.Unit = player.Team.GetUnit(input);
    }

    private void PrintUnitOptions(Player player)
    {
        for (var i = 0; i < player.Team.Length(); i++)
            _view.WriteLine($"{i}: {player.Team.GetUnit(i).Name}");
    }
    
    private void SetUnits()
    {
        _unit = _currentAttacker.Unit;
        _rival = _currentDefender.Unit;
    }
    
    private void SetRoundInfo()
    {
        _unit.IsAttacker = true;
        _rival.IsAttacker = false;
        _unit.HasAttacked = false;
        _rival.HasAttacked = false;
        _unit.CounterAttackDenial = false;
        _rival.CounterAttackDenial = false;
        _unit.DenialOfCounterAttackDenial = false;
        _rival.DenialOfCounterAttackDenial = false;
        _unit.FollowUpGuarantee = 0;
        _rival.FollowUpGuarantee = 0;
        _unit.DenialOfFollowUp = 0;
        _rival.DenialOfFollowUp = 0;
        _unit.DenialOfFollowUpGuarantee = false;
        _rival.DenialOfFollowUpGuarantee = false;
        _unit.DenialOfFollowUpDenial = false;
        _rival.DenialOfFollowUpDenial = false;
        _unit.ReductionOfPercentageDamage = 1;
        _rival.ReductionOfPercentageDamage = 1;
        _unit.SetFirstCombatInfo();
        _rival.SetFirstCombatInfo();
        _unit.Rival = _rival;
        _rival.Rival = _unit;
    }

    private void PrintStartOfRound()
    {
        _view.WriteLine($"Round {_round}: {_unit.Name} (Player {_currentAttacker.Id}) comienza");
    }
    
    private void WeaponsTriangle()
    {
        if (AttackUtils.HasAdvantage(_unit, _rival))
        {
            _view.WriteLine($"{_unit.Name} ({_unit.Weapon}) tiene ventaja " +
                           $"con respecto a {_rival.Name} ({_rival.Weapon})");
            _unit.Wtb = 1.2;
            _rival.Wtb = 0.8;
        }
        else if (AttackUtils.HasAdvantage(_rival, _unit))
        {
            _view.WriteLine($"{_rival.Name} ({_rival.Weapon}) tiene ventaja " +
                            $"con respecto a {_unit.Name} ({_unit.Weapon})");
            _unit.Wtb = 0.8;
            _rival.Wtb = 1.2;
        }
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
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
        EffectsPrinter.PrintMessages(_view, _unit);
        EffectsPrinter.PrintMessages(_view, _rival);
        EffectsPrinter.PrintHealingBeforeCombatMessages(_view, _unit);
        EffectsPrinter.PrintHealingBeforeCombatMessages(_view, _rival);
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
            _view.WriteLine($"{_unit.Name} ataca a {_rival.Name} con {damage} de daño");
            defender.UpdateTeam();
            PrintHpHealing();
        }
        SwitchUnits();
    }

    private void PrintHpHealing(bool endOfRound = false)
    {
        if ((endOfRound && _rival.Hp == 0) || !endOfRound)
            EffectsPrinter.PrintHpHealingMessages(_view, _unit);
    }
    
    private void SwitchUnits()
    {
        (_unit, _rival) = (_rival, _unit);
    }
    
    private void FollowUp()
    {
        SetFollowUpInfo();
        AlterStats();
        AlterDamage();
        bool didAttack = false;
        if (AttackUtils.CanDoFollowUp(_unit, _rival) && !_unit.CounterAttackDenial && 
            (_unit.DenialOfFollowUp == 0 && _unit.FollowUpGuarantee == 0 || 
             (_unit.DenialOfFollowUp == _unit.FollowUpGuarantee && _unit.DenialOfFollowUp > 0 
                                                               && _unit.FollowUpGuarantee > 0 
                                                               && !_unit.DenialOfFollowUpGuarantee) || 
            (_unit.DenialOfFollowUp > _unit.FollowUpGuarantee && _unit.DenialOfFollowUpDenial && _unit.FollowUpGuarantee == 0) ||
            (_unit.FollowUpGuarantee > _unit.DenialOfFollowUp && _unit.DenialOfFollowUpGuarantee && _unit.DenialOfFollowUp == 0))
            || (_unit.FollowUpGuarantee > _unit.DenialOfFollowUp && !_unit.DenialOfFollowUpGuarantee) ||
            (_unit.FollowUpGuarantee <= _unit.DenialOfFollowUp && !_unit.DenialOfFollowUpGuarantee && _unit.DenialOfFollowUpDenial && 
             _unit.FollowUpGuarantee > 0))
        {
            Attack(_currentDefender);
            SwitchUnits();
            didAttack = true;
        }
        if (AttackUtils.CanDoFollowUp(_rival, _unit) && !_rival.CounterAttackDenial && 
            (_rival.DenialOfFollowUp == 0 && _rival.FollowUpGuarantee == 0 || 
             (_rival.DenialOfFollowUp == _rival.FollowUpGuarantee && _rival.DenialOfFollowUp > 0 
                                                                && _rival.FollowUpGuarantee > 0 
                                                                && !_rival.DenialOfFollowUpGuarantee) || 
             (_rival.DenialOfFollowUp > _rival.FollowUpGuarantee && _rival.DenialOfFollowUpDenial && _rival.FollowUpGuarantee == 0) ||
             (_rival.FollowUpGuarantee > _rival.DenialOfFollowUp && _rival.DenialOfFollowUpGuarantee && _rival.DenialOfFollowUp == 0))
            || (_rival.FollowUpGuarantee > _rival.DenialOfFollowUp && !_rival.DenialOfFollowUpGuarantee) ||
            (_rival.FollowUpGuarantee <= _rival.DenialOfFollowUp && !_rival.DenialOfFollowUpGuarantee && _rival.DenialOfFollowUpDenial && 
             _rival.FollowUpGuarantee > 0))
        {
            SwitchUnits();
            Attack(_currentAttacker);
            didAttack = true;
        }
        if (!didAttack)
        {
            if (_unit.CounterAttackDenial && !_rival.CounterAttackDenial)
                _view.WriteLine($"{_rival.Name} no puede hacer un follow up");
            else if (!_unit.CounterAttackDenial && _rival.CounterAttackDenial)
                _view.WriteLine($"{_unit.Name} no puede hacer un follow up");
            else
                _view.WriteLine("Ninguna unidad puede hacer un follow up");
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
        EffectsPrinter.PrintAfterCombatMessages(_view, _currentAttacker.Unit);
        EffectsPrinter.PrintAfterCombatMessages(_view, _currentDefender.Unit);
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
    {
        _view.WriteLine($"{_currentAttacker.Unit.Name} ({_currentAttacker.Unit.Hp}) :" +
                        $" {_currentDefender.Unit.Name} ({_currentDefender.Unit.Hp})");
    }
    
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
    {
        (_currentAttacker, _currentDefender) = (_currentDefender, _currentAttacker);
    }
    
    private void HandleEndOfGame(Exception exception)
    {
        PrintHpHealing(true);
        ApplyAfterCombatEffects();
        PrintEndOfRoundInfo();
        _view.WriteLine(exception.Message == "Player 1 sin unidades" ? "Player 2 ganó" : "Player 1 ganó");
    }
}