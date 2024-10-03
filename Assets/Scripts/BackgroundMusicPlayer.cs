using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource _audioPlayer;
    private VideoPlayer[] _videoPlayers;

    private void Awake()
    {
        _videoPlayers = FindObjectsOfType<VideoPlayer>(true);
        _audioPlayer = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (VideoPlayer player in _videoPlayers)
        {
            player.started += OnVideoStarted;
            player.loopPointReached += OnVideoFinished;
        }
    }

    private void OnDisable()
    {
        foreach (VideoPlayer player in _videoPlayers)
        {
            player.started -= OnVideoStarted;
            player.loopPointReached -= OnVideoFinished;
        }
    }

    private void OnVideoStarted(VideoPlayer player)
    {
        _audioPlayer.Pause();
    }

    private void OnVideoFinished(VideoPlayer player)
    {
        _audioPlayer.Play();
    }
}
