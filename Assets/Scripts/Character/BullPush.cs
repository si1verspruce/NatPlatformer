using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BullPush : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ColliderDetection _detection;
    [SerializeField] private EventToggleInvoker _pushingEvent;
    [SerializeField] private int _movableLayer;
    [SerializeField] private float _pushVelocity;
    [SerializeField] private float _maxPushAngle;

    private readonly Dictionary<Collider2D, Rigidbody2D> _movableColliderRigidbodyPairs = new();

    private Collider2D _collider;

    public bool IsPushing { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnDisable()
    {
        _movableColliderRigidbodyPairs.Clear();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > 0 && _movableColliderRigidbodyPairs.Count > 0 
            && _detection.CheckForColliders(DirectionCode.Bottom))
        {
            int pushingObjectsCount = 0;

            foreach (Collider2D collider in _movableColliderRigidbodyPairs.Keys)
            {
                Rigidbody2D rigidbody = _movableColliderRigidbodyPairs[collider];
                Vector2 bodyDirection = (_collider.bounds.center - collider.bounds.center).normalized;
                float bodyDirectionSign = Mathf.Sign(bodyDirection.x);
                float angle = Vector2.Angle(bodyDirectionSign * Vector2.right, bodyDirection);

                if (angle <= _maxPushAngle && Mathf.Sign(Mathf.Round(_rigidbody.velocity.x)) == -bodyDirectionSign)
                {
                    rigidbody.velocity = new Vector2(_pushVelocity * -bodyDirectionSign, rigidbody.velocity.y);
                    pushingObjectsCount++;
                }
                else
                {
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
            }

            bool isPushingSomething = pushingObjectsCount > 0;

            if (isPushingSomething && IsPushing == false)
            {
                _pushingEvent.Toggle(true);
                IsPushing = true;
            }
        }
        else if(IsPushing == true)
        {
            _pushingEvent.Toggle(false);
            IsPushing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _movableLayer && collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            _movableColliderRigidbodyPairs[collision.collider] = rigidbody;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_movableColliderRigidbodyPairs.ContainsKey(collision.collider))
        {
            _movableColliderRigidbodyPairs[collision.collider].velocity = new Vector2(0, _movableColliderRigidbodyPairs[collision.collider].velocity.y);
            _movableColliderRigidbodyPairs.Remove(collision.collider);
        }
    }
}
