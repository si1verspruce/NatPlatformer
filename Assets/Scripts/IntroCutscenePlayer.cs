using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class IntroCutscenePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;

    private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        _videoPlayer.Play();
        TimeScaleChanger.Change(0);
    }

    private void OnEnable()
    {
        _videoPlayer.started += CloseBlackScreen;
        _videoPlayer.loopPointReached += StopVideo;
    }

    private void OnDisable()
    {
        _videoPlayer.started -= CloseBlackScreen;
        _videoPlayer.loopPointReached -= StopVideo;
    }

    private void StopVideo(VideoPlayer videoPlayer)
    {
        StartCoroutine(StopAfterTime());
    }

    private void CloseBlackScreen(VideoPlayer videoPlayer)
    {
        _blackScreen.SetActive(false);
    }

    private IEnumerator StopAfterTime()
    {
        yield return new WaitForSecondsRealtime(1);
        _videoPlayer.gameObject.SetActive(false);
        TimeScaleChanger.Change(1);
    }
}
