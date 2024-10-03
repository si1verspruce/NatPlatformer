using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeView : CharacterView
{
    [SerializeField] private string _animatorIsSlidingName;
    [SerializeField] private int _slideTransitionVelocity;

    private int _animatorIsSliding;
    private bool _isSliding;

    protected override void Awake()
    {
        base.Awake();
        _animatorIsSliding = Animator.StringToHash(_animatorIsSlidingName);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _isSliding = false;
    }

    protected override void OnUpdate()
    {
        Slide();
    }

    private void Slide()
    {
        if (CharacterRigidbody.velocity.y <= _slideTransitionVelocity && IsGrounded == false)
        {
            if (CharacterColliderDetection.CheckForColliders(DirectionCode.Left))
            {
                SetWalkingActivity(true, ref _isSliding);
                CharacterAnimator.SetBool(_animatorIsSliding, true);
                FlipToDirection(1);
            }
            else if (CharacterColliderDetection.CheckForColliders(DirectionCode.Right))
            {
                SetWalkingActivity(true, ref _isSliding);
                CharacterAnimator.SetBool(_animatorIsSliding, true);
                FlipToDirection(-1);
            }
            else
            {
                SetWalkingActivity(false, ref _isSliding);
                CharacterAnimator.SetBool(_animatorIsSliding, false);
            }
        }
        else
        {
            SetWalkingActivity(false, ref _isSliding);
            CharacterAnimator.SetBool(_animatorIsSliding, false);
        }
    }
}
