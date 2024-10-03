using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacer : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private GameOverPanel _panel;
    [SerializeField] private ColliderCallback[] _checkpoints;

    private Transform _activePoint;
    private Rigidbody2D _characterRigidbody;
    private Breathing _characterBreathing;

    private void Awake()
    {
        _characterRigidbody = _character.GetComponent<Rigidbody2D>();
        _characterBreathing = _character.GetComponent<Breathing>();
    }

    private void OnEnable()
    {
        _activePoint = transform;
        _panel.Retried += PlacePlayer;

        foreach (ColliderCallback callback in _checkpoints)
            callback.TriggerEntered += OnCheckpointEntered;

        PlacePlayer();
    }


    private void OnDisable()
    {
        _panel.Retried -= PlacePlayer;

        foreach (ColliderCallback callback in _checkpoints)
            callback.TriggerEntered -= OnCheckpointEntered;
    }

    public void OnCheckpointEntered(GameObject checkpoint, Collider2D collider)
    {
        if (collider.TryGetComponent(out PlayerCharacter _))
            _activePoint = checkpoint.transform;
    }

    public void PlacePlayer()
    {
        _character.transform.position = _activePoint.position;
        _characterRigidbody.velocity = Vector2.zero;
        _characterBreathing.ResetAir();
        _character.SetActive(true);
    }
}
