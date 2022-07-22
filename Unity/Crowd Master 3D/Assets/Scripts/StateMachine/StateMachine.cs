using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    protected Player _player;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected State _currentState;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(_rigidbody, _animator, _player);
    }

    private void Update() 
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState); 
    }

    protected void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_rigidbody, _animator, _player);
    }
}
