using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject[] templates)
    {
        for (int i = 0; i < _capacity; i++) 
        {
            GameObject spawned = Instantiate(templates[Random.Range(0, templates.Length)], 
                                                _container.transform.position, Quaternion.identity, _container.transform);

            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result) 
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected int GetActiveObjectsCount()
    {
        return _pool.Count(p => p.activeSelf == true);
    }

    private void OnDisable() 
    {
        Destroy(gameObject);    
    }
}
