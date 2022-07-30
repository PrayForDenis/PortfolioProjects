using UnityEngine;

public class Bullet : MonoBehaviour, IHitVisitor, IHitable
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
        if (other.collider.TryGetComponent(out IHitable hitable))
        {
            hitable.Accept(this);
        }    
    }

    public void Accept(IHitVisitor hitVisitor)
    {
        hitVisitor.Visit(this);
    }

    public void Visit(Tower tower)
    {
        tower.ApplyDamage(_damage);
        InstantiateEffect(_explosionEffect, transform.position);
    }

    public void Visit(Bullet bullet)
    {
        InstantiateEffect(_explosionEffect, transform.position);
    }

    public void Visit(Shield shield)
    {
        shield.ApplyDamage();
        InstantiateEffect(_shieldEffect, transform.position);
    }

    public void Visit(ShieldBackSide shieldBackSide)
    {
        InstantiateEffect(_shieldEffect, transform.position);
    }

    private void InstantiateEffect(Effect effect, Vector3 at)
    {
        Instantiate(effect, at, Quaternion.identity);
        Destroy(gameObject);
    }
}
