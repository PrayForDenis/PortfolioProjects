public class PlayerStateMachine : StateMachine
{
    private const string BrokenTriggerHash = "broken";

    private void OnEnable() 
    {
        _player.Died += OnDied;
    }

    private void OnDisable() 
    {
        _player.Died -= OnDied;
    }

    private void OnDied()
    {
        enabled = false;
        _animator.SetTrigger(BrokenTriggerHash);
    }
}
