using UnityEngine;
using System;

public class PlayerCollision: MonoBehaviour 
{
    public event Action OnSpikeCollision;
    public event Action OnDoorCollision;
    public event Action<Coin> OnCoinCollision;

    [Header("Layers")]
    public LayerMask groundLayer;
    //public LayerMask wallLayer;
    public LayerMask environmentLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public Wall wallSide;

    [Space]

    [HideInInspector]
    [Header("Collision")]
    public Collider2D groundCollider;
    public Collider2D leftWallCollider;
    public Collider2D rightWallCollider;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, leftOffset, rightOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        if(onRightWall)
        {
            wallSide = Wall.right;
        } else if (onLeftWall)
        {
            wallSide = Wall.left;
        } else {
            wallSide = Wall.none;
        }

        // Get other collider object from each collision circle
        groundCollider = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        leftWallCollider = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        rightWallCollider = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Spikes
        if(other.gameObject.CompareTag("spike"))
        {
            OnSpikeCollision.Invoke();
        }

        // Level exit
        if(other.gameObject.CompareTag("Door")) 
        {
            OnDoorCollision.Invoke();
        }

        // Coins
        if(other.gameObject.CompareTag("Coin"))
        {
            OnCoinCollision.Invoke(other.GetComponent<Coin>());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}