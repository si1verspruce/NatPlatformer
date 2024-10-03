using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWaterDash : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerSwimming _swimmingInput;
    [SerializeField] private Swimming _swimming;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    private Coroutine _dashingCoutdown;
    private float _lastDashTime;

    private void OnEnable()
    {
        _input.Jump.started += Dash;
    }

    private void OnDisable()
    {
        _input.Jump.started -= Dash;
        _swimmingInput.enabled = true;

        if (_dashingCoutdown != null)
            StopCoroutine(_dashingCoutdown);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (_swimming.Underwater && Time.time - _lastDashTime >= _duration)
        {
            if (_dashingCoutdown != null)
                StopCoroutine(_dashingCoutdown);

            float faceDirection = (_movement.RightFaced ? 1 : -1);

            if (_movement.Velocity == Vector2.zero)
                _movement.SetVelocity(faceDirection * _speed * Vector2.right, true, false);

            _movement.SetVelocity(_movement.Velocity.normalized * _speed, true, true);
            _lastDashTime = Time.time;
            _swimmingInput.enabled = false;
            _dashingCoutdown = StartCoroutine(CountdownDashDuration());
        }
    }

    private IEnumerator CountdownDashDuration()
    {
        yield return new WaitForSeconds(_duration);
        _swimmingInput.enabled = true;
    }
}
