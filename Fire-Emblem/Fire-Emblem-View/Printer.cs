using Fire_Emblem;

namespace Fire_Emblem_View;

public class Printer
{
    private View _view;
    public Printer(View view)
        => _view = view;
    
    public void PrintMessages(Unit unit)
    {
        PrintAlterStatMessages(unit, "Bonus", "+");
        PrintAlterStatMessages(unit, "BonusInFirstAttack", "+" , " en su primer ataque");
        PrintAlterStatMessages(unit, "BonusInFollowUp", "+", " en su Follow-Up");
        PrintAlterStatMessages(unit, "Penalty", "");
        PrintAlterStatMessages(unit, "PenaltyInFirstAttack", "", " en su primer ataque");
        PrintAlterStatMessages(unit, "PenaltyInFollowUp", "", " en su Follow-Up");
        PrintNeutralizeEffects(unit, "Bonus");
        PrintNeutralizeEffects(unit, "Penalty");
        PrintExtraDamageMessages(unit, "ExtraDamage", "cada ataque");
        PrintExtraDamageMessages(unit, "ExtraDamageInFirstAttack", "su primer ataque");
        PrintExtraDamageMessages(unit, "ExtraDamageInFollowUp", "su Follow-Up");
        PrintPercentageDamageReductionMessages(
            unit, "PercentageDamageReduction", "de los ataques");
        PrintPercentageDamageReductionMessages(unit, 
            "PercentageDamageReductionInFirstAttack", "del primer ataque");
        PrintPercentageDamageReductionMessages(unit, 
            "PercentageDamageReductionInFollowUp", "del Follow-Up");
        PrintAbsolutDamageReductionMessages(unit);
        PrintHealingMessages(unit);
        PrintCounterAttackDenialMessages(unit);
        PrintDenialOfCounterAttackDenialMessages(unit);
        PrintFollowUpGuaranteeMessages(unit);
        PrintDenialOfFollowUpMessages(unit);
        PrintDenialOfFollowUpDenialMessages(unit);
        PrintDenialOfFollowUpGuaranteeMessages(unit);
    }

    private void PrintAlterStatMessages(Unit unit, string effect, string sign, string extraMessage = "")
    {
        foreach (var (stat, statInfo) in unit.StatsManager.StatsDictionary[effect])
        {
            var value = (int)statInfo[0];
            if (value != 0)
                _view.WriteLine($"{unit.Name} obtiene {stat}{sign}{value}{extraMessage}");
        }
    }

    private void PrintNeutralizeEffects(Unit unit, string effect)
    {
        foreach (var stat in Stats.AllStats)
            if ((bool)unit.StatsManager.StatsDictionary[effect][stat][1] && stat != Stats.Hp)
                _view.WriteLine($"Los {effect.ToLower()} de {stat} de {unit.Name} fueron neutralizados");
    }
    
    private void PrintExtraDamageMessages(Unit unit, string effect, string extraMessage)
    {
        var value = unit.DamageManager.DamageDictionary[effect];
        if (value != 0)
            _view.WriteLine($"{unit.Name} realizará +{value} daño extra en {extraMessage}");
    }
    
    private void PrintPercentageDamageReductionMessages(Unit unit, string effect, string extraMessage)
    {
        var value = unit.DamageManager.DamageDictionary[effect];
        if (value != 0)
            _view.WriteLine($"{unit.Name} reducirá el daño {extraMessage} del rival en un {value}%");
    }
    
    private void PrintAbsolutDamageReductionMessages(Unit unit)
    {
        var value = unit.DamageManager.DamageDictionary["AbsolutDamageReduction"];
        if (value != 0)
            _view.WriteLine($"{unit.Name} recibirá {value} daño en cada ataque");
    }

    public void PrintHealingBeforeCombatMessages(Unit unit)
    {
        var value = (int)unit.StatsManager.StatsDictionary["HealingBeforeCombat"][Stats.Hp][0];
        if (value != 0)
        {
            unit.Hp = Math.Max(1, unit.Hp + value);
            _view.WriteLine(
                $"{unit.Name} recibe {-value} de daño antes de iniciar el combate y queda con {unit.Hp} HP");
        }
    }

    private void PrintHealingMessages(Unit unit)
    {
        foreach (var (stat, statInfo) in unit.StatsManager.StatsDictionary["Healing"])
        {
            var value = (int)statInfo[0];
            if (value != 0)
                _view.WriteLine(
                    $"{unit.Name} recuperará HP igual al {value}% del daño realizado en cada ataque");
        }
    }

    private void PrintCounterAttackDenialMessages(Unit unit)
    {
        if (unit.CounterAttackDenial)
            _view.WriteLine($"{unit.Name} no podrá contraatacar");
    }
    
    private void PrintDenialOfCounterAttackDenialMessages(Unit unit)
    {
        if (unit.DenialOfCounterAttackDenial)
            _view.WriteLine($"{unit.Name} neutraliza los efectos que previenen sus contraataques");
    }

    private void PrintFollowUpGuaranteeMessages(Unit unit)
    {
        var value = (int)unit.StatsManager.StatsDictionary["FollowUpGuarantee"][Stats.Hp][0];   
        if (unit.FollowUpGuarantee > 0)
            _view.WriteLine($"{unit.Name} tiene {value} efecto(s) que garantiza(n) su follow up activo(s)");
    }

    private void PrintDenialOfFollowUpMessages(Unit unit)
    {
        var value = (int)unit.StatsManager.StatsDictionary["DenialOfFollowUp"][Stats.Hp][0];
        if (unit.DenialOfFollowUp > 0)
            _view.WriteLine($"{unit.Name} tiene {value} efecto(s) que neutraliza(n) su follow up activo(s)");
    }

    private void PrintDenialOfFollowUpGuaranteeMessages(Unit unit)
    {
        if (unit.DenialOfFollowUpGuarantee)
            _view.WriteLine($"{unit.Name} es inmune a los efectos que garantizan su follow up"); 
    }
    
    private void PrintDenialOfFollowUpDenialMessages(Unit unit)
    {
        if (unit.DenialOfFollowUpDenial)
            _view.WriteLine($"{unit.Name} es inmune a los efectos que neutralizan su follow up"); 
    }

    public void PrintAfterCombatMessages(Unit unit)
    {
        foreach (var (stat, statInfo) in unit.StatsManager.StatsDictionary["HealingAfterCombat"])
        {
            var value = (int)statInfo[0];
            var hasAttackedCondition = (bool)statInfo[1];
            if (!hasAttackedCondition || (hasAttackedCondition && unit.HasAttacked))
            {
                if (value > 0 && unit.Hp > 0)
                    _view.WriteLine($"{unit.Name} recupera {value} HP despues del combate");
                else if (value < 0 && unit.Hp > 0)
                    _view.WriteLine($"{unit.Name} recibe {-value} de daño despues del combate");
            }
        }
    }

    public void PrintHpHealingMessages(Unit unit)
    {
        var percentage = (int)unit.StatsManager.StatsDictionary["Healing"][Stats.Hp][0];
        var value = unit.Damage * percentage / 100;
        if (value > 0)
        {
            unit.Hp = Math.Min(unit.InitialStats[Stats.Hp], unit.Hp + value);
            _view.WriteLine($"{unit.Name} recupera {value} HP luego de atacar y queda con {unit.Hp} HP.");
        }
    }

    public void PrintChoiceTeamMessage()
        => _view.WriteLine("Elige un archivo para cargar los equipos");
    
    public void PrintFileOption(int index, string[] files)
        => _view.WriteLine($"{index}: {Path.GetFileName(files[index])}");
    
    public void PrintInvalidTeamMessage()
        => _view.WriteLine($"Archivo de equipos no válido");
    
    public void PrintChoiceUnitMessage(Player player)
        => _view.WriteLine($"Player {player.Id} selecciona una opción");
    
    public void PrintUnitOption(Player player, int index)
        => _view.WriteLine($"{index}: {player.Team.GetUnit(index).Name}");
    
    public void PrintStartOfRound(int round, Unit unit, Player player)
        => _view.WriteLine($"Round {round}: {unit.Name} (Player {player.Id}) comienza");
    
    public void PrintAdvantageMessage(Unit unit)
        => _view.WriteLine($"{unit.Name} ({unit.Weapon}) tiene ventaja " +
                           $"con respecto a {unit.Rival.Name} ({unit.Rival.Weapon})");
    
    public void PrintNoAdvantageMessage()
        => _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
    
    public void PrintAttackMessage(Unit unit, int damage)
        => _view.WriteLine($"{unit.Name} ataca a {unit.Rival.Name} con {damage} de daño");
    
    public void PrintNoFollowUpWithCounterDenialMessage(Unit unit)
        => _view.WriteLine($"{unit.Name} no puede hacer un follow up");
    
    public void PrintNoFollowUpMessage()
        => _view.WriteLine("Ninguna unidad puede hacer un follow up");
    
    public void PrintEndOfRoundInfo(Player currentAttacker, Player currentDefender)
        => _view.WriteLine($"{currentAttacker.Unit.Name} ({currentAttacker.Unit.Hp}) :" +
                          $" {currentDefender.Unit.Name} ({currentDefender.Unit.Hp})");
    
    public void PrintEndOfGameMessage(FireEmblemException exception)
        => _view.WriteLine(exception.Message == "Player 1 sin unidades" ? "Player 2 ganó" : "Player 1 ganó");

    public int Read()
    {
        return Utils.Int(_view.ReadLine());
    }
}