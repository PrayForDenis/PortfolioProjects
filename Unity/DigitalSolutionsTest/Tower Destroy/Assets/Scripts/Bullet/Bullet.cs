using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private Effect _explosionEffect;
    private Effect _shieldEffect;

    private void Awake() 
    {
        _explosionEffect = Resources.Load<Effect>(AssetPath.ExplosionEffect);
        _shieldEffect = Resources.Load<Effect>(AssetPath.ShieldEffect);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.TryGetComponent(out Tower tower))
        {
            tower.ApplyDamage(_damage);
            InstantiateEffect(_explosionEffect, transform.position);
        }
        else if (other.collider.TryGetComponent(out Bullet bullet))
        {
            InstantiateEffect(_explosionEffect, transform.position);
        }
        else if (other.collider.TryGetComponent(out Shield shield))
        {
            shield.ApplyDamage();
            InstantiateEffect(_shieldEffect, transform.position);
        }
        else if (other.collider.TryGetComponent(out ShieldBackSide backSide))
        {
            InstantiateEffect(_shieldEffect, transform.position);
        }
    }

    private void InstantiateEffect(Effect effect, Vector3 at)
    {
        Instantiate(effect, at, Quaternion.identity);
        Destroy(gameObject);
    }
}
