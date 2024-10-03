using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _leftTargetPoint;
    [SerializeField] private Transform _rightTargetPoint;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isMovingRight;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _maxSoundDelay;

    private int _direction;

    private void Awake()
    {
        _direction = _isMovingRight ? 1 : -1;
        _renderer.flipX = _direction == 1;
        StartCoroutine(PlayAudio());
    }

    private void Update()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime * Vector2.right);

        if (transform.position.x > _rightTargetPoint.position.x)
        {
            _direction = -1;
            _renderer.flipX = false;
        }
        else if (transform.position.x < _leftTargetPoint.position.x)
        {
            _direction = 1;
            _renderer.flipX = true;
        }
    }

    private IEnumerator PlayAudio()
    {
        float delay = Random.Range(0, _maxSoundDelay);

        yield return new WaitForSeconds(delay);

        _audio.Play();
    }
}
