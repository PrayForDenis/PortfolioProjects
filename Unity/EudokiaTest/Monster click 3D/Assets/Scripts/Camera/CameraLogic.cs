using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private const float Delta = 200;

    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _border;
 
    private Vector3 _rightDirection = Vector3.back;
    private Vector3 _leftDirection = Vector3.forward;
    private float _screenWidth;
    private Camera _camera;

    private void Start() 
    {
        _camera = Camera.main;
        _screenWidth = UnityEngine.Screen.width;
    }

    private void Update()
    {
        if (Input.mousePosition.x <= 0 + Delta && _camera.transform.position.z <= _border.x)
        {
            transform.position += _leftDirection * Time.deltaTime * _speed;
        }

        if (Input.mousePosition.x >= _screenWidth - Delta && _camera.transform.position.z >= _border.y)
        {
            transform.position += _rightDirection * Time.deltaTime * _speed;
        }

    }
}
