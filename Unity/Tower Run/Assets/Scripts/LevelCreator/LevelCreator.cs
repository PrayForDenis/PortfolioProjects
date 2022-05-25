using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;

    private void Start() 
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweenTowers = roadLength / _humanTowerCount;

        Vector3 spawnPoint;
        float distanceTravelled = 0;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetweenTowers;
            spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        }
    }
}
