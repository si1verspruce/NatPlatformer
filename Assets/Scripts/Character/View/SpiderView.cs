using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderView : CharacterView
{
    [SerializeField] private string _animatorIsClimbingName;
    [SerializeField] private PlayerClimb _climb;
    [SerializeField] private float _climbRotationAngle;

    private int _animatorIsClimbing;
    private bool _isClimbing;

    protected override void Awake()
    {
        base.Awake();
        _animatorIsClimbing = Animator.StringToHash(_animatorIsClimbingName);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (IsGrounded == false)
            Climb(1);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _isClimbing = false;
    }

    protected override void OnUpdate()
    {
        float directionY = Input.Climb.ReadValue<float>();
        UpdateAnimator(directionY);
    }

    private void UpdateAnimator(float direction)
    {
        if (_climb.IsClimbing && IsGrounded == false)
        {
            if (direction != 0)
            {
                SetWalkingActivity(true, ref _isClimbing);
                CharacterAnimator.SetBool(_animatorIsClimbing, true);
            }
            else
            {
                SetWalkingActivity(false, ref _isClimbing);
                CharacterAnimator.SetBool(_animatorIsClimbing, false);
            }

            Climb(direction);
        }
        else
        {
            SetWalkingActivity(false, ref _isClimbing);
            CharacterAnimator.SetBool(_animatorIsClimbing, false);
            transform.localEulerAngles = Vector3.zero;
            Renderer.flipY = false;
        }
    }

    private void Climb(float direction)
    {
        if (CharacterColliderDetection.CheckForColliders(DirectionCode.Left))
            RotateToClimbDirection(direction, false);
        else if (CharacterColliderDetection.CheckForColliders(DirectionCode.Right))
            RotateToClimbDirection(direction, true);
    }

    private void RotateToClimbDirection(float direction, bool isRightFaced)
    {
        transform.localEulerAngles = Vector3.forward * _climbRotationAngle;
        Renderer.flipY = isRightFaced == false;

        if (direction > 0)
            Renderer.flipX = false;
        else if (direction < 0)
            Renderer.flipX = true;
    }
}
