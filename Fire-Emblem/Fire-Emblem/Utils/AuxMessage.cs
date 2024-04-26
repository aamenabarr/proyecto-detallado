namespace Fire_Emblem;

public class AuxMessage
{
    public string Message;
    public bool IsAttacker;
    public Type Type;
    
    public AuxMessage(Type type, string message, bool isAttacker)
    {
        Type = type;
        Message = message;
        IsAttacker = isAttacker;
    }
}