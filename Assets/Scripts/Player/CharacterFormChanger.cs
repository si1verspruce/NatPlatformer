using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterFormChanger : MonoBehaviour, IEventInvoker
{
    [SerializeField] private Transform _formsContainer;
    [SerializeField] private GameObject _startForm;

    private readonly GameObject[] _forms = new GameObject[5];
    private readonly InputAction[] _inputActions = new InputAction[5];
    private readonly Action<InputAction.CallbackContext>[] _enterFormActions = new Action<InputAction.CallbackContext>[5];
    private readonly Dictionary<GameObject, bool> _formAvailabilityPairs = new();

    private PlayerInput _input;
    private GameObject _currentForm;
    private bool _isInitialized;

    public event UnityAction Event;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();

        for (int i = 0; i < _formsContainer.childCount; i++)
            _forms[i] = _formsContainer.GetChild(i).gameObject;

        _enterFormActions[0] = x => TryEnterForm(_forms[0]);
        _enterFormActions[1] = x => TryEnterForm(_forms[1]);
        _enterFormActions[2] = x => TryEnterForm(_forms[2]);
        _enterFormActions[3] = x => TryEnterForm(_forms[3]);
        _enterFormActions[4] = x => TryEnterForm(_forms[4]);

        foreach (GameObject form in _forms)
            if (form)
                _formAvailabilityPairs[form] = false;

        _formAvailabilityPairs[_forms[0]] = true;
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
        {
            _inputActions[0] = _input.EnterForm1;
            _inputActions[1] = _input.EnterForm2;
            _inputActions[2] = _input.EnterForm3;
            _inputActions[3] = _input.EnterForm4;
            _inputActions[4] = _input.EnterForm5;

            _isInitialized = true;
        }

        for (int i = 0; i < _inputActions.Length; i++)
            _inputActions[i].started += _enterFormActions[i];
    }

    private void OnDisable()
    {
        for (int i = 0; i < _inputActions.Length; i++)
            _inputActions[i].started -= _enterFormActions[i];
    }

    private void Start()
    {
        EnterForm(_startForm);
    }

    public void EnterForm(GameObject form)
    {
        if (form == null)
            return;

        if (_currentForm != form && _formAvailabilityPairs[form])
        {
            if (_currentForm)
            {
                _currentForm.SetActive(false);
                form.transform.SetPositionAndRotation(_currentForm.transform.position, _currentForm.transform.rotation);
            }

            Event?.Invoke();
            form.SetActive(true);
            _currentForm = form;
        }
    }

    public void UnlockForm(GameObject form)
    {
        if (_formAvailabilityPairs.ContainsKey(form))
            _formAvailabilityPairs[form] = true;
    }

    private void TryEnterForm(GameObject form)
    {
        if (Time.timeScale != 0)
            EnterForm(form);
    }
}
