using Fire_Emblem;
using Fire_Emblem_View;

namespace Fire_Emblem_Controller;

public static class EffectsPrinter
{
    public static void PrintMessages(View view, Unit unit)
    {
        PrintAlterStatMessages(view, unit, "Bonus", "+");
        PrintAlterStatMessages(view, unit, "BonusInFirstAttack", "+" , " en su primer ataque");
        PrintAlterStatMessages(view, unit, "BonusInFollowUp", "+", " en su Follow-Up");
        PrintAlterStatMessages(view, unit, "Penalty", "");
        PrintAlterStatMessages(view, unit, "PenaltyInFirstAttack", "", " en su primer ataque");
        PrintAlterStatMessages(view, unit, "PenaltyInFollowUp", "", " en su Follow-Up");
        PrintNeutralizeEffects(view, unit, "Bonus");
        PrintNeutralizeEffects(view, unit, "Penalty");
        PrintExtraDamageMessages(view, unit, "ExtraDamage", "cada ataque");
        PrintExtraDamageMessages(view, unit, "ExtraDamageInFirstAttack", "su primer ataque");
        PrintExtraDamageMessages(view, unit, "ExtraDamageInFollowUp", "su Follow-Up");
        PrintPercentageDamageReductionMessages(view, unit, "PercentageDamageReduction", "de los ataques");
        PrintPercentageDamageReductionMessages(view, unit, 
            "PercentageDamageReductionInFirstAttack", "del primer ataque");
        PrintPercentageDamageReductionMessages(view, unit, 
            "PercentageDamageReductionInFollowUp", "del Follow-Up");
        PrintAbsolutDamageReductionMessages(view, unit);
    }

    private static void PrintAlterStatMessages(View view, Unit unit, string effect, string sign, string extraMessage = "")
    {
        foreach (var (stat, statInfo) in unit.StatsManager.StatsDictionary[effect])
        {
            var value = (int)statInfo[0];
            if (value != 0)
                view.WriteLine($"{unit.Name} obtiene {stat}{sign}{value}{extraMessage}");
        }
    }

    private static void PrintNeutralizeEffects(View view, Unit unit, string effect)
    {
        foreach (var stat in Stats.AllStats)
            if ((bool)unit.StatsManager.StatsDictionary[effect][stat][1] && stat != Stats.Hp)
                view.WriteLine($"Los {effect.ToLower()} de {stat} de {unit.Name} fueron neutralizados");
    }
    
    private static void PrintExtraDamageMessages(View view, Unit unit, string effect, string extraMessage)
    {
        var value = unit.DamageManager.DamageDictionary[effect];
        if (value != 0)
            view.WriteLine($"{unit.Name} realizará +{value} daño extra en {extraMessage}");
    }
    
    private static void PrintPercentageDamageReductionMessages(View view, Unit unit, string effect, string extraMessage)
    {
        var value = unit.DamageManager.DamageDictionary[effect];
        if (value != 0)
            view.WriteLine($"{unit.Name} reducirá el daño {extraMessage} del rival en un {value}%");
    }
    
    private static void PrintAbsolutDamageReductionMessages(View view, Unit unit)
    {
        var value = unit.DamageManager.DamageDictionary["AbsolutDamageReduction"];
        if (value != 0)
            view.WriteLine($"{unit.Name} recibirá {value} daño en cada ataque");
    }

    public static void PrintAfterCombatMessages(View view, Unit unit)
    {
        foreach (var (stat, statInfo) in unit.StatsManager.StatsDictionary["HealingAfterCombat"])
        {
            var value = (int)statInfo[0];
            var hasAttackedCondition = (bool)statInfo[1];
            if (!hasAttackedCondition || (hasAttackedCondition && unit.HasAttacked))
            {
                if (value > 0 && unit.Hp > 0)
                    view.WriteLine($"{unit.Name} recupera {value} HP despues del combate");
                else if (value < 0 && unit.Hp > 0)
                    view.WriteLine($"{unit.Name} recibe {-value} de daño despues del combate");
            }
        }
    }
}