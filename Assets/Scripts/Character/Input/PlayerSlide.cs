using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityScaleChanger))]
public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float _passiveSlidingSpeed;
    [SerializeField] private float _activeSlidingSpeed;
    [SerializeField] private Rigidbody2D _characterRigidbody;
    [SerializeField] private ColliderDetection _detection;
    [SerializeField] private PlayerInput _input;

    private GravityScaleChanger _gravityScaleChanger;
    private bool _isMovingFinished;

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
        float direction = Mathf.Clamp(_input.Climb.ReadValue<float>(), float.MinValue, 0);
        bool isLeftCollision = _detection.CheckForColliders(DirectionCode.Left);
        bool isRightCollision = _detection.CheckForColliders(DirectionCode.Right);

        if (isLeftCollision || isRightCollision)
        {
            _isMovingFinished = true;
            float currentMinSpeed;

            if (direction == 0)
                currentMinSpeed = _passiveSlidingSpeed;
            else
                currentMinSpeed = _activeSlidingSpeed;

            float verticalVelocity = Mathf.Clamp(_characterRigidbody.velocity.y, -currentMinSpeed, float.MaxValue);
            _characterRigidbody.velocity = new Vector2(_characterRigidbody.velocity.x, verticalVelocity);

            if (verticalVelocity == -currentMinSpeed)
                _gravityScaleChanger.SetGravityScale(this, 0);
            else
                _gravityScaleChanger.SetGravityScale(this, 1);
        }
        else if (_isMovingFinished)
        {
            _isMovingFinished = false;
            _gravityScaleChanger.SetGravityScale(this, 1);
        }
    }
}
