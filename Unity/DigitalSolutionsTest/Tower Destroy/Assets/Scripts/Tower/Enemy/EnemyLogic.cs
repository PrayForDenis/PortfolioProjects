using System;

public abstract class EnemyLogic
{
    private float _elapsedTime;
    private Action _action;

    public EnemyLogic(Action action)
    {
        _action = action;
    } 

    public void TryExecuteAction(float delta) 
    {
        _elapsedTime += delta;

        if (_elapsedTime > GetCooldown())
        {
            _action?.Invoke();
            _elapsedTime = 0;
        }
    }

    protected abstract float GetCooldown(); 
}
