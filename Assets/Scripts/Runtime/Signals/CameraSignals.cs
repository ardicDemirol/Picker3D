using UnityEngine;
using UnityEngine.Events;

public class CameraSignals : MonoBehaviour
{
    public static CameraSignals Instance;

    public UnityAction onSetCameraTarget = delegate { };

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

  

}
