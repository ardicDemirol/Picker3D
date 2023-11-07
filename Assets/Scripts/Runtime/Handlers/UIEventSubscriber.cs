using UnityEngine;
using UnityEngine.UI;

public class UIEventSubscriber : MonoBehaviour
{
    #region Serialized Variables  

    [SerializeField] UIEventSubscriptionTypes type;
    [SerializeField] private Button button;

    #endregion

    #region Private Variables

    private UIManager _manager;


    #endregion


    private void Awake()
    {
        GetReferences();
    }

    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnSubscribeEvent();
    }



    private void SubscribeEvent()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.AddListener(() => _manager.Play());
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.AddListener(() => _manager.NextLevel());
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.AddListener(() => _manager.RestartLevel());
                break;
            default:
                break;
        }
    }

    private void UnSubscribeEvent()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.RemoveListener(() => _manager.Play());
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.RemoveListener(() => _manager.NextLevel());
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.RemoveListener(() => _manager.RestartLevel());
                break;
            default:
                break;
        }
    }

    private void GetReferences()
    {
        _manager = FindObjectOfType<UIManager>();
    }
}
