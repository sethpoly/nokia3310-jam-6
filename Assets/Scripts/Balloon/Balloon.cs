using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public Vector3 startingScale;
    public readonly float minScale = .01f;

    void Awake()
    {
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
        Destroy(this);
    }
}
