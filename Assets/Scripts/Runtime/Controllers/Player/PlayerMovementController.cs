using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    #region Serialized Variables

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private new Collider collider;

    #endregion

    #region Private Variables

    private PlayerMovementData _data;
    private bool _isReadyToMove,isReadyToPlay;
    private float _xValue;
    private float2 _clampValues;

    #endregion

    internal void SetData(PlayerMovementData data)
    {
        _data = data;
    }

    private void FixedUpdate()
    {
        if(!isReadyToPlay)
        {
            StopPlayer();
            return;
        }

        if(_isReadyToMove)
        {
            MovePlayer();
        }
        else
        {
            StopPlayerHorizontally();
        }
    }

    private void MovePlayer()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_xValue*_data.SideWaySpeed,velocity.y,_data.ForwardSpeed);
        rigidbody.velocity = velocity;

        var position1 = rigidbody.position;
        Vector3 position;
        position = new Vector3(Mathf.Clamp(position1.x,_clampValues.x,_clampValues.y),(position = rigidbody.position).y,position.z);
        rigidbody.position = position;
    }

    private void StopPlayerHorizontally()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void StopPlayer()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    internal void IsReadyToPlay(bool condition)
    {
        isReadyToPlay = condition;
    }

    internal void IsReadyToMove(bool condition)
    {
        _isReadyToMove = condition;
    }

    internal void UpdateInputParams(HorizontalInputParams inputParams)
    {
        _xValue = inputParams.HorizontalValue;
        _clampValues = inputParams.ClampValues;
    }

    internal void OnReset()
    {
        StopPlayer();
        _isReadyToMove = false;
        isReadyToPlay = false;
    }
}
