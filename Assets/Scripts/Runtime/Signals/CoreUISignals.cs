﻿using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
    public static CoreUISignals Instance;

    public UnityAction<UIPanelTypes,int> onOpenPanel = delegate { };
    public UnityAction<int> onClosePanel = delegate { };
    public UnityAction onCloseAllPanels = delegate { };



    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
}
