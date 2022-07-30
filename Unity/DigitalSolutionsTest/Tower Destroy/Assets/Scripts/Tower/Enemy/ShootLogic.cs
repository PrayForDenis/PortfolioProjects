using System;
using UnityEngine;

public class ShootLogic : EnemyLogic
{
    private readonly Vector2 _shotDelay;
    private readonly float _rechargeTime;

    public ShootLogic(Action action, Vector2 shotDelay, float rechargeTime) : base(action)
    {
        _shotDelay = shotDelay;
        _rechargeTime = rechargeTime;
    }

    protected override float GetCooldown()
    {
        return _rechargeTime + UnityEngine.Random.Range(_shotDelay.x, _shotDelay.y);
    }
}
