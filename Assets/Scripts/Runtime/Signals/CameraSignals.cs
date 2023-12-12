using Runtime.Extensions;
using UnityEngine.Events;

public class CameraSignals : MonoSingleton<CameraSignals>
{
    public UnityAction onSetCameraTarget = delegate { };
}
