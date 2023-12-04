using UnityEngine;

public class PlayerPhysicsController : MonoBehaviour
{

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;

    #endregion

    #region Private Variables

    private const string StageArea = "StageArea";
    private const string Finish = "FinishArea";
    private const string MiniGameArea = "MiniGameArea";
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StageArea))
        {
            manager.ForceCommand.Execute();
            CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();

            // stage area control process
        }

        if(other.CompareTag(Finish))
        {
            CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            return;
        }

        if(other.CompareTag(MiniGameArea))
        {
           // Write minigame mechanicsf
        }
    }


    internal void OnReset()
    {
    }
}
