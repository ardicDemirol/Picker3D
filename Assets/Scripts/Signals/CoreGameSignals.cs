using UnityEngine;
using UnityEngine.Events;
using System;

public class CoreGameSignals : MonoBehaviour
{
    public static CoreGameSignals Instance;

    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestart = delegate { };
    public UnityAction onReset = delegate { };

    public Func<byte> onGetLevelValue = delegate { return 0; };


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

    }
}
