using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable() 
    {
        _player.HealthChanged += OnHealthChanged;    
    }

    private void OnDisable() 
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        if (_hearts.Count < value)
        {
            int createHealth = value - _hearts.Count;

            for (int i = 0; i < createHealth; i++)
            {
                CreateHeart();
            }
        }
        else if (_hearts.Count > value && _hearts.Count != 0)
        {
            int destroyHealth = _hearts.Count - value;

            for (int i = 0; i < destroyHealth; i++)
            {
                DestroyHeart(_hearts[_hearts.Count - 1]);
            }
        }
    }

    private void DestroyHeart(Heart heart)
    {
        _hearts.Remove(heart);
        heart.ToEmpty();
    }

    private void CreateHeart()
    {
        GameObject newHeart = Instantiate(_heartTemplate, transform);
        Heart newHeartComponent = newHeart.GetComponent<Heart>();
        _hearts.Add(newHeartComponent);
        newHeartComponent.ToFill();
    }
}
