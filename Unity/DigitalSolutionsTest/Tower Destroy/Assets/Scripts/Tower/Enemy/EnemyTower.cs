using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class EnemyTower : Tower
{
    private const int GunAngleSpeed = 20;
    private const float MinAngleZ = 0;
    private const float MaxAngleZ = 60;
    private const float GunRotateDuration = 0.6f;
    private const float GunRotateDelay = 1.3f;

    [SerializeField] private Vector2 _shotDelay;
    [SerializeField] private float _boostDelay;

    private float _delayTime;
    private Coroutine _gunRotation;
    private List<EnemyLogic> _actions;

    private void Update() 
    {
        foreach(var action in _actions)
        {
            action.TryExecuteAction();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _actions = new List<EnemyLogic>
        {
            new ShootLogic(Gun.Shoot, _shotDelay, RechargeTime),
            new ActiveBoostLogic(ActiveBoost, _boostDelay)
        };

        _gunRotation = StartCoroutine(RotateGun());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopCoroutine(_gunRotation);
    }

    protected override void OnTimerFinished()
    {
        Shield.gameObject.SetActive(false);
    }

    private IEnumerator RotateGun()
    {
        while (true)
        {
            Gun.transform.DORotate(new Vector3(0, 0, Random.Range(MinAngleZ, MaxAngleZ)), GunRotateDuration)
                .SetLoops(1, LoopType.Yoyo);

            yield return new WaitForSeconds(GunRotateDelay);
        }
    } 
}
