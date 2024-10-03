using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private GameObject _theEndText;
    [SerializeField] private GameObject _backgroundMusicPlayer;
    [SerializeField] private VideoPlayer _lastCutscenePlayer;

    private void OnEnable()
    {
        _lastCutscenePlayer.loopPointReached += OnFinishCutsceneEnded;
    }

    private void OnDisable()
    {
        _lastCutscenePlayer.loopPointReached -= OnFinishCutsceneEnded;
    }

    private void OnFinishCutsceneEnded(VideoPlayer videoPlayer)
    {
        _blackScreen.SetActive(true);
        _theEndText.SetActive(true);
        _backgroundMusicPlayer.SetActive(false);
    }
}
