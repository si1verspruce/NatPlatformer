using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraSettingsChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _characterCamera;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _deadZoneHeight;

    private CinemachineFramingTransposer _transposer;

    private void Awake()
    {
        _transposer = _characterCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCharacter _))
        {
            Transit();
        }
    }

    private void Transit()
    {
        if (_transposer.m_TrackedObjectOffset != _offset)
            _transposer.m_TrackedObjectOffset = _offset;

        if (_transposer.m_DeadZoneHeight != _deadZoneHeight)
        {
            _transposer.m_DeadZoneHeight = 0;
            StartCoroutine(DoNextFrame());
        }
    }

    private IEnumerator DoNextFrame()
    {
        yield return new WaitForFixedUpdate();
        yield return null;

        _transposer.m_DeadZoneHeight = _deadZoneHeight;
    }
}
