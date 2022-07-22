using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : StateMachine, IDamageable
{
    [SerializeField] private BrokenState _brokenState;
    [SerializeField] private HealthContainer _healthContainer;

    private float _minDamage;

    public event UnityAction<EnemyStateMachine> Died;

    private void OnEnable() 
    {
        _healthContainer.Died += OnEnemyDied;
    }

    private void OnDisable() 
    {
        _healthContainer.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        Died?.Invoke(this);
    }

    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        if (force > _minDamage && _currentState != _brokenState) 
        {
            _healthContainer.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplyDamage(rigidbody, force);

            return true;
        }

        return false;
    }
}
