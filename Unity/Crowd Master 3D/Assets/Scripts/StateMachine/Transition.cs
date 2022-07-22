using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] protected State _targetState;

    protected Player Player { get; private set; }
    
    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    protected virtual void OnEnable() 
    {
        NeedTransit = false;
    }

    public void Init(Player player)
    {
        Player = player;
    }
}
