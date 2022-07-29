using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    private readonly Inputs _input;

    public float DeltaY => _input.Player.RotateGun.ReadValue<float>();

    public event UnityAction Shooted;

    public InputService()
    {
        _input = new Inputs();
    }

    public void Enable()
    {
        _input.Enable();
        _input.Player.Shoot.performed += OnShootPressed;
    }

    public void Disable()
    {
        _input.Disable();
        _input.Player.Shoot.performed -= OnShootPressed;
    }

    private void OnShootPressed(InputAction.CallbackContext ctx)
    {
        Shooted?.Invoke();
    }
}
