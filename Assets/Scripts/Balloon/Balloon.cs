using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Balloon : MonoBehaviour
{
    private Animator animator;
    public Vector3 startingScale;
    public readonly float minScale = .01f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        startingScale = transform.localScale;
    }
    
    public void Shrink(float targetScale)
    {
        if(transform.localScale.x >= minScale) 
        {
            transform.localScale = new(targetScale, targetScale, targetScale);
        }
        else {
            Debug.LogError("Cannot shrink balloon any more. It should POP!");
        }
    }

    public void Inflate(float targetScale)
    {
        if(transform.localScale.x < startingScale.x) 
        {
            transform.localScale = new(targetScale, targetScale, targetScale);
        }
        else {
            transform.localScale = startingScale;
        }
    }

    public void Pop()
    {
        Debug.Log("Balloon POPPED!");
        StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        transform.localScale = startingScale * 1.2f;
        animator.Play("ExplodeBalloon");
        yield return new WaitForSeconds(.5f);
        Destroy(this);
    }
}
