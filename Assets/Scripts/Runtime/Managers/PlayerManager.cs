using System;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class PlayerManager : MonoBehaviour
{

    public PlayerData PlayerData;

    #region Public Variables

    public byte StageValue;
    internal ForceBallsToPoolCommand ForceCommand;

    #endregion

    #region SerializeField Variables

    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerMeshController meshController;
    [SerializeField] private PlayerPhysicsController physicsController;

    #endregion

    #region Private Variables

    private PlayerData _data;

    #endregion

    private void Awake()
    {
        _data = GetPlayerData();
        SendDataToControllers();
        Init();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }


    private void Init()
    {
        ForceCommand = new ForceBallsToPoolCommand(this, _data.ForceData);
    }

    private void SendDataToControllers()
    {
        movementController.SetData(_data.MovementData);
        meshController.SetData(_data.MeshData);
        //physicsController.SetData(_data.ForceData);
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>("Data/CD_Player").Data;
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += () => movementController.IsReadyToPlay(true);
        InputSignals.Instance.onInputReleased += () => movementController.IsReadyToPlay(false);
        InputSignals.Instance.onInputDragged += OnInputDragged;

        UISignals.Instance.onPlay += () => movementController.IsReadyToPlay(true);

        CoreGameSignals.Instance.onLevelSuccessful += () => movementController.IsReadyToMove(false);
        CoreGameSignals.Instance.onLevelFailed += () => movementController.IsReadyToMove(false);
        CoreGameSignals.Instance.onStageAreaEntered += () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful(0);
        CoreGameSignals.Instance.onFinishAreaEntered += OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset += OnReset;
    }



    private void UnsubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= () => movementController.IsReadyToPlay(true);
        InputSignals.Instance.onInputReleased -= () => movementController.IsReadyToPlay(false);
        InputSignals.Instance.onInputDragged -= OnInputDragged;

        UISignals.Instance.onPlay -= () => movementController.IsReadyToPlay(true);

        CoreGameSignals.Instance.onLevelSuccessful -= () => movementController.IsReadyToMove(false);
        CoreGameSignals.Instance.onLevelFailed -= () => movementController.IsReadyToMove(false);
        CoreGameSignals.Instance.onStageAreaEntered -= () => movementController.IsReadyToPlay(false);
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful(0);
        CoreGameSignals.Instance.onFinishAreaEntered -= OnFinishAreaEntered;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private UnityAction OnStageAreaSuccessful(byte value)
    {
        StageValue = ++value;
        return null;
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputParams(inputParams);
    }

    private void OnFinishAreaEntered()
    {
        CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        // mini game yazılmalı
    }
   

    private void OnReset()
    {
        StageValue = 0;
        movementController.OnReset();
        physicsController.OnReset();
        meshController.OnReset();
    }

}
