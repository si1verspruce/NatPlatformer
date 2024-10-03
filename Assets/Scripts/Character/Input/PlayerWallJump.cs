using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : PlayerJump
{
    [SerializeField, Range(0, 90)] private float _jumpAngle;

    private Vector2 _topRightDirection;
    private Vector2 _topLeftDirection;
    private bool _isLeftCollision;
    private bool _isRightCollision;

    private void Awake()
    {
        float angleRadians = Mathf.Deg2Rad * _jumpAngle;
        _topRightDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        _topLeftDirection = new Vector2(-_topRightDirection.x, _topRightDirection.y);
    }

    private void Jump(Vector2 startVelocity)
    {
        CharacterRigidbody.velocity = startVelocity;
        StartLockDetection();
    }

    protected override bool TryJump()
    {
        bool isBottomCollision = ColliderDetection.CheckForColliders(DirectionCode.Bottom);
        _isLeftCollision = ColliderDetection.CheckForColliders(DirectionCode.Left);
        _isRightCollision = ColliderDetection.CheckForColliders(DirectionCode.Right);

        return isBottomCollision == false && (_isLeftCollision || _isRightCollision);
    }

    protected override void OnJump()
    {
        if (_isLeftCollision)
            Jump(_topRightDirection * StartVelocity);
        else if (_isRightCollision)
            Jump(_topLeftDirection * StartVelocity);
    }
}
