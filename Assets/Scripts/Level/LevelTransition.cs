using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour, IEventInvoker
{
    [SerializeField] private Level _previousLevel;
    [SerializeField] private Level _nextLevel;
    [SerializeField] private Image _darkeningScreen;
    [SerializeField] private float _darkeningDuration;
    [SerializeField] private float _transitionDuration;

    public event UnityAction Event;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCharacter _))
        {
            Event?.Invoke();
            StartCoroutine(Transit());
        }
    }

    private IEnumerator Transit()
    {
        Tween darkeningScreen = _darkeningScreen.DOColor(Color.black, _darkeningDuration);

        yield return darkeningScreen.WaitForCompletion();

        _nextLevel.Launch();
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(_transitionDuration);

        Time.timeScale = 1;
        _previousLevel.Exit();

        _darkeningScreen.DOColor(Color.clear, _darkeningDuration);
    }
}
