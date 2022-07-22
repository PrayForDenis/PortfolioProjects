using UnityEngine;

public class FoundPlayerTransition : Transition
{
    [SerializeField] private float _foundDistance;

    private void Update() 
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < _foundDistance) 
            NeedTransit = true;
    }
}
