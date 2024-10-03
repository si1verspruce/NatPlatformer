using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderCallback : MonoBehaviour
{
    public event UnityAction<GameObject, Collider2D> TriggerEntered;
    public event UnityAction<GameObject, Collider2D> TriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEntered?.Invoke(gameObject, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit?.Invoke(gameObject,collision);
    }
}
