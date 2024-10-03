using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _followTo;

    private void FixedUpdate()
    {
        transform.position = (Vector2)_followTo.position;
    }
}
