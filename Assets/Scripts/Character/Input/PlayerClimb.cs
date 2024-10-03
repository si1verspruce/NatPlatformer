using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityScaleChanger))]
public class PlayerClimb : MonoBehaviour
{
    [SerializeField] private float _climbingSpeed;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody2D _characterRigidbody;
    [SerializeField] private ColliderDetection _detection;

    private GravityScaleChanger _gravityScaleChanger;
    private bool _isMovingFinished;

    public bool IsClimbing { get; private set; }

    private void Awake()
    {
        _gravityScaleChanger = GetComponent<GravityScaleChanger>();
    }

    private void OnDisable()
    {
        _characterRigidbody.gravityScale = 1;
    }

    private void FixedUpdate()
    {
        float direction = _input.Climb.ReadValue<float>();
        bool isLeftCollision = _detection.CheckForColliders(DirectionCode.Left);
        bool isRightCollision = _detection.CheckForColliders(DirectionCode.Right);

        if (isLeftCollision || isRightCollision)
        {
            IsClimbing = true;
            _gravityScaleChanger.SetGravityScale(this, 0);
            _characterRigidbody.velocity = new Vector2(_characterRigidbody.velocity.x, _climbingSpeed * direction);
            _isMovingFinished = true;
        }
        else if (_isMovingFinished)
        {
            IsClimbing = false;
            _gravityScaleChanger.SetGravityScale(this, 1);
            _isMovingFinished = false;
        }

    }
}