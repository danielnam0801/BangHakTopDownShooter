using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    public MovementDataSO _movementSO;

    private Rigidbody2D _rigidbody;

    protected float _currentVelocity = 0;
    protected Vector2  _movementdirection;

    public UnityEvent<float> onVelocityChange;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 moveInput)
    {
        if(moveInput.sqrMagnitude > 0)
        {
            if(Vector2.Dot(moveInput, _movementdirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementdirection = moveInput.normalized;

        }
        _currentVelocity = CalcurateSpeed(moveInput);
    }

    private float CalcurateSpeed(Vector2 moveInput)
    {
        if(moveInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.accerlation * Time.deltaTime;

        }
        else
        {
            _currentVelocity -= _movementSO.deAccerlation * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity,0,_movementSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        onVelocityChange?.Invoke(_currentVelocity);
        _rigidbody.velocity = _movementdirection * _currentVelocity;
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigidbody.velocity = Vector2.zero;
    }
    void Update()
    {
        
    }
}
