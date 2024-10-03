using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScaleChanger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private readonly Dictionary<Component, float> _componentScalePairs = new();

    private float _defaultGravityScale;

    private void Awake()
    {
        _defaultGravityScale = _rigidbody.gravityScale;
    }

    private void OnDisable()
    {
        _componentScalePairs.Clear();
    }

    public void SetGravityScale(Component component, float value)
    {
        if (component == null)
            return;

        if (value == _defaultGravityScale && _componentScalePairs.ContainsKey(component))
            _componentScalePairs.Remove(component);
        else
            _componentScalePairs[component] = value;

        _rigidbody.gravityScale = FindMinValue(_componentScalePairs);
    }

    private float FindMinValue<K>(Dictionary<K, float> dictionary)
    {
        float minValue = _defaultGravityScale;

        foreach (K key in dictionary.Keys)
            if (dictionary[key] < minValue)
                minValue = dictionary[key];

        return minValue;
    }
}
