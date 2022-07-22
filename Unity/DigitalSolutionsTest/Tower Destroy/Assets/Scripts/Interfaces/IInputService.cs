using UnityEngine.Events;

public interface IInputService : IEnable, IDisable
{
    event UnityAction Shooted;

    float DeltaY { get; }
}
