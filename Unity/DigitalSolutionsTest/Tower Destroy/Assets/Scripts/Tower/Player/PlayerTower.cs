using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerTower : Tower
{
    private const int GunAngleSpeed = 20;

    private IInputService _input;

    public event UnityAction TimerFinished;
    public event UnityAction<float> TimeUpdated;    

    [Inject]
    private void Construct(IInputService input)
    {
        _input = input;
    }

    private void OnEnable() 
    {
        _input.Shooted += OnPressShooted;    
    }

    private void Update() 
    {
        ElapsedTime += Time.deltaTime;    

        Gun.transform.Rotate(Vector3.back, _input.DeltaY * GunAngleSpeed * Time.deltaTime, Space.World);
    }

    private void OnDisable() 
    {
        _input.Shooted -= OnPressShooted;    
    }

    protected override void Awake()
    {
        base.Awake();
        Shield.TimeUpdated += OnTimeUpdated;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Shield.TimeUpdated -= OnTimeUpdated; 
    }

    protected override void ResetBoost()
    {
        base.ResetBoost();
        TimerFinished?.Invoke();
        TimeUpdated?.Invoke(0f);
    }

    protected override void OnTimerFinished()
    {
        Shield.gameObject.SetActive(false);
        TimerFinished?.Invoke();
    }

    private void OnPressShooted()
    {
        if (ElapsedTime >= RechargeTime)
        {
            Gun.Shoot();
            ElapsedTime = 0;
        }
    }

    private void OnTimeUpdated(float seconds)
    {
        TimeUpdated?.Invoke(seconds);
    }
}
