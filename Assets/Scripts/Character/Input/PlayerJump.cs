using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ColliderDetection))]
public abstract class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Rigidbody2D _characterRigidbody;
    [SerializeField] private ColliderDetection _characterColliderDetection;
    [SerializeField] private float _startVelocity;
    [SerializeField] private float _delay;
    [SerializeField] private float _timeToLockDetection;
    [SerializeField] private PlayerMovement _movement;

    protected bool IsDetectionLocked;

    private Coroutine _lockDetectionCoroutine;
    private Coroutine _jumpCoroutine;

    public float StartVelocity => _startVelocity;

    public event UnityAction JumpStarted;
    public event UnityAction JumpCompleted;
    public event UnityAction DetectionUnlocked;

    protected PlayerInput Input => _input;
    protected Rigidbody2D CharacterRigidbody => _characterRigidbody;
    protected ColliderDetection ColliderDetection => _characterColliderDetection;

    protected virtual void OnEnable()
    {
        _input.Jump.started += StartJump;
    }

    protected virtual void OnDisable()
    {
        _input.Jump.started -= StartJump;

        if (_lockDetectionCoroutine != null)
        {
            IsDetectionLocked = false;
            StopCoroutine(_lockDetectionCoroutine);
        }

        if (_jumpCoroutine != null)
            StopCoroutine(_jumpCoroutine);
    }

    protected abstract bool TryJump();
    protected abstract void OnJump();

    protected void StartLockDetection()
    {
        _lockDetectionCoroutine = StartCoroutine(LockDetection());
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        if (TryJump())
            _jumpCoroutine = StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        JumpStarted?.Invoke();

        if (_delay > 0)
            yield return new WaitForSeconds(_delay);

        JumpCompleted?.Invoke();
        OnJump();
    }

    private IEnumerator LockDetection()
    {
        IsDetectionLocked = true;
        _movement.enabled = false;

        yield return new WaitForSeconds(_timeToLockDetection);

        IsDetectionLocked = false;
        _movement.enabled = true;
        DetectionUnlocked?.Invoke();
    }
}
