using UnityEngine;

public class Hud : MonoBehaviour
{
    private void OnDisable() 
    {
        Destroy(gameObject);    
    }
}
