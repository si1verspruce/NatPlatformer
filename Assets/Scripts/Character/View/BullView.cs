using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullView : CharacterView
{
    [SerializeField] private string _animatorIsPushingName;
    [SerializeField] private BullPush _push;

    private int _animatorIsPushing;

    protected override void Awake()
    {
        base.Awake();
        _animatorIsPushing = Animator.StringToHash(_animatorIsPushingName);
    }

    protected override void OnUpdate()
    {
        CharacterAnimator.SetBool(_animatorIsPushing, _push.IsPushing);
    }
}
