using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour, IDamageable, IBreathing
{
    [SerializeField] private CharacterForm _form;
    [SerializeField] private CharacterFormChanger _changer;
    [SerializeField] private Health _characterHealth;
    [SerializeField] private Breathing _characterBreathing;
    [SerializeField] private bool _isUnderwaterBreathing;

    public event UnityAction<bool> UnderwaterStatusChanged;

    public bool IsUnderwater { get; private set; }

    public CharacterForm Form => _form;
    public CharacterFormChanger FormChanger => _changer;
    public bool IsUnderwaterBreathing => _isUnderwaterBreathing;

    private void OnEnable()
    {
        SetUnderwaterStatus(false);
    }

    public void ApplyDamage()
    {
        _characterHealth.ApplyDamage();
    }

    public void SetUnderwaterStatus(bool isUnderWater)
    {
        IsUnderwater = isUnderWater;
        UnderwaterStatusChanged?.Invoke(isUnderWater);
        _characterBreathing.SetBreathing(_isUnderwaterBreathing == isUnderWater);
    }
}
