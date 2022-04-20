using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMove _move;

    private void Start() 
    {
        _move = GetComponent<PlayerMove>();   
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.W))
            _move.TryMoveUp();

        if (Input.GetKeyDown(KeyCode.S))
            _move.TryMoveDown();
    }
}
