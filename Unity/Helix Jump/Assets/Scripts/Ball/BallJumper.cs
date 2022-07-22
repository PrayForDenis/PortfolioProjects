using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidBody;

    private void Start() 
    {
        _rigidBody = GetComponent<Rigidbody>();   
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent(out PlatformSegment platformSegment))
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }   
    }
}
