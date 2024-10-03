using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Activator))]
public class Door : MonoBehaviour
{
    [SerializeField] private MovingPart _topPart;
    [SerializeField] private MovingPart _bottomPart;

    private Activator _activator;
    private bool _isOpen;

    private void Awake()
    {
        _activator = GetComponent<Activator>();
    }

    private void OnEnable()
    {
        _activator.Activated += Activate;
    }

    private void OnDisable()
    {
        _activator.Activated -= Activate;
    }

    public void Activate()
    {
        if (_isOpen == false)
        {
            _isOpen = true;
            _topPart.Move(1);
            _bottomPart.Move(-1);
        }
    }

    [Serializable]
    private class MovingPart
    {
        public Transform Transform;
        public float MovingTime;
        public float MovingDistance;

        public void Move(int direction)
        {
            int sign = Math.Sign(direction);
            Transform.DOMoveY(Transform.position.y + MovingDistance * sign, MovingTime);
        }
    }
}
