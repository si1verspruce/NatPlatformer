using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AirBar : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _fill;
    [SerializeField] private Breathing _playerBreathing;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        OnAirChanged();

        _playerBreathing.AirChanged += OnAirChanged;
    }

    private void OnDisable()
    {
        _playerBreathing.AirChanged -= OnAirChanged;
    }

    private void OnAirChanged()
    {
        _slider.value = _playerBreathing.CurrentAir / _playerBreathing.MaxAir;
    }
}
