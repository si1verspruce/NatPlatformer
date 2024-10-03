using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class OnTriggerEnterCutscenePlayer : MonoBehaviour
{
    [SerializeField] private bool _disableCharacter = true;

    private VideoPlayer _videoPlayer;

    private bool _isTriggered;
    private PlayerCharacter _character;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        _videoPlayer.loopPointReached += StopVideo;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= StopVideo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered == false && collision.TryGetComponent(out PlayerCharacter character))
        {
            _isTriggered = true;
            Time.timeScale = 0;
            _videoPlayer.Play();

            if (_disableCharacter)
            {
                _character = character;
                character.gameObject.SetActive(false);
            }
        }
    }

    private void StopVideo(VideoPlayer videoPlayer)
    {
        StartCoroutine(StopAfterTime());
    }

    private IEnumerator StopAfterTime()
    {
        yield return new WaitForSecondsRealtime(1);

        if (_disableCharacter)
            _character.gameObject.SetActive(true);

        _videoPlayer.Stop();
        Time.timeScale = 1;
    }
}
