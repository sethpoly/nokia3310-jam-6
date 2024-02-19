using UnityEngine;
using System;

public class PlayerCollision: MonoBehaviour 
{
    public event Action OnSpikeCollision;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
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
    public Collider2D environmentCollider;

    public Vector2 environmentColliderSize;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, leftOffset, rightOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);

        // Check spike collision
        var envCollison = Physics2D.OverlapBox((Vector2)transform.position, environmentColliderSize, environmentLayer);
        {
            if (envCollison != null) {
                if(envCollison.gameObject.CompareTag("spike"))
                {
                    OnSpikeCollision.Invoke();
                }
            }
        }

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
        leftWallCollider = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);
        rightWallCollider = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer);
        environmentCollider = Physics2D.OverlapBox((Vector2)transform.position, environmentColliderSize, environmentLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireCube((Vector2)transform.position, environmentColliderSize);
    }
}