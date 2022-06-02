namespace ItransitionTask4.Services;

public class Actions : IActions
{
    public event EventHandler? OnAnyUserAction;

    public void InvokeAnyUserAction()
    {
        OnAnyUserAction?.Invoke(this, EventArgs.Empty);
    }
}