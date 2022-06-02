namespace ItransitionTask4.Services;

public interface IActions
{
    public event EventHandler OnAnyUserAction;
    public void InvokeAnyUserAction();
}