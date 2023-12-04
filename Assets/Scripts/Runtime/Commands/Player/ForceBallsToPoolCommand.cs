using UnityEngine;

public class ForceBallsToPoolCommand : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayerForceData _forceData;

    public ForceBallsToPoolCommand(PlayerManager playerManager, PlayerForceData forceData)
    {
        this._playerManager = playerManager;
        this._forceData = forceData;
    }

    internal void Execute()
    {
        //_playerManager.PlayerData.BallsPool.ForceBallsToPool(_forceData);
    }

}
