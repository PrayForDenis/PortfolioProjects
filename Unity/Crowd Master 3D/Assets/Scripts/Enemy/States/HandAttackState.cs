using System.Collections;
using UnityEngine;

public class HandAttackState : State
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _coroutine;

    private void OnEnable() 
    {
        _coroutine = StartCoroutine(Attack());
    }

    private void OnDisable() 
    {
        StopCoroutine(_coroutine);    
    }

    private IEnumerator Attack()
    {
        while(enabled) 
        {
            Animator.SetTrigger("attack");
            yield return new WaitForSeconds(_attackDelay);
            Player.ApplyDamage(_attackForce);
        }
    }
}
