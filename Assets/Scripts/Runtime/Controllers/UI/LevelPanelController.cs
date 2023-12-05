using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private List<Image> stageImages;
    [SerializeField] private List<TextMeshProUGUI> levelTexts;

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
        UISignals.Instance.onSetLevelValue += OnSetLevelValue;
        UISignals.Instance.onSetStageColor += OnSetStageColor;
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onSetLevelValue -= OnSetLevelValue;
        UISignals.Instance.onSetStageColor -= OnSetStageColor;
    }

    private void OnSetStageColor(byte stageValue)
    {
        // DOColor
    }

    private void OnSetLevelValue(byte levelValue)
    {
        var additionalValue = ++levelValue;
        levelTexts[0].text = additionalValue.ToString();
        additionalValue++;
        levelTexts[1].text = additionalValue.ToString();
    }

  

}
