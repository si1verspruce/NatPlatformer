using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishView : CharacterView
{
    [SerializeField] private Movement _movement;
    [SerializeField] private PlayerCharacter _playerCharacter;

    private bool _isUnderwater;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerCharacter.UnderwaterStatusChanged += UnderwaterStatusChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _playerCharacter.UnderwaterStatusChanged -= UnderwaterStatusChanged;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            Vector2 direction = _movement.Velocity.normalized;
            CharacterAnimator.SetBool(AnimatorIsMoving, direction == Vector2.zero ? false : true);

            if (IsGrounded == true)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            }
            else if (direction != Vector2.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-direction.y, direction.x));
                Renderer.flipX = _movement.RightFaced == false;
                float angleZ = Renderer.flipX ? rotation.eulerAngles.z - 180 : rotation.eulerAngles.z;
                rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, angleZ);
                transform.rotation = rotation;
            }

            if (_isUnderwater == false)
                SetGrounded();
        }
    }

    private void UnderwaterStatusChanged(bool isUnderwater)
    {
        _isUnderwater = isUnderwater;
    }
}
