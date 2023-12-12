using Runtime.Extensions;
using UnityEngine.Events;

public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction<byte> onSetStageColor = delegate { };
    public UnityAction<byte> onSetLevelValue = delegate { };
    public UnityAction onPlay = delegate { };
}
