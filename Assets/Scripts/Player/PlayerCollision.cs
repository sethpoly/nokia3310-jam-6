using UnityEngine;
using System;

public class PlayerCollision: MonoBehaviour 
{
    public event Action OnSpikeCollision;
    // public event Action OnGroundCollisionEnter;
    // public event Action OnGroundCollisionExit;

    // void OnCollisionEnter2D(Collision2D collisionInfo)
    // {
    //     if(collisionInfo.gameObject.CompareTag("spike")) 
    //     {
    //         OnSpikeCollision.Invoke();
    //         return;
    //     }

    //     if(collisionInfo.gameObject.CompareTag("Ground"))
    //     {
    //         OnGroundCollisionEnter.Invoke();
    //         return;
    //     }
    // }

    // void OnCollisionExit2D(Collision2D other)
    // {
    //     if(other.gameObject.CompareTag("Ground"))
    //     {
    //         OnGroundCollisionExit.Invoke();
    //         return;
    //     }
    // }


    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;

    [Space]

    [HideInInspector]
    [Header("Collision")]
    public Collider2D groundCollider;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
      

        // Get other collider object from each collision circle
        groundCollider = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
    
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("spike")) 
        {
            OnSpikeCollision.Invoke();
            return;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }
}