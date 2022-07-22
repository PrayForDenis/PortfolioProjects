using UnityEngine;
using Zenject;

public class PlayerTower : Tower
{
    private const int GunAngleSpeed = 20;

    private IInputService _input;

    [Inject]
    private void Construct(IInputService input)
    {
        _input = input;
    }

    private void OnEnable() 
    {
        _input.Shooted += OnPressShooted;    
    }

    private void Update() 
    {
        ElapsedTime += Time.deltaTime;    

        Gun.transform.Rotate(Vector3.back, _input.DeltaY * GunAngleSpeed * Time.deltaTime, Space.World);
    }

    private void OnDisable() 
    {
        _input.Shooted -= OnPressShooted;    
    }

    private void OnPressShooted()
    {
        if (ElapsedTime >= RechargeTime)
        {
            Gun.Shoot();
            ElapsedTime = 0;
        }
    }

}
