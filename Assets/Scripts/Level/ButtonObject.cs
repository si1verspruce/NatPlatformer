using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ButtonObject : MonoBehaviour, IEventInvoker
{
    [SerializeField] private Activator _activator;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _buttonPressed;

    private bool _isActivated;

    public event UnityAction Event;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActivated == false)
        {
            if (collision.TryGetComponent(out PlayerCharacter character))
            {
                if (character.Form == CharacterForm.Humanoid)
                {
                    _isActivated = true;
                    Event?.Invoke();
                    _activator.Activate();
                    _button.SetActive(false);
                    _buttonPressed.SetActive(true);
                }
            }
        }
    }
}
