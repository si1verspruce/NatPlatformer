using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class CharacterView : MonoBehaviour
{
    [SerializeField] private AllformView _view;
    [SerializeField] protected PlayerInput Input;
    [SerializeField] protected ColliderDetection CharacterColliderDetection;
    [SerializeField] protected Rigidbody2D CharacterRigidbody;

    [SerializeField] private string _animatorIsMovingName;
    [SerializeField] private string _animatorIsGroundedName;
    [SerializeField] private string _animatorVelocityYName;

    protected SpriteRenderer Renderer;
    protected int AnimatorIsMoving;
    protected int AnimatorIsGrounded;
    protected float Direction;

    private bool _isDetectionActive = true;
    private int _animatorVelocityY;
    private bool _isWalking;

    public event UnityAction<bool> WalkingActivityChanged;

    public Animator CharacterAnimator { get; private set; }
    public bool IsGrounded { get; private set; }

    protected virtual void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        CharacterAnimator = GetComponent<Animator>();

        AnimatorIsMoving = Animator.StringToHash(_animatorIsMovingName);
        AnimatorIsGrounded = Animator.StringToHash(_animatorIsGroundedName);
        _animatorVelocityY = Animator.StringToHash(_animatorVelocityYName);
    }

    protected virtual void OnEnable()
    {
        transform.localEulerAngles = Vector3.zero;
        Renderer.flipX = _view.FlipX;
    }

    protected virtual void OnDisable()
    {
        _isWalking = false;
        _view.FlipX = Renderer.flipX;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            Direction = Input.Move.ReadValue<float>();

            SetGrounded();
            FlipToDirection(Direction);
            MoveGrounded(Direction);
            CharacterAnimator.SetFloat(_animatorVelocityY, Mathf.Abs(CharacterRigidbody.velocity.y));

            OnUpdate();
        }
    }

    public void SetDetectionActive(bool isActive)
    {
        _isDetectionActive = isActive;

        if (_isDetectionActive == false)
            IsGrounded = false;
    }

    protected virtual void OnUpdate() { }

    protected virtual void OnLeftDirection()
    {
        Renderer.flipX = true;
    }

    protected virtual void OnRightDirection()
    {
        Renderer.flipX = false;
    }

    protected void FlipToDirection(float direction)
    {
        if (direction < 0)
            OnLeftDirection();
        else if (direction > 0)
            OnRightDirection();
    }

    protected void MoveGrounded(float direction)
    {
        if (IsGrounded && direction != 0)
        {
            SetWalkingActivity(true, ref _isWalking);
            CharacterAnimator.SetBool(AnimatorIsMoving, true);
        }
        else
        {
            SetWalkingActivity(false, ref _isWalking);
            CharacterAnimator.SetBool(AnimatorIsMoving, false);
        }
    }

    protected void SetGrounded()
    {
        if (_isDetectionActive)
        {
            IsGrounded = CharacterColliderDetection.CheckForColliders(DirectionCode.Bottom);
            CharacterAnimator.SetBool(AnimatorIsGrounded, IsGrounded);
        }
    }

    protected void SetWalkingActivity(bool isActive, ref bool isWalking)
    {
        if (isActive != isWalking)
        {
            if (isWalking == false)
                isWalking = true;
            else
                isWalking = false;

            WalkingActivityChanged?.Invoke(isWalking);
        }
    }
}
