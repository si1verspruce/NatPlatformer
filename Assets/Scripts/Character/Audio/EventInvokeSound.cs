using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInvokeSound : MonoBehaviour
{
    [SerializeField] private GameObject _eventInvokeObject;
    [SerializeField] private AudioSource _source;

    private IEventInvoker _invoker;

    private void Awake()
    {
        _invoker = _eventInvokeObject.GetComponent<IEventInvoker>();
    }

    private void OnEnable()
    {
        _invoker.Event += OnEventInvoke;
    }

    private void OnDisable()
    {
        _invoker.Event -= OnEventInvoke;
    }

    private void OnEventInvoke()
    {
        _source.Play();
    }
}
