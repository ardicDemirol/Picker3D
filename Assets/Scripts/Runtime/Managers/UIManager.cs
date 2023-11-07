using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
    }


    private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
    }

    private void OnLevelInitialize(byte arg0)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
        UISignals.Instance.onSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
    }

    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
    }

   
    private void OnReset()
    {
        CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start,1);
    }

    public void NextLevel()
    {
        CoreGameSignals.Instance.onNextLevel?.Invoke();
    }

    public void RestartLevel()
    {
        CoreGameSignals.Instance.onRestart?.Invoke();
    }

    public void Play()
    {
        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
        InputSignals.Instance.onEnableInput?.Invoke();
    }
}
