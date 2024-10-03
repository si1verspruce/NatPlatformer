using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirtualCameraOffsetTuning : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _detectionOffsetStepX;
    [SerializeField, Min(1)] private int _detectionPointCount;
    [SerializeField] private float _hitToDownBorderDistance;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private float _offsetTransitDuration;

    private CinemachineFramingTransposer _transposer;
    private float _startOffsetY;
    private ContactFilter2D _contactFilter = new();
    private float _targetOffset;
    private Coroutine _activeOffsetTransition;

    private void Awake()
    {
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _startOffsetY = _transposer.m_TrackedObjectOffset.y;
        _contactFilter.layerMask = _raycastMask;
        _contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        List<float> groundToBorderDistanceList = new();
        int startPointIndex = -_detectionPointCount / 2;

        for (int i = startPointIndex; i < startPointIndex + _detectionPointCount; i++)
        {
            float offsetX = i * _detectionOffsetStepX;
            float groundToBorderDistance = GetGroundToCharacterBorderDistance(offsetX);
            groundToBorderDistanceList.Add(groundToBorderDistance); 
        }

        float minCharacterToGroundDistance = _transposer.FollowTarget.position.y - groundToBorderDistanceList.Min();
        float newOffset = Mathf.Clamp((_startOffsetY - minCharacterToGroundDistance), _minOffset, _maxOffset);

        if (newOffset != _targetOffset && (newOffset == _minOffset || newOffset == _maxOffset))
        {
            if (_activeOffsetTransition != null)
                StopCoroutine(_activeOffsetTransition);

            _targetOffset = newOffset;
            StartCoroutine(MoveToTargetOffset());
        }
    }

    private float GetGroundToCharacterBorderDistance(float offsetX, float defaultValue = 0)
    {
        Vector2 rayOrigin = new(_virtualCamera.transform.position.x + offsetX, _transposer.FollowTarget.position.y);

        if (Physics2D.OverlapPoint(rayOrigin, _raycastMask.value) == null)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];
            Physics2D.Raycast(rayOrigin, Vector2.down, _contactFilter, hits);
            Debug.DrawLine(rayOrigin, hits[0].point, Color.red, Time.deltaTime);

            return hits[0].point.y;
        }

        return defaultValue;
    }

    private IEnumerator MoveToTargetOffset()
    {
        float step = Mathf.Abs((_targetOffset - _transposer.m_TrackedObjectOffset.y) / _offsetTransitDuration);

        while (_transposer.m_TrackedObjectOffset.y != _targetOffset)
        {
            _transposer.m_TrackedObjectOffset = Mathf.MoveTowards(_transposer.m_TrackedObjectOffset.y, _targetOffset, step * Time.deltaTime) * Vector3.up;

            yield return null;
        }

        Debug.Log("offset transition complete");
    }
}
