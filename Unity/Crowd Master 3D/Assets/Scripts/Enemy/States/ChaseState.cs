using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : State
{
    private const string RunTriggerHash = "run";
    
    private NavMeshAgent _agent;

    private void Awake() 
    {
        _agent = GetComponent<NavMeshAgent>();    
    }

    private void OnEnable() 
    {
        _agent.enabled = true;
        Animator.SetTrigger(RunTriggerHash);    
    }

    private void OnDisable() 
    {
        _agent.enabled = false;    
    }

    private void Update() 
    {
        _agent.SetDestination(Player.transform.position);
    }
}
