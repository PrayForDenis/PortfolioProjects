using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private Rigidbody _rigidbody;

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();    
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0) && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }   
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.TryGetComponent(out Road road))
        {
            _isGrounded = true;
        }   
    }
}
