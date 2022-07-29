using System;
using UnityEngine;

public class ShootLogic : EnemyLogic
{
    private Vector2 _shotDelay;
    private float _rechargeTime;

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
