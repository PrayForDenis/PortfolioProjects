using UnityEngine;

public class AttackTransition : Transition
{
    private void Update() 
    {
        if (Input.GetMouseButtonUp(0))
            NeedTransit = true;    
    }
}
