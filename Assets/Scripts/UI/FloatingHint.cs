using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingHint : MonoBehaviour
{
    [SerializeField] private ColliderCallback callback;
    [SerializeField] private TextMeshProUGUI _hint;

    private void OnEnable()
    {
        callback.TriggerEntered += Show;
        callback.TriggerExit += Hide;
    }

    private void OnDisable()
    {
        callback.TriggerEntered -= Show;
        callback.TriggerExit -= Hide;
    }

    private void Show(GameObject gameObject, Collider2D collider)
    {
        if (collider)
            if (collider.TryGetComponent(out PlayerCharacter _))
                _hint.gameObject.SetActive(true);
    }

    private void Hide(GameObject gameObject, Collider2D collider)
    {
        if (collider)
            if (collider.TryGetComponent(out PlayerCharacter _))
                _hint.gameObject.SetActive(false);
    }
}
