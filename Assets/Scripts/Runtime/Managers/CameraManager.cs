using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    #region Self Variables

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    #endregion


    #region Self Variables

    private float3 _firstPosition;

    #endregion


    private void OnEnable()
    {
        SubscribeEvents();
    }


    void Start()
    {
        Init();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

   

    private void Init()
    {
        _firstPosition = (float3)transform.position;
    }

    private void SubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnReset()
    {
        transform.position = _firstPosition;
    }

    private void OnSetCameraTarget()
    {
        //var player = FindObjectOfType<PlayerManager>().transform;
        //virtualCamera.Follow = player;
        // virtualCamera.LookAt = player;
    }
}
