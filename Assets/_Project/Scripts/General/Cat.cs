using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cat : MonoBehaviour
{
    [SerializeField] private UnityEvent _onFinishAnimation;
    [SerializeField] private Animator _catAnimator;
    
    private float GetAnimationDuration(string animationName)
    {
        float length = -1;
        AnimationClip[] clips = _catAnimator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == animationName)
            {
                length = clip.length;
                break;
            }
        }
        return  length;
    }

    public void WaitForFinishAnimation(string animationName)
    {
        StopAllCoroutines();
        StartCoroutine(AnimationTimer(animationName));
    }

    public IEnumerator AnimationTimer(string animationName)
    {
        Debug.Log("animation length");
        Debug.Log(GetAnimationDuration(animationName));
        yield return new WaitForSeconds(GetAnimationDuration(animationName));
        _onFinishAnimation?.Invoke();
    }

}
