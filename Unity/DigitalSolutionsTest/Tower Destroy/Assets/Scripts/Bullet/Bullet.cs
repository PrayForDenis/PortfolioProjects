using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.TryGetComponent(out Tower tower))
        {
            tower.ApplyDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.collider.TryGetComponent(out Bullet bullet))
        {
            Destroy(gameObject);
        }
        else if (other.collider.TryGetComponent(out Shield shield))
        {
            shield.ApplyDamage();
            Destroy(gameObject);
        }
        else if (other.collider.TryGetComponent(out ShieldBackSide backSide))
        {
            Destroy(gameObject);
        }
    }
}
