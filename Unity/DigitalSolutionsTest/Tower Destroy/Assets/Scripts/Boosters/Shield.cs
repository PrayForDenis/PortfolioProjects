using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour, IHitable
{
    [SerializeField] private int _health;
    [SerializeField] private float _duration;

    private int _currentHealth;
    private float _timeLeft = 0f;

    public event UnityAction ShieldDestroyed;
    public event UnityAction TimerFinished;
    public event UnityAction<float> TimeUpdated;

    private void Awake() 
    {
        gameObject.SetActive(false);    
    }

    private void OnEnable() 
    {
        _currentHealth = _health;
        _timeLeft = _duration;
        TimeUpdated?.Invoke(_timeLeft % 60);        
    }

    public void Accept(IHitVisitor hitVisitor)
    {
        hitVisitor.Visit(this);
    }

    public void ApplyDamage()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            ShieldDestroyed?.Invoke();
        }
    }

    public IEnumerator StartShieldTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            
            if (_timeLeft < 0)
                _timeLeft = 0;

            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            TimeUpdated?.Invoke(seconds);

            yield return null;
        }

        TimerFinished?.Invoke();
    }
}
