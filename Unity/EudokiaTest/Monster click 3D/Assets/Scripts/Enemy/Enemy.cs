using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    public event UnityAction<Enemy> Died;
    public bool _isSubbed;
    private int _currentHealth;

    private void OnEnable() 
    {
        _currentHealth = _health;    
    }

    public void ApplyDamage()
    {
        _currentHealth--;
        
        if (_currentHealth <= 0)
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
            _isSubbed = false;
        }
    }
}
