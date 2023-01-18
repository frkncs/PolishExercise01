using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private enum AnimationState
    {
        MoveIdle,
        MoveLeft,
        MoveRight,
        Jump,
        Land
    }
    
    #region Variables
    
    // Public Variables

    // Private Variables
    [SerializeField] private float moveRightLeftBlendShapeDuration = .3f;
    
    private Animator _animator;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private AnimationState _animationState;
    private Coroutine _moveAnimCor;
    
    private readonly int JumpAnim = Animator.StringToHash("Jump");
    private readonly int LandAnim = Animator.StringToHash("Land");

    private const float AnimationCrossFadeDuration = .15f;
    private const int MaxMoveBsWeight = 30;
    
    #endregion Variables

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        SignUpEvents();
    }

    private void SignUpEvents()
    {
        PlayerMovement.IdleEvent += PlayMoveIdleAnim;
        PlayerMovement.MoveRightEvent += PlayMoveRightAnim;
        PlayerMovement.MoveLeftEvent += PlayMoveLeftAnim;
        PlayerMovement.JumpEvent += PlayJumpAnim;
        PlayerMovement.LandEvent += PlayLandAnim;
    }

    private void PlayMoveIdleAnim()
    {
        if (_animationState == AnimationState.MoveIdle)
        {
            return;
        }
        
        if (_moveAnimCor != null)
        {
            StopCoroutine(_moveAnimCor);
        }

        ResetMoveBlendShapes(.1f);
        _animationState = AnimationState.MoveIdle;
    }

    private void ResetMoveBlendShapes(float duration)
    {
        if (_skinnedMeshRenderer.GetBlendShapeWeight(0) == 0)
        {
            _moveAnimCor = StartCoroutine(SetBlendShapeCor(1, 0, duration));
        }
        else if (_skinnedMeshRenderer.GetBlendShapeWeight(1) == 0)
        {
            _moveAnimCor = StartCoroutine(SetBlendShapeCor(0, 0, duration));
        }
    }

    private void PlayMoveRightAnim()
    {
        PlayMoveAnim(0, AnimationState.MoveRight);
    }

    private void PlayMoveLeftAnim()
    {
        PlayMoveAnim(1, AnimationState.MoveLeft);
    }

    private void PlayMoveAnim(int index, AnimationState animationState)
    {
        if (_animationState == animationState)
        {
            return;
        }

        if (_moveAnimCor != null)
        {
            StopCoroutine(_moveAnimCor);
        }
        
        _skinnedMeshRenderer.SetBlendShapeWeight(index == 0 ? 1 : 0, 0);
        _moveAnimCor = StartCoroutine(SetBlendShapeCor(index, MaxMoveBsWeight, moveRightLeftBlendShapeDuration));
        
        _animationState = animationState;
    }
    
    private void PlayJumpAnim() => PlayAnim(JumpAnim, AnimationState.Jump);
    private void PlayLandAnim() => PlayAnim(LandAnim, AnimationState.Land);

    private void PlayAnim(int animName, AnimationState animationState)
    {
        if (animationState == _animationState)
        {
            return;
        }
        
        _animator.CrossFade(animName, AnimationCrossFadeDuration);
        _animationState = animationState;
    }

    private IEnumerator SetBlendShapeCor(int index, int endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = _skinnedMeshRenderer.GetBlendShapeWeight(index);
        
        while (elapsedTime < duration)
        {
            var value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            
            _skinnedMeshRenderer.SetBlendShapeWeight(index, value);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
            
        _skinnedMeshRenderer.SetBlendShapeWeight(index, endValue);
    }
}
