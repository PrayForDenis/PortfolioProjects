using UnityEngine;

public class Difficult : MonoBehaviour
{
    [SerializeField] private AnimationCurve _difficultKoefficient;
    [SerializeField] private Vector2 _timeBetweenSpawn;

    public float GetSpawnTime(float time)
    {
        return Random.Range(_timeBetweenSpawn.x, _timeBetweenSpawn.y) * _difficultKoefficient.Evaluate(time);
    }
}
