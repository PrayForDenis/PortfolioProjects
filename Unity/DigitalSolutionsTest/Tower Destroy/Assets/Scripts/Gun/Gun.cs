using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Bullet _template;
    [SerializeField] private Transform _shootPoint;

    public void Shoot()
    {
        Bullet bullet = Instantiate(_template, _shootPoint.position, Quaternion.identity, _shootPoint);

        var rigidbody = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.Euler(_shootPoint.eulerAngles.x, _shootPoint.eulerAngles.y, _shootPoint.eulerAngles.z);

        rigidbody.AddRelativeForce(bullet.transform.TransformDirection
            (new Vector2(
                (Mathf.Cos(bullet.transform.rotation.z * Mathf.Deg2Rad) * _bulletSpeed),
                (Mathf.Sin(bullet.transform.rotation.z * Mathf.Deg2Rad) * _bulletSpeed)
            )), 
            ForceMode2D.Impulse);

        bullet.gameObject.transform.SetParent(transform.parent, true);
    }
}
