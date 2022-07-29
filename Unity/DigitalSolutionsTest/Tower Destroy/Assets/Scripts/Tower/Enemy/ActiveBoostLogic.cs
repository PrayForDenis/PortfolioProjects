using System;

public class ActiveBoostLogic : EnemyLogic
{
    private readonly float _boostDelay;

    public ActiveBoostLogic(Action action, float boostDelay) : base(action)
    {
        _boostDelay = boostDelay;
    }

    protected override float GetCooldown()
    {
        return UnityEngine.Random.Range(_boostDelay - 3f, _boostDelay);
    }
}
