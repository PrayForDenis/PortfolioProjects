using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update() 
    {
        transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);   
    }
}
