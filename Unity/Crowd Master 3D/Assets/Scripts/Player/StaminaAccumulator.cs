using UnityEngine;

public class StaminaAccumulator : MonoBehaviour
{
    [SerializeField] private float _accumulationTime;
    [SerializeField] private Ability _ability;
    [SerializeField] private Ability _ultimateAbility;

    private float _staminaValue;

    private void Update() 
    {
        _staminaValue += Time.deltaTime;    
    }

    public void StartAccumulate()
    {
        _staminaValue = 0;
    }

    public Ability GetAbility()
    {
        if (_staminaValue > _accumulationTime)
            return _ultimateAbility;

        return _ability;
    }
}
