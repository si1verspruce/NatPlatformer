using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class TimeScaleChanger
{
    public static event UnityAction Changed;

    public static void Change(float value)
    {
        Time.timeScale = value;
        Changed?.Invoke();
    }
}
