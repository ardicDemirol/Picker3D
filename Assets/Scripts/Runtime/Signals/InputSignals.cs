using Runtime.Extensions;
using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onEnableInput = delegate { };
    public UnityAction onDisableInput = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction onInputReleased = delegate { };
    public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
   
}
