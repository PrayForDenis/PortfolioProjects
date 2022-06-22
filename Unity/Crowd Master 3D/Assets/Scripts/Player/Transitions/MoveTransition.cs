using UnityEngine;

public class MoveTransition : Transition
{
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            NeedTransit = true;
        }
    }
}
