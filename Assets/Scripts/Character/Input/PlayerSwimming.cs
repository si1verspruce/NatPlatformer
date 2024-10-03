using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimming : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Swimming _swimming;

    private void FixedUpdate()
    {
        Vector2 direction = new(_input.Move.ReadValue<float>(), _input.Climb.ReadValue<float>());
        _swimming.Swim(direction);
    }
}
