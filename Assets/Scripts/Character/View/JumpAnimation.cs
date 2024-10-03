using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class JumpAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _jumpComponentsObject;
    [SerializeField] private string _animatorJumpedTriggerName;

    private PlayerJump[] _jumpComponents;
    private CharacterView _characterView;
    private int _animatorJumpedTrigger;

    private void Awake()
    {
        _characterView = GetComponent<CharacterView>();
        _jumpComponents = _jumpComponentsObject.GetComponents<PlayerJump>();
        _animatorJumpedTrigger = Animator.StringToHash(_animatorJumpedTriggerName);
    }

    private void OnEnable()
    {
        foreach (var jumpComponent in _jumpComponents)
        {
            jumpComponent.JumpStarted += ShowJump;
            jumpComponent.DetectionUnlocked += UnlockDetection;
        }
    }

    private void OnDisable()
    {
        foreach (var jumpComponent in _jumpComponents)
        {
            jumpComponent.JumpStarted -= ShowJump;
            jumpComponent.DetectionUnlocked -= UnlockDetection;
        }
    }

    public void ShowJump()
    {
        _characterView.SetDetectionActive(false);
        _characterView.CharacterAnimator.SetTrigger(_animatorJumpedTrigger);
    }

    public void UnlockDetection()
    {
        _characterView.SetDetectionActive(true);
    }
}
