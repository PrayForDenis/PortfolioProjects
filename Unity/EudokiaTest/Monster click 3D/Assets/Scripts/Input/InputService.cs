using UnityEngine;
using UnityEngine.Events;

public class InputService : MonoBehaviour, IInputService
{
    private Camera _camera;
    private RaycastHit _hit;
    private Ray _ray;
    private int _score;

    public event UnityAction<int> ScoreChanged; 

    private void Awake() 
    {
        _camera = Camera.main;
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 100))
            {
                if (_hit.collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    if (!enemy._isSubbed)
                        RegisterEvent(enemy);

                    enemy.ApplyDamage();
                }
            }
        }   
    }

    private void RegisterEvent(Enemy enemy)
    {
        enemy.Died += OnEnemyDied;
        enemy._isSubbed = true;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _score++;
        ScoreChanged?.Invoke(_score);
        enemy.Died -= OnEnemyDied;
    }

    private void OnDisable() 
    {
        Destroy(gameObject);    
    }
}
