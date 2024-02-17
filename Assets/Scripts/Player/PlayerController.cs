using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetSlowdownMultiplier(float multiplier)
    {
        playerMovement.SetSlowdownMultiplier(multiplier);
    }
    
}
