using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour
{
    [SerializeField] protected CharacterView FormView;
    [SerializeField] protected AudioSource Source;

    private void OnEnable()
    {
        FormView.WalkingActivityChanged += SetWalkingSound;
    }

    private void OnDisable()
    {
        Source.Stop();

        FormView.WalkingActivityChanged -= SetWalkingSound;
    }

    private void SetWalkingSound(bool isActive)
    {
        if (isActive && Source.isPlaying == false)
            Source.Play();
        else if (Source.isPlaying)
            Source.Stop();
    }
}
