using UnityEngine;

public class WaitingState : State
{
    private const string SetupTriggerHash = "setup";

    private void OnEnable() 
    {
        Animator.SetTrigger(SetupTriggerHash);
    }

    private void Update() 
    {
        
    }
}
