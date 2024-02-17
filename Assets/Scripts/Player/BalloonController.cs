using UnityEngine;

public class BalloonController: MonoBehaviour 
{
    [SerializeField] private float slowdownMultiplier = .3f;
    private PlayerController playerController;
    private bool actionHeld = false;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        // If button is held, set slowdown in movement script
        playerController.SetSlowdownMultiplier(actionHeld ? slowdownMultiplier : 0f);
    }

    void Update()
    {
        actionHeld = Input.GetKey(KeyCode.Space);
    }
}