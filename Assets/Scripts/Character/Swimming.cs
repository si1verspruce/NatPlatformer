using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GravityScaleChanger), typeof(Collider2D))]
public class Swimming : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private GravityScaleChanger _gravityScaleChanger;
    private Collider2D _collider;
    private Collider2D _currentWaterCollider;

    public bool Underwater { get; private set; }

    private void Awake()
    {
        _gravityScaleChanger = GetComponent<GravityScaleChanger>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _movement.MoveSpeedScaleChanged += OnMoveSpeedScaleChanged;
    }

    private void OnDisable()
    {
        Underwater = false;
        _movement.MoveSpeedScaleChanged -= OnMoveSpeedScaleChanged;
    }

    public void Swim(Vector2 direction)
    {
        if (Underwater)
        {
            if (_collider.bounds.max.y <= _currentWaterCollider.bounds.max.y)
            {
                _movement.Move(direction, true, true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Water _))
        {
            Underwater = true;
            _currentWaterCollider = collision;
            _gravityScaleChanger.SetGravityScale(this, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Water _))
        {
            Underwater = false;
            _gravityScaleChanger.SetGravityScale(this, 1);
        }
    }

    private void OnMoveSpeedScaleChanged()
    {
        if (_movement.MoveSpeedScale != 1)
            _movement.MoveSpeedScale = 1;
    }
}
