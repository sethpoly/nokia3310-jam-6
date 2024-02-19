using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private PlayerMovement playerMovement;
    private PlayerCollision playerCollision;

    void Awake()
    {
        // Get GameManager ref
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerCollision.OnSpikeCollision += OnSpikeCollision;
        playerCollision.OnDoorCollision += OnDoorCollision;
    }

    void Update()
    {
        playerMovement.SetOnGround(playerCollision.onGround);
        playerMovement.SetOnWall(playerCollision.wallSide);
    }

    public void SetSlowdownMultiplier(float multiplier)
    {
        playerMovement.SetSlowdownMultiplier(multiplier);
    }

    public void SetAdditionalPassiveSlowdown(float passiveSlowdown)
    {
        playerMovement.SetAdditionalPassiveSlowdown(passiveSlowdown);
    }

    private void OnSpikeCollision() 
    {
        Debug.Log("Collided with spike");
        Destroy(gameObject);
    }

    private void OnDoorCollision()
    {
        Debug.Log("Collided with door");
        gameManager.NextLevel();
    }
}
