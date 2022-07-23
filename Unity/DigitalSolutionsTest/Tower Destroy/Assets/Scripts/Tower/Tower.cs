using UnityEngine;
using UnityEngine.Events;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] protected Shield Shield;
    [SerializeField] protected Gun Gun;
    [SerializeField] protected float RechargeTime;

    private int _currentHealth;

    protected float ElapsedTime;
    protected Coroutine ShieldTimer;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Died;

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Died?.Invoke();
    }

    public void ActiveBoost()
    {
        Shield.gameObject.SetActive(true);
        ShieldTimer = StartCoroutine(Shield.StartShieldTimer());
    }

    public virtual void Reset()
    {
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth, _health); 
        ElapsedTime = 0;   
        
        if (ShieldTimer != null)
            ResetBoost();

        Gun.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected abstract void OnTimerFinished();
    
    protected virtual void Awake() 
    {
        Shield.ShieldDestroyed += OnShieldDestroyed;
        Shield.TimerFinished += OnTimerFinished;

        _currentHealth = _health;  
        HealthChanged?.Invoke(_currentHealth, _health);           
    }

    protected virtual void OnDestroy() 
    {
        Shield.ShieldDestroyed -= OnShieldDestroyed;
        Shield.TimerFinished -= OnTimerFinished;   
    }

    protected virtual void ResetBoost()
    {
        StopCoroutine(ShieldTimer);
        Shield.gameObject.SetActive(false);
    }

    private void OnShieldDestroyed()
    {
        Shield.gameObject.SetActive(false);
    }
}
