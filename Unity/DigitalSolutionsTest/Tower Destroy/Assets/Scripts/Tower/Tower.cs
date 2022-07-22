using UnityEngine;
using UnityEngine.Events;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Shield _shield;
    [SerializeField] protected Gun Gun;
    [SerializeField] protected float RechargeTime;

    private int _currentHealth;

    protected float ElapsedTime;
    protected Coroutine _shieldTimer;

    public event UnityAction Died;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction TimerFinished;
    public event UnityAction<float> TimeUpdated;

    private void Awake() 
    {
        _shield.ShieldDestroyed += OnShieldDestroyed;
        _shield.TimerFinished += OnTimerFinished;
        _shield.TimeUpdated += OnTimeUpdated;

        _currentHealth = _health;  
        HealthChanged?.Invoke(_currentHealth, _health);           
    }

    private void OnDestroy() 
    {
        _shield.ShieldDestroyed -= OnShieldDestroyed;
        _shield.TimerFinished -= OnTimerFinished;
        _shield.TimeUpdated -= OnTimeUpdated;    
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Died?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth, _health); 
        ElapsedTime = 0;   
        
        if (_shieldTimer != null)
            ResetBoost();

        Gun.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ActiveBoost()
    {
        _shield.gameObject.SetActive(true);
        _shieldTimer = StartCoroutine(_shield.StartShieldTimer());
    }

    protected void ResetBoost()
    {
        StopCoroutine(_shieldTimer);
        TimerFinished?.Invoke();
        TimeUpdated?.Invoke(0f);
        _shield.gameObject.SetActive(false);
    }

    private void OnShieldDestroyed()
    {
        _shield.gameObject.SetActive(false);
    }

    private void OnTimerFinished()
    {
        _shield.gameObject.SetActive(false);
        TimerFinished?.Invoke();
    }

    private void OnTimeUpdated(float seconds)
    {
        TimeUpdated?.Invoke(seconds);
    }
}
