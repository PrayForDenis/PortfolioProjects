using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : ObjectPool
{
    private const int SpawnOffsetY = 1;
    private const int MaxActiveEnemies = 10;

    [SerializeField] private GameObject[] _templates;
    [SerializeField] private Vector3 _minSpawnPos;
    [SerializeField] private Vector3 _maxSpawnPos;
    [SerializeField] private float _sessionTime;

    private float _elapsedTime = 0;
    private int _activeEnemyCount;
    private Difficult _difficult;

    public event UnityAction MaxActiveEnemiesSpawned;
    public event UnityAction<int> EnemyCountChanged;

    private void Start() 
    {
        Initialize(_templates);
        _difficult = GetComponent<Difficult>();  
    }

    private void Update() 
    {
        _elapsedTime += Time.deltaTime;

        _activeEnemyCount = GetActiveObjectsCount();
        EnemyCountChanged?.Invoke(_activeEnemyCount);

        if (_elapsedTime >= _difficult.GetSpawnTime(Time.time / _sessionTime))
        {
            if (TryGetObject(out GameObject enemy))
            {
                _elapsedTime = 0;

                Vector3 spawnPosition = new Vector3(Random.Range(_minSpawnPos.x, _maxSpawnPos.x), SpawnOffsetY,
                                                Random.Range(_minSpawnPos.z, _maxSpawnPos.z));

                enemy.SetActive(true);
                enemy.transform.position = spawnPosition;
                enemy.transform.rotation = Quaternion.Euler(-90, 0, 0);
            }
        }  

        if (_activeEnemyCount == MaxActiveEnemies)
            MaxActiveEnemiesSpawned?.Invoke();
    }
}
