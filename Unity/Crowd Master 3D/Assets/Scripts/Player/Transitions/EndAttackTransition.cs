using UnityEngine;

public class EndAttackTransition : Transition
{
    [SerializeField] private AttackState _attackState;

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackState.AttackEnded += OnAttackEnded;
    }

    private void OnDisable() 
    {
        _attackState.AttackEnded -= OnAttackEnded;
    }

    private void OnAttackEnded()
    {
        NeedTransit = true;
    }

    private void Update() 
    {
        
    }
}
