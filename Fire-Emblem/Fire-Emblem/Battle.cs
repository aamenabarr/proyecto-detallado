using Fire_Emblem_View;

namespace Fire_Emblem;

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
            catch (EndOfRound)
            {
                HandleEndOfRound();
            }
            catch (EndOfGame exception)
            {
                HandleEndOfGame(exception);
                return;
            }
        }
    }
    
    private void Round()
    {
        ChooseUnits();
        PrintStartOfRound();
        WeaponsTriangle();
        ApplySkills();
        StartAttacks();
        FollowUp();
        HandleEndOfRound();
    }
    
    private void ChooseUnits()
    {
        _currentAttacker.ChooseUnit(_view);
        _currentDefender.ChooseUnit(_view);
        SetUnits();
        SetRoundInfo();
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
        if (_unit.HasAdvantage(_rival))
        {
            _unit.Wtb = 1.2;
            _rival.Wtb = 0.8;
        }
        else if (_rival.HasAdvantage(_unit))
        {
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
        _unit.CreateSkills(_skillsManager);
        _rival.CreateSkills(_skillsManager);
        _skillsManager.ApplySkills();
        _unit.PrintSkillsMessages();
        _rival.PrintSkillsMessages();
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
        _unit.Attack(_rival);
        defender.UpdateTeam();
        SwitchUnits();
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
        if (_unit.CanDoFollowUp(_rival))
            Attack(_currentDefender);
        else if (_rival.CanDoFollowUp(_unit))
        {
            SwitchUnits();
            Attack(_currentAttacker);
        }
        else
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        ResetStats();
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
        PrintEndOfRoundInfo();
        _view.WriteLine(exception.Message == "Player 1 sin unidades" ? "Player 2 ganó" : "Player 1 ganó");
    }
}