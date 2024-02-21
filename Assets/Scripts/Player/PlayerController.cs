using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private PlayerMovement playerMovement;
    private PlayerCollision playerCollision;
    private Animator animator;

    void Awake()
    {
        // Get GameManager ref
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Animator ref
        animator = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerCollision.OnSpikeCollision += OnSpikeCollision;
        playerCollision.OnDoorCollision += OnDoorCollision;
        playerCollision.OnCoinCollision += OnCoinCollision;
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
        StartCoroutine(OnPlayerDestroyed());
    }

    private void OnDoorCollision()
    {
        Debug.Log("Collided with door");
        gameManager.NextLevel();
    }

    private void OnCoinCollision(Coin coin)
    {
        Debug.Log("Collided with coin");
        gameManager.CollectCoin(coin);
    }

    private IEnumerator OnPlayerDestroyed()
    {
        animator.Play("Player_Death");
        playerMovement.DisableMovement();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
