namespace Fire_Emblem;

public class AuxMessage
{
    public string Message;
    public Type Type;
    public bool IsAttacker;
    public AuxMessage(Type type, string message, bool isAttacker)
    {
        Type = type;
        Message = message;
        IsAttacker = isAttacker;
    }
}