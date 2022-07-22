using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public void Close()
    {
        CanvasGroup.alpha = 0;

        Destroy(gameObject);
    }
}
