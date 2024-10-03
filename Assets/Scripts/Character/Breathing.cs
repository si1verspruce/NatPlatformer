using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breathing : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _maxAir;
    [SerializeField] private float _airRecoveryRate;
    [SerializeField] private float _airReductionRate;

    private float _air;
    private bool _isBreathing = true;
    private Coroutine _activeCoroutine;

    public event UnityAction AirChanged;

    public float CurrentAir => _air;
    public float MaxAir => _maxAir;

    private float Air
    {
        set
        {
            _air = value;
            AirChanged?.Invoke();
        }
    }

    private void Awake()
    {
        Air = _maxAir;
    }

    private void OnDisable()
    {
        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);
    }

    public void SetBreathing(bool isBreathing)
    {
        if (gameObject.scene.isLoaded == false)
            return;

        if (_isBreathing != isBreathing)
        {
            _isBreathing = isBreathing;

            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);

            if (isBreathing)
                _activeCoroutine = StartCoroutine(RecoverAir());
            else
                _activeCoroutine = StartCoroutine(ReductAir());
        }
    }

    public void ResetAir()
    {
        Air = _maxAir;
    }

    private IEnumerator RecoverAir()
    {
        while (_air < _maxAir)
        {
            ChangeAir(_airRecoveryRate * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator ReductAir()
    {
        while (_air > 0)
        {
            ChangeAir(-_airReductionRate * Time.deltaTime);

            yield return null;
        }

        _health.ApplyDamage();
    }

    private void ChangeAir(float value)
    {
        Air = Mathf.Clamp(_air + value, 0, _maxAir);
    }
}
