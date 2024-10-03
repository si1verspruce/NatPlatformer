using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Health _characterHealth;

    private Transform[] _objects;

    public event UnityAction Retried;

    private void Awake()
    {
        _objects = transform.GetComponentsInChildren<Transform>(true).Skip(1).ToArray();
    }

    private void OnEnable()
    {
        _characterHealth.Died += OnCharacterDied;
    }

    private void OnDisable()
    {
        _characterHealth.Died -= OnCharacterDied;
    }

    private void OnCharacterDied()
    {
        TimeScaleChanger.Change(0);

        foreach (Transform gameObject in _objects)
            gameObject.gameObject.SetActive(true);
    }

    public void Retry()
    {
        TimeScaleChanger.Change(1);

        foreach (Transform gameObject in _objects)
            gameObject.gameObject.SetActive(false);

        Retried?.Invoke();
    }
}
