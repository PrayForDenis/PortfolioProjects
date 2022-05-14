using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    public void Break()
    {
        PlatformSegment[] segments = GetComponentsInChildren<PlatformSegment>();

        foreach (var segment in segments)
        {
            segment.Bounce(_bounceForce, transform.position, _bounceRadius);
        }
    }
}
