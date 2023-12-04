using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Self Variables

    private InputData _data;
    private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;

    private float _currentVelocity;
    private Unity.Mathematics.float3 _moveVector;
    private Vector2? _mousePosition;



    #endregion

    private void Awake()
    {
        _data = GetInputData();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        if (!_isAvailableForTouch) return;

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
            Debug.LogWarning("Executed ------> Input Released");
        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            Debug.LogWarning("Executed ------> On Input Taken");

            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                Debug.LogWarning("Executed ------> On First Time Touch Taken");
            }

            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else if (mouseDeltaPos.x < _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);
                    }

                    _mousePosition = Input.mousePosition;

                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizontalValue = _moveVector.x,
                        ClampValues = _data.ClampValues
                    });

                }
            }
        }

    }



    private void OnDisable()
    {
        UnSubscribeEvents();
    }


    private InputData GetInputData()
    {
        return Resources.Load<CD_Input>("Data/CD_Input").Data;
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onReset += OnReset;
        InputSignals.Instance.onEnableInput += OnEnableInput;
        InputSignals.Instance.onDisableInput += OnDisableInput;
    }
    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onReset -= OnReset;
        InputSignals.Instance.onEnableInput -= OnEnableInput;
        InputSignals.Instance.onDisableInput -= OnDisableInput;
    }

    private void OnEnableInput()
    {
        _isAvailableForTouch = true;
    }

    private void OnDisableInput()
    {
        _isAvailableForTouch = false;
    }

    private void OnReset()
    {
        _isAvailableForTouch = false;
        _isFirstTimeTouchTaken = false;
        _isTouching = false;
    }

    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = (Vector2) Input.mousePosition
        };

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
        
    }

}
