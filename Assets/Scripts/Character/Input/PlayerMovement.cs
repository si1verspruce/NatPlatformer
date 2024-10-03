using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Movement _movement;
    [SerializeField] private ColliderDetection _groundDetection;
    [SerializeField] private Rigidbody2D _characterRigidbody;

    private void FixedUpdate()
    {
        Vector2 direction = new(_input.Move.ReadValue<float>(), 0);

        if (_groundDetection.CheckForColliders(DirectionCode.Bottom) || _characterRigidbody.gravityScale == 0)
            _movement.Move(direction, true, false);
        else
            _movement.ChangeVelocity(direction, true, false);
    }
}
