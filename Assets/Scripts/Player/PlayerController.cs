using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private PlayerMovement playerMovement;
    private PlayerCollision playerCollision;
    private BalloonController balloonController;
    private Animator animator;
    private bool deathStateOccurred = false;
    private bool invincible = false;
    private bool playedDoorClosedSound = false;

    void Awake()
    {
        // Get GameManager ref
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Animator ref
        animator = GetComponent<Animator>();

        // Balloon ref
        balloonController = GetComponentInChildren<BalloonController>();

        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerCollision.OnSpikeCollision += OnSpikeCollision;
        playerCollision.OnDoorCollision += OnDoorCollision;
        playerCollision.OnCoinCollision += OnCoinCollision;
        playerCollision.OnRandomDoorCollision += OnRandomDoorCollision;
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
        if(!deathStateOccurred && !invincible) 
        {
            deathStateOccurred = true;
            StartCoroutine(OnPlayerDestroyed());
        }
    }

    private void OnDoorCollision(Door door)
    {
        Debug.Log("Collided with door");

        if(door.isOpen)
        {
            invincible = true;
            gameManager.NextLevel();
        } 
        else
        {
            gameManager.SetRestartInterfaceVisibility(true);

            if(!playedDoorClosedSound)
            {
                playedDoorClosedSound = true;
                gameManager.PlaySound(Sound.doorCollisionWhileClosed);
            }
        }
    }

    private void OnCoinCollision(Coin coin)
    {
        Debug.Log("Collided with coin");
        gameManager.CollectCoin(coin);
    }

    private void OnRandomDoorCollision(Portal portal)
    {
        var portalPosition = portal.TeleportTo();
        if(portalPosition != null) 
        {
            playerMovement.SetPlayerPosition(portalPosition.transform.position);
            gameManager.PlaySound(Sound.teleport);
        }
    }

    private IEnumerator OnPlayerDestroyed()
    {
        animator.Play("Player_Death");
        playerMovement.DisableMovement();
        balloonController.DetachBalloon();
        gameManager.Screenshake();
        gameManager.OnPlayerDestroyed();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
