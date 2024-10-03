using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVerticalJump : PlayerJump
{
    [SerializeField] private float _timeToSaveGrounded;

    private bool _isGrounded;
    private float _lastGroundedTime;

    private void FixedUpdate()
    {
        if (IsDetectionLocked == false)
        {
            bool isCollisionBottom = ColliderDetection.CheckForColliders(DirectionCode.Bottom);

            if (_isGrounded && isCollisionBottom == false)
            {
                if (Time.time - _lastGroundedTime >= _timeToSaveGrounded)
                    _isGrounded = false;
            }
            else
            {
                _isGrounded = isCollisionBottom;

                if (_isGrounded)
                    _lastGroundedTime = Time.time;
            }
        }
    }

    protected override bool TryJump()
    {
        return _isGrounded;
    }

    protected override void OnJump()
    {
        _isGrounded = false;
        StartLockDetection();
        Vector2 jumpDirection = new Vector2(CharacterRigidbody.velocity.x, StartVelocity).normalized;
        float horizontalSpeed = CharacterRigidbody.velocity.x * (1 + Mathf.Abs(jumpDirection.x));
        float verticalSpeed = StartVelocity * jumpDirection.y;
        //Debug.Log(horizontalSpeed + " " + verticalSpeed);
        CharacterRigidbody.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }
}
