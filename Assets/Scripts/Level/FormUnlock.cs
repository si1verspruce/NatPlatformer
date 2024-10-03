using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormUnlock : MonoBehaviour
{
    [SerializeField] private GameObject _toUnlock;
    [SerializeField] private bool _morphImmediately = true;

    private bool _isActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isActivated == false && collision.TryGetComponent(out PlayerCharacter character))
        {
            _isActivated = true;
            character.FormChanger.UnlockForm(_toUnlock);

            if (_morphImmediately)
                character.FormChanger.EnterForm(_toUnlock);
        }
    }
}
