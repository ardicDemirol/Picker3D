using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIPanelController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private List<Transform> layers;

    #endregion

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
        if (CoreUISignals.Instance == null)
        {
            StartCoroutine(Coroutine());
        }
        //CoreUISignals.Instance.onClosePanel += OnClosePanel;
        //CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        //CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
    }

    private IEnumerator Coroutine()
    {
        while (CoreUISignals.Instance == null)
        {
            yield return null;
        }
        if (CoreUISignals.Instance != null)
        {
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
        }


    }

    private void UnSubscribeEvents()
    {
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
    }

    private void OnOpenPanel(UIPanelTypes panelType, int value)
    {
        OnClosePanel(value);
        Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[value]);
    }


    private void OnClosePanel(int value)
    {
        if (layers[value].childCount <= 0) return;

#if UNITY_EDITOR
        DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
                Destroy(layers[value].GetChild(0).gameObject);
#endif
    }


    private void OnCloseAllPanels()
    {
        foreach (var layer in layers)
        {
            if (layer.childCount <= 0) return;
#if UNITY_EDITOR
            DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
        }
    }

}
