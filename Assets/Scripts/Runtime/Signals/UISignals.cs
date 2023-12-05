using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    public static UISignals Instance;

    public UnityAction<byte> onSetStageColor = delegate { };
    public UnityAction<byte> onSetLevelValue = delegate { };
    public UnityAction onPlay = delegate { };


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
}
