using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private BackgroundLayer[] _layers;

    private void Start()
    {
        foreach (BackgroundLayer layer in _layers)
            layer.Init();
    }

    private void Update()
    {
        foreach (BackgroundLayer layer in _layers)
            layer.MoveX(_playerCamera.transform.position.x);
    }

    private void LateUpdate()
    {
        foreach (BackgroundLayer layer in _layers)
            layer.MoveY(_playerCamera.transform.position.y);
    }

    [Serializable]
    private class BackgroundLayer
    {
        [SerializeField] private Transform _layer;
        [SerializeField] private float _offsetMultiplier;

        private Vector3 _scaledSize;
        private float _startPositionX;

        public void Init()
        {
            _scaledSize = _layer.GetComponent<SpriteRenderer>().size * _layer.localScale;
            _startPositionX = _layer.position.x;
            Transform leftLayer = Instantiate(_layer);
            Transform rightLayer = Instantiate(_layer);
            BindToLayer(leftLayer, -1);
            BindToLayer(rightLayer, 1);
        }

        public void MoveX(float positionX)
        {
            _layer.position = new Vector3(_startPositionX - (positionX - _startPositionX) * _offsetMultiplier, _layer.position.y, 0);
            float distanceFromCameraX = positionX - _layer.position.x;

            if (Mathf.Abs(distanceFromCameraX) >= _scaledSize.x)
            {
                _layer.position += distanceFromCameraX * Vector3.right;
                _startPositionX = _layer.position.x;
            }
        }

        public void MoveY(float positionY)
        {
            _layer.position = new Vector3(_layer.position.x, positionY);
        }

        private void BindToLayer(Transform layerToBind, float direction)
        {
            direction = Mathf.Sign(direction);
            layerToBind.transform.parent = _layer;
            layerToBind.localScale = Vector3.one;
            layerToBind.position = _layer.position + direction * _scaledSize.x * Vector3.right;
        }
    }
}
