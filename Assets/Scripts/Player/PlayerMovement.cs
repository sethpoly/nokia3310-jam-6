using UnityEngine;

public class PlayerMovement: MonoBehaviour 
{
    [SerializeField] private float fallSpeed = 1100f;
    [SerializeField] private float horizontalSpeed = 100f;
    private float activeSlowDownMultiplier = 0f;
    private float additionalPassiveSlowdown = 0f;
    private Rigidbody2D rb;
    private TouchControls touchControls;
 
    private float horizontal;
    private readonly float vertical = -1;

    private bool onGround = false;
    private Wall onWall = Wall.none;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        touchControls = FindObjectOfType<TouchControls>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector2(GetHorizontalSpeed(), GetFallSpeed());
        move = Time.fixedDeltaTime * move;
        rb.velocity = move;
    }

    void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private float GetFallSpeed()
    {
        float multipler = activeSlowDownMultiplier + 1;
        return onGround ? 0 : vertical * fallSpeed / multipler + additionalPassiveSlowdown;
    }

    private float GetHorizontalSpeed()
    {
        float baseSpeed = horizontal * horizontalSpeed;
        return onWall switch
        {
            Wall.left => baseSpeed < 0 ? 0 : baseSpeed,
            Wall.right => baseSpeed < 0 ? baseSpeed : 0,
            Wall.none => baseSpeed,
            _ => baseSpeed,
        };
    }

    public void SetSlowdownMultiplier(float multiplier)
    {
        activeSlowDownMultiplier = multiplier;
    }

    // Additional force to add when balloons pop, etc.
    public void SetAdditionalPassiveSlowdown(float passiveSlowdown)
    {
        this.additionalPassiveSlowdown = passiveSlowdown;
    }

    public void SetOnGround(bool onGround)
    {
        this.onGround = onGround;
    }

    public void SetOnWall(Wall wall)
    {
        this.onWall = wall;
    }

    /// <summary>
    ///  Disable movement when object is in destruction process
    /// </summary>
    public void DisableMovement()
    {
        horizontalSpeed = 0f;
        fallSpeed = 0f;
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        transform.parent.transform.position = pos;
    }
}