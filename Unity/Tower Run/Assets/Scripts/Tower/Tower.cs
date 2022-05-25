using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Human[] _humansTemplate;
    [SerializeField] private Vector2Int _humanInTowerRange;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private List<Human> _humanInTower;

    private void Start() 
    {
        _humanInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    public void Break()
    {
        Human[] humans = GetComponentsInChildren<Human>();

        foreach (var human in humans)
        {
            human.transform.parent = null;
            Rigidbody rigidbody = human.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        //Destroy(gameObject);
    }

    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humanInTower.Count; i++)
        {
            float distanceBetweenPoints = CheckDistanceY(distanceChecker, _humanInTower[i].FixationPoint.transform);

            if (distanceBetweenPoints < fixationMaxDistance)
            {
                List<Human> collectedHumans = _humanInTower.GetRange(0, i + 1);
                _humanInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }

        return null;
    }
    
    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humansTemplate[Random.Range(0, _humansTemplate.Length)];

            _humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));

            _humanInTower[i].transform.localPosition = new Vector3(0, _humanInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humanInTower[i].FixationPoint.position;
        }
    }


    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }
}
