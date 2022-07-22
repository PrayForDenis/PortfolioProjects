using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyEffect;
    
    private const float _blockOffsetY = 1.38f;
    private MeshRenderer _meshRenderer;

    public event UnityAction<Block> BulletHit;

    private void Awake() 
    {
        _meshRenderer = GetComponent<MeshRenderer>();    
    }

    public void Break()
    {
        BulletHit?.Invoke(this);

        ParticleSystemRenderer renderer = Instantiate(_destroyEffect, new Vector3(transform.position.x, 
                                    transform.position.y - _blockOffsetY / 2, transform.position.z), 
                                    _destroyEffect.transform.rotation).GetComponent<ParticleSystemRenderer>();
        renderer.material.color = _meshRenderer.material.color;

        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
