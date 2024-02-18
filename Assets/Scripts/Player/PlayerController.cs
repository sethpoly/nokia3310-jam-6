using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCollision playerCollision;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerCollision.OnSpikeCollision += OnSpikeCollision;
    }

    void Update()
    {
        playerMovement.SetOnGround(playerCollision.onGround);
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
        Destroy(this.gameObject);
    }
}
