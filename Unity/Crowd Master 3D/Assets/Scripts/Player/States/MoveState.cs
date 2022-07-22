using UnityEngine;

public class MoveState : State
{
    [SerializeField] private StaminaAccumulator _staminaAccumulator;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedRatio;

    private const string RunHash = "run";

    private void OnEnable() 
    {
        _playerInput.DirectionChanged += OnDirectionChanged;
        _staminaAccumulator.StartAccumulate();
    }

    private void OnDisable() 
    {
        _playerInput.DirectionChanged -= OnDirectionChanged;
        Animator.SetFloat(RunHash, 0);
    }

    private void Update() 
    {
        Animator.SetFloat(RunHash, Rigidbody.velocity.magnitude / _maxSpeed);
    }

    private void OnDirectionChanged(Vector2 direction)
    {
        Rigidbody.velocity = new Vector3(direction.x, 0, direction.y) * _speedRatio;

        if (Rigidbody.velocity.magnitude >= _maxSpeed)
            Rigidbody.velocity *= _maxSpeed / Rigidbody.velocity.magnitude; 

        if (Rigidbody.velocity.magnitude != 0)
            Rigidbody.MoveRotation(Quaternion.LookRotation(Rigidbody.velocity, Vector3.up));
    }
}
