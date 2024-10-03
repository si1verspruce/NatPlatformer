using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public event UnityAction Activated;
    public event UnityAction Deactivated;

    public void Activate()
    {
        Activated?.Invoke();
    }

    public void Deactivate()
    {
        Deactivated?.Invoke();
    }
}
