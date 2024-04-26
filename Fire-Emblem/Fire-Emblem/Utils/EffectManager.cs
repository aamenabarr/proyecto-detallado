using Fire_Emblem_View;

namespace Fire_Emblem;

public class EffectManager
{
    public View View;
    public List<Effect> Effects = new List<Effect>();
    private List<AuxMessage> _messages = new List<AuxMessage>();
    
    public void AddUniqueMessage(Type type, string message, bool isAttacker)
    {
        if (message.Contains('+') || message.Contains('-'))
        {
            string[] parts = message.Split(' ')[2].Split('+', '-');
            string statName = parts[0];
            int modifier = int.Parse(parts[1]);
            if (message.Contains('-')) modifier *= -1;

            var existingMessage = _messages.FirstOrDefault(m =>
                m.IsAttacker == isAttacker && m.Message.Contains(statName) && m.Type == type &&
                m.Message.Contains("primer") == message.Contains("primer") && 
                 (m.Message.Contains("Follow-Up") == message.Contains("Follow-Up")));

            if (existingMessage != null)
            {
                string[] existingParts = existingMessage.Message.Split(' ');
                int existingModifier = int.Parse(existingParts[2].Split('+', '-')[1]);
                if (message.Contains('-')) existingModifier *= -1;
                existingModifier += modifier;

                string symbol = "";
                if (existingModifier > 0) symbol = "+";

                existingParts[2] = statName + symbol + Convert.ToString(existingModifier);

                string updatedMessage = string.Join(" ", existingParts);

                _messages.Remove(existingMessage);
                _messages.Add(new AuxMessage(existingMessage.Type, updatedMessage, isAttacker));
            }
            else if (!_messages.Any(m => m.Message == message && m.IsAttacker == isAttacker))
                _messages.Add(new AuxMessage(type, message, isAttacker));
        }
        else if (!_messages.Any(m => m.Message == message && m.IsAttacker == isAttacker))
            _messages.Add(new AuxMessage(type, message, isAttacker));
    }

    public void ApplyEffects()
    {
        foreach (var effect in OrderedEffects())
        {
            if (!effect.InFollowUp)
                effect.Apply();
            else
            {
                effect.IsActive = true;
                effect.AddAlterStatMessage();
            }
        }
        AddMessages();
    }

    public void ApplyFollowUpEffects()
    {
        foreach (var effect in OrderedEffects())
            if (effect.InFollowUp && effect.IsActive)
                effect.Apply();
    }
    
    private void AddMessages()
    {
        foreach (var effect in OrderedEffects())
        {
            foreach (var message in effect.Messages)
                AddUniqueMessage(effect.GetType(), message, effect.Unit.IsAttacker);
            if (!effect.InFollowUp)
                Effects.Remove(effect);
        }
        PrintMessages();
    }

    private void PrintMessages()
    {
        foreach (var auxMessage in OrderedMessages())
            View.WriteLine(auxMessage.Message);
        _messages.Clear();
    }
    
    public void ResetFollowUpEffects()
    {
        foreach (var effect in OrderedEffects())
            if (effect.InFollowUp && effect.IsActive)
                effect.ResetFollowUpEffect();
    }

    private List<Effect> OrderedEffects()
    {
        return Effects.OrderBy(e =>
        {
            var isAttacker = e.Unit.IsAttacker ? 0 : 1;
            var typeOrder = GetTypeOrder(e.GetType());
            return (isAttacker, typeOrder);
        }).ToList();
    }
    
    private List<AuxMessage> OrderedMessages()
    {
        return _messages.OrderBy(s =>
        {
            var isAttacker = s.IsAttacker? 0 : 1;
            var typeOrder = GetTypeOrder(s.Type);
            var statOrder = GetStatOrder(s.Message);
            var whenAppliesOrder = GetWhenAppliesOrder(s.Message);
            return (isAttacker, typeOrder, whenAppliesOrder, statOrder);
        }).ToList();
    }

    private int GetTypeOrder(Type type)
    {
        if (type == typeof(Bonus)) return 1;
        if (type == typeof(Penalty)) return 2;
        if (type == typeof(NeutralizeBonus)) return 3;
        if (type == typeof(NeutralizePenalty)) return 4;
        return 0;
    }

    private int GetStatOrder(string message)
    {
        if (message.Contains("Atk")) return 1;
        if (message.Contains("Spd")) return 2;
        if (message.Contains("Def")) return 3;
        if (message.Contains("Res")) return 4;
        return 0;
    }
    
    private int GetWhenAppliesOrder(string message)
    {
        if (message.Contains("primer ataque")) return 1;
        if (message.Contains("Follow-Up")) return 2;
        return 0;
    }
}