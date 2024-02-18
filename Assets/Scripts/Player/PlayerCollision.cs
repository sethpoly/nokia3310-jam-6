using UnityEngine;
using System;

public class PlayerCollision: MonoBehaviour 
{
    public event Action OnSpikeCollision;

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "spike") 
        {
            OnSpikeCollision.Invoke();
        }
    }
}