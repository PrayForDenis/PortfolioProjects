using UnityEngine;
using UnityEngine.Events;

public class AttackState : State
{
    [SerializeField] private StaminaAccumulator _staminaAccumulator;

    private const string AttackTrigger = "attack";

    private Ability _currentAbility;

    public event UnityAction<IDamageable> CollisionDetected;
    public event UnityAction AttackEnded;

    private void OnEnable() 
    {
        Animator.SetTrigger(AttackTrigger);

        _currentAbility = _staminaAccumulator.GetAbility();
        _currentAbility.AbilityEnded += OnAttackEnded;

        _currentAbility.UseAbility(this);
    }

    private void OnDisable() 
    {
        _currentAbility.AbilityEnded -= OnAttackEnded;    
    }

    private void OnAttackEnded()
    {
        AttackEnded?.Invoke();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out IDamageable damageable))
            CollisionDetected?.Invoke(damageable);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
            CollisionDetected?.Invoke(damageable);
    }

    private void Update() 
    {
        
    }
}
