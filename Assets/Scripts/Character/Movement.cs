using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveSpeedScale = 1;
    [SerializeField] private float _speedToAccelerationMultiplier;
    [SerializeField] private Rigidbody2D _rigidbody;

    public event UnityAction MoveSpeedScaleChanged;

    public bool RightFaced { get; private set; }

    public float MoveSpeedScale
    {
        get { return _moveSpeedScale; }
        set
        {
            _moveSpeedScale = Mathf.Clamp(value, 0, float.MaxValue);
            MoveSpeedScaleChanged?.Invoke();
        }
    }

    public Vector2 Velocity => _rigidbody.velocity;

    private void FixedUpdate()
    {
        if (Velocity.x > 0)
            RightFaced = true;
        else if (Velocity.x < 0)
            RightFaced = false;
    }

    public void Move(Vector2 direction, bool setHorizontalSpeed, bool setVerticalSpeed)
    {
        Vector2 velocity = _moveSpeed * _moveSpeedScale * direction.normalized;
        SetVelocity(velocity, setHorizontalSpeed, setVerticalSpeed);
    }

    public void ChangeVelocity(Vector2 direction, bool setHorizontalSpeed, bool setVerticalSpeed)
    {
        float scaledMoveSpeed = _moveSpeed * _moveSpeedScale;
        Vector2 absDirection = new(Mathf.Abs(direction.x), Mathf.Abs(direction.y));
        Vector2 acceleration = scaledMoveSpeed * _speedToAccelerationMultiplier * Time.deltaTime * absDirection;
        float acceleratedVelocityX = GrowTowards(Velocity.x, scaledMoveSpeed * Mathf.Sign(direction.x), acceleration.x);
        float acceleratedVelocityY = GrowTowards(Velocity.y, scaledMoveSpeed * Mathf.Sign(direction.y), acceleration.y);
        SetVelocity(new Vector2(acceleratedVelocityX, acceleratedVelocityY), setHorizontalSpeed, setVerticalSpeed);
    }

    public void SetVelocity(Vector2 velocity, bool setHorizontalSpeed, bool setVerticalSpeed)
    {
        if (setHorizontalSpeed && setVerticalSpeed)
            _rigidbody.velocity = velocity;
        else if (setHorizontalSpeed == false)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, velocity.y);
        else
            _rigidbody.velocity = new Vector2(velocity.x, _rigidbody.velocity.y);
    }

    private float GrowTowards(float current, float target, float maxDelta)
    {
        if (Mathf.Sign(target) == 1)
            target = Mathf.Clamp(target, current, float.MaxValue);
        else
            target = Mathf.Clamp(target, float.MinValue, current);

        return Mathf.MoveTowards(current, target, maxDelta);
    }
}
