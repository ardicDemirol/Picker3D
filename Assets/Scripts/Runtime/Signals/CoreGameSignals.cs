using Runtime.Extensions;
using System;
using UnityEngine.Events;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction<byte> onLevelInitialize = delegate { };

    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };


    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestart = delegate { };
    public UnityAction onReset = delegate { };

    public UnityAction onStageAreaEntered = delegate { };
    public UnityAction onStageAreaSuccessful = delegate { };
    public UnityAction onFinishAreaEntered = delegate { };

    public Func<byte> onGetLevelValue = delegate { return 0; };
  
}
