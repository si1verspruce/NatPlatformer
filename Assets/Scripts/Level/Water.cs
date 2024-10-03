using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _velocityScale;
    [SerializeField] private float _verticalDeceleration;
    [SerializeField] private float _maxVerticalVelocity;

    private readonly List<Movement> _movements = new();

    private void FixedUpdate()
    {
        foreach (Movement movement in _movements)
            if (movement.Velocity.y < _maxVerticalVelocity)
                movement.ChangeVelocity(new Vector2(0, 1), false, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IBreathing breathing))
            breathing.SetUnderwaterStatus(true);

        if (collision.TryGetComponent(out GravityScaleChanger gravityScaleChanger))
            gravityScaleChanger.SetGravityScale(this, _gravityScale);
        else if (collision.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.gravityScale = _gravityScale;

        if (collision.TryGetComponent(out Movement movement))
        {
            movement.SetVelocity(movement.Velocity / 2, true, true);
            movement.MoveSpeedScale = _velocityScale;
            _movements.Add(movement);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IBreathing breathing))
            breathing.SetUnderwaterStatus(false);

        if (collision.TryGetComponent(out GravityScaleChanger gravityScaleChanger))
            gravityScaleChanger.SetGravityScale(this, 1);
        else if (collision.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.gravityScale = 1;

        if (collision.TryGetComponent(out Movement movement) && _movements.Contains(movement))
        {
            movement.MoveSpeedScale = 1;
            _movements.Remove(movement);
        }
    }
}
