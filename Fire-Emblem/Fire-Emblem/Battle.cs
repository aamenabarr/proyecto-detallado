using Fire_Emblem_View;

namespace Fire_Emblem;

public class Battle
{
    private readonly View _view;
    private int _round;
    private Player _currentAttacker;
    private Player _currentDefender;
    private Unit _unit;
    private Unit _rival;
    private EffectManager _effectManager;

    public Battle(Player player1, Player player2, View view)
    {
        _currentAttacker = player1;
        _currentDefender = player2;
        _view = view;
        _effectManager = new EffectManager(_view);
        _currentAttacker.ApplyBaseEffects(_effectManager);
        _currentDefender.ApplyBaseEffects(_effectManager);
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
        ChoiceUnits();
        _view.WriteLine($"Round {_round}: {_unit.Name} (Player {_currentAttacker.Id}) comienza");
        WeaponsTriangle();
        ApplySkills();
        StartAttacks();
        FollowUp();
        HandleEndOfRound();
    }

    private void HandleEndOfGame(Exception exception)
    {
        PrintRoundInfo();
        _view.WriteLine(exception.Message == "Player 1 sin unidades" ? "Player 2 ganó" : "Player 1 ganó");
    }

    private void HandleEndOfRound()
    {
        PrintRoundInfo();
        SaveRoundInfo();
        ResetStats();
        SwitchPlayers();
    }

    private void SwitchPlayers()
    {
        (_currentAttacker, _currentDefender) = (_currentDefender, _currentAttacker);
    }
    
    private void SwitchUnits()
    {
        (_unit, _rival) = (_rival, _unit);
    }

    private void ChoiceUnits()
    {
        _currentAttacker.ChoiceUnit();
        _currentDefender.ChoiceUnit();
        SetUnits();
        SetRoundInfo();
    }
    
    private void SetUnits()
    {
        _unit = _currentAttacker.Unit;
        _rival = _currentDefender.Unit;
    }

    private void StartAttacks()
    {
        Attack(_currentDefender);
        Attack(_currentAttacker);
    }

    private void Attack(Player defender)
    {
        _unit.Attack(_rival);
        defender.UpdateTeam();
        SwitchUnits();
    }

    private void ApplySkills()
    {
        _unit.SetSkillsEffects();
        _rival.SetSkillsEffects();
        _unit.ApplySkills();
        _rival.ApplySkills();
        _effectManager.ApplyEffects();
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

    private void FollowUp()
    {
        ResetFirstAttackEffects();
        _effectManager.ApplyFollowUpEffects();
        if (_unit.CanDoFollowUp(_rival))
            Attack(_currentDefender);
        else if (_rival.CanDoFollowUp(_unit))
        {
            SwitchUnits();
            Attack(_currentAttacker);
        }
        else
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
    }
    
    private void PrintRoundInfo()
    {
        _view.WriteLine($"{_currentAttacker.Unit.Name} ({_currentAttacker.Unit.Hp}) :" +
                        $" {_currentDefender.Unit.Name} ({_currentDefender.Unit.Hp})");
    }
    
    private void SaveRoundInfo()
    {
        _unit.LastRival = _rival;
        _rival.LastRival = _unit;
    }

    private void SetRoundInfo()
    {
        _unit.IsAttacker = true;
        _unit.Rival = _rival;
        _rival.Rival = _unit;
    }

    private void ResetStats()
    {
        _unit.IsAttacker = false;
        _effectManager.ResetFollowUpEffects();
        _unit.ResetStats();
        _rival.ResetStats();
        _effectManager.Effects.Clear();
    }

    private void ResetFirstAttackEffects()
    {
        _unit.ResetFirstAttackEffects();
        _rival.ResetFirstAttackEffects();
    }
}