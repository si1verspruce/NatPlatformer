using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterAudio : MonoBehaviour
{
    [SerializeField] protected PlayerCharacter[] _forms;
    [SerializeField] protected AudioSource Source;

    private void OnEnable()
    {
        foreach (PlayerCharacter form in _forms)
            form.UnderwaterStatusChanged += SetWalkingSound;
    }

    private void OnDisable()
    {
        if (Source)
            Source.Stop();

        foreach (PlayerCharacter form in _forms)
            form.UnderwaterStatusChanged -= SetWalkingSound;
    }

    private void SetWalkingSound(bool isActive)
    {
        if (isActive && Source.isPlaying == false)
            Source.Play();
        else if (Source.isPlaying)
            Source.Pause();
    }
}
