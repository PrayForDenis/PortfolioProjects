using UnityEngine;

public class ApproachedPlayerTransition : Transition
{
    [SerializeField] private float _approachedDistance;

    private void Update() 
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < _approachedDistance) 
            NeedTransit = true;
    }
}
