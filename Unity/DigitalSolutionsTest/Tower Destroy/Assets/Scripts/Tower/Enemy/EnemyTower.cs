using UnityEngine;
using DG.Tweening;
using System.Collections;

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

    private void Start() 
    {
        _gunRotation = StartCoroutine(RotateGun());    
    }

    private void Update() 
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= RechargeTime + Random.Range(_shotDelay.x, _shotDelay.y))
        {
            Gun.Shoot();
            ElapsedTime = 0;
        }

        _delayTime += Time.deltaTime;

        if (_delayTime >= Random.Range(_boostDelay - 3f, _boostDelay))
        {
            ActiveBoost();
            _delayTime = 0;
        }
    }

    public void EnableGunRotation() 
    {
        if (_gunRotation == null)
            _gunRotation = StartCoroutine(RotateGun());
    }

    public override void Reset()
    {
        StopCoroutine(_gunRotation);
        base.Reset();
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
