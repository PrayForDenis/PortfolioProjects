using UnityEngine;

public class LostPlayerTransition : Transition
{
    [SerializeField] private float _minimumLostDistance;

    private void Update() 
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > _minimumLostDistance) 
            NeedTransit = true;
    }
}
