using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationTime = .5f;

    public IEnumerator LoadLevel(int level, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(animationTime);
        // TODO: Load next scene
    }
}
