using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedDetection : MonoBehaviour
{
    [SerializeField] private ColliderDetection _detection;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private EventToggleInvoker _groundedEvent;

    private bool _isGrounded;

    private void OnDisable()
    {
        _isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (_detection.CheckForColliders(DirectionCode.Bottom) && _rigidbody.gravityScale != 0)
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                _rigidbody.velocity = Vector2.zero;
                _groundedEvent.Toggle(true);
            }
        }
        else if (_isGrounded)
        {
            _isGrounded = false;
            _groundedEvent.Toggle(false);
        }
    }
}
