using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Player { get; private set; }
    protected Animator Animator { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    
    public void Enter(Rigidbody rigidbody, Animator animator, Player player)
    {
        if (!enabled) 
        {
            Rigidbody = rigidbody;
            Animator = animator;
            Player = player;

            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Player);
            }
        }
    }

    public void Exit() 
    {
        if (enabled) 
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
