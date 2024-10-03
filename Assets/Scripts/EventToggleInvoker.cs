using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventToggleInvoker : MonoBehaviour
{
    public event UnityAction<bool> Toggled;

    public void Toggle(bool isActive)
    {
        Toggled?.Invoke(isActive);
    }
}
