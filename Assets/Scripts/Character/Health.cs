using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IEventInvoker
{
    public event UnityAction Died;
    public event UnityAction Event;

    public void ApplyDamage()
    {
        Died?.Invoke();
        Event?.Invoke();
    }
}
