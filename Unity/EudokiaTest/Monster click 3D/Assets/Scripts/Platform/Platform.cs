using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnDisable() 
    {
        Destroy(gameObject);    
    }
}
