using UnityEngine;
using System;

public class PlayerCollision: MonoBehaviour 
{
    public event Action OnSpikeCollision;
    public event Action<Door> OnDoorCollision;
    public event Action<Coin> OnCoinCollision;

    [Header("Layers")]
    public LayerMask groundLayer;
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

    public Vector2 groundCollisionSize, wallCollisionSize;
    public Vector2 bottomOffset, leftOffset, rightOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundCollisionSize, 0, groundLayer);
        onWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, wallCollisionSize, 0, groundLayer)
            || Physics2D.OverlapBox((Vector2)transform.position + leftOffset, wallCollisionSize,0,  groundLayer);

        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, wallCollisionSize, 0, groundLayer);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, wallCollisionSize, 0, groundLayer);

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
        groundCollider = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, groundCollisionSize, 0, groundLayer);
        leftWallCollider = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, wallCollisionSize, 0, groundLayer);
        rightWallCollider = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, wallCollisionSize, 0, groundLayer);
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
            OnDoorCollision.Invoke(other.GetComponent<Door>());
        }

        // Coins
        if(other.gameObject.CompareTag("Coin"))
        {
            OnCoinCollision.Invoke(other.GetComponentInChildren<Coin>());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugCollisionColor;

        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, groundCollisionSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, wallCollisionSize);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, wallCollisionSize);
    }
}