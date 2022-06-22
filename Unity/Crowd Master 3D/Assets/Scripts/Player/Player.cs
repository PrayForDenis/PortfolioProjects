using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthContainer))]
public class Player : MonoBehaviour
{
    private HealthContainer _health;

    public event UnityAction Died;

    private void Awake() 
    {
        _health = GetComponent<HealthContainer>();    
    }

    private void OnEnable() 
    {
        _health.Died += OnDied;    
    }

    private void OnDisable() 
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Died?.Invoke();
    }

    public void ApplyDamage(float force)
    {
        _health.TakeDamage((int)force);
    }
}
