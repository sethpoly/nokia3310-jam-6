using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Vector3 startingScale;

    void Awake()
    {
        startingScale = transform.localScale;
    }
    
    public void Shrink(float amount)
    {
        if(transform.localScale.x > 0) 
        {
            transform.localScale = new(transform.localScale.x - amount, transform.localScale.y - amount, transform.localScale.z - amount);
        }
        else {
            Debug.LogError("Cannot shrink balloon any more. It should POP!");
        }
    }

    public void Grow(float amount)
    {
        if(transform.localScale.x < startingScale.x) 
        {
            transform.localScale = new(transform.localScale.x + amount, transform.localScale.y + amount, transform.localScale.z + amount);
        }
        else {
            transform.localScale = startingScale;
        }
    }
}
