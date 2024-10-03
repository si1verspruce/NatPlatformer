using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderDetection : MonoBehaviour
{
    [SerializeField] private LayerMask _detectionMask;
    [SerializeField] private Detection _bottomDetection;
    [SerializeField] private Detection _leftDetection;
    [SerializeField] private Detection _topDetection;
    [SerializeField] private Detection _rightDetection;

    private readonly Dictionary<DirectionCode, Detection> _parametersByCode = new();

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        _parametersByCode[DirectionCode.Top] = _topDetection;
        _parametersByCode[DirectionCode.Right] = _rightDetection;
        _parametersByCode[DirectionCode.Bottom] = _bottomDetection;
        _parametersByCode[DirectionCode.Left] = _leftDetection;

        _bottomDetection.Init(DetectionOrientation.Vertical, -1);
        _leftDetection.Init(DetectionOrientation.Horizontal, -1);
        _topDetection.Init(DetectionOrientation.Vertical, 1);
        _rightDetection.Init(DetectionOrientation.Horizontal, 1);
    }

    public bool CheckForColliders(DirectionCode direction)
    {
        return Detect(_parametersByCode[direction]);
    }

    private bool Detect(Detection parameters)
    {
        if (Time.frameCount != parameters.LastFrameActivation)
        {
            parameters.LastFrameActivation = Time.frameCount;

            Vector2 boxSize;
            Vector2 castPoint;

            if (parameters.Orientation == DetectionOrientation.Vertical)
            {
                castPoint = GetCastPoint(parameters.DetectionDistance, _collider.bounds.extents.y, Vector3.up, parameters.Direction);
                boxSize = new(_collider.bounds.extents.x * 2 * parameters.DetectionWidth, parameters.DetectionDistance);
            }
            else
            {
                castPoint = GetCastPoint(parameters.DetectionDistance, _collider.bounds.extents.x, Vector3.right, parameters.Direction);
                boxSize = new(parameters.DetectionDistance, _collider.bounds.extents.y * 2 * parameters.DetectionWidth);
            }

            Collider2D[] overlappedColliders = Physics2D.OverlapBoxAll(castPoint, boxSize, 0, _detectionMask);

            foreach (Collider2D collider in overlappedColliders)
            {
                if (collider != _collider)
                {
                    if (collider.isTrigger == false)
                    {
                        parameters.LastValue = true;

                        return true;
                    }
                }
            }

            parameters.LastValue = false;

            return false;
        }
        else
        {
            return parameters.LastValue;
        }
    }

    private Vector2 GetCastPoint(float castDistance, float colliderExtent, Vector3 orientation, float direction)
    {
        direction = Mathf.Sign(direction);
        float castOffset = (colliderExtent + castDistance / 2) * direction;
        Vector2 castPoint = _collider.bounds.center + orientation * castOffset;

        return castPoint;
    }

    [Serializable]
    private class Detection
    {
        public float DetectionDistance;
        [Range(0, 2)] public float DetectionWidth;

        [HideInInspector] public DetectionOrientation Orientation;
        [HideInInspector] public float Direction;
        [HideInInspector] public int LastFrameActivation;
        [HideInInspector] public bool LastValue;

        public void Init(DetectionOrientation orientation, float direction)
        {
            Orientation = orientation;
            Direction = direction;
        }
    }

    private enum DetectionOrientation
    {
        Vertical,
        Horizontal
    }
}
