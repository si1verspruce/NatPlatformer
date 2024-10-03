using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventToggleSound : MonoBehaviour
{
    [SerializeField] private EventToggleInvoker _eventToggle;
    [SerializeField] private AudioSource _source;

    private void OnEnable()
    {
        _eventToggle.Toggled += OnEventInvoke;
    }

    private void OnDisable()
    {
        _source.Stop();
        _eventToggle.Toggled -= OnEventInvoke;
    }

    private void OnEventInvoke(bool isActive)
    {
        if (isActive && _source.isPlaying == false)
            _source.Play();
        else if (_source.isPlaying)
            _source.Stop();
    }
}
