using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _initialInputComponents;

    private InputActions _input;

    public InputAction Move { get; private set; }
    public InputAction Jump { get; private set; }
    public InputAction Climb { get; private set; }
    public InputAction EnterForm1 { get; private set; }
    public InputAction EnterForm2 { get; private set; }
    public InputAction EnterForm3 { get; private set; }
    public InputAction EnterForm4 { get; private set; }
    public InputAction EnterForm5 { get; private set; }

    private void Awake()
    {
        _input = new InputActions();
        _input.Enable();

        Move = _input.Player.Move;
        Jump = _input.Player.Jump;
        Climb = _input.Player.Climb;
        EnterForm1 = _input.Player.EnterForm1;
        EnterForm2 = _input.Player.EnterForm2;
        EnterForm3 = _input.Player.EnterForm3;
        EnterForm4 = _input.Player.EnterForm4;
        EnterForm5 = _input.Player.EnterForm5;

        foreach (MonoBehaviour component in _initialInputComponents)
            component.enabled = true;
    }
}
