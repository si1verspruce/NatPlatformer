using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAudio : MonoBehaviour
{
    [SerializeField] protected PlayerJump JumpComponentsObject;
    [SerializeField] protected AudioSource Source;

    private PlayerJump[] _characterJumps;

    private void Awake()
    {
        _characterJumps = JumpComponentsObject.GetComponents<PlayerJump>();
    }

    private void OnEnable()
    {
        foreach (PlayerJump jump in _characterJumps)
            jump.JumpCompleted += PlayJumpSound;
    }

    private void OnDisable()
    {
        Source.Stop();

        foreach (PlayerJump jump in _characterJumps)
            jump.JumpCompleted -= PlayJumpSound;
    }

    private void PlayJumpSound()
    {
        Source.Play();
    }
}
