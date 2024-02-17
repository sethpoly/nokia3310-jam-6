using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonController: MonoBehaviour 
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject balloonPref;
    [SerializeField] private int maxBalloonCount = 2;
    [SerializeField] private float slowdownMultiplier = .3f;
    private PlayerController playerController;
    private bool actionHeld = false;
    private List<GameObject> balloons = new();

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        InitializeBalloons();
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

    private void InitializeBalloons() 
    {
        for(int i = 0; i < maxBalloonCount; i++)
        {
            AddBalloon();
        }
    }

    // Adds a balloon to the player
    // Offsets the balloon position and rotation based on current balloon list count
    private void AddBalloon() {
        int currentBalloonCount = balloons.Count;
        if(currentBalloonCount >= maxBalloonCount)
        {
            Debug.Log("Cannot add another balloon. Max balloons reached.");
            return;
        }

        float offset = currentBalloonCount > 0 ? .5f : -.5f;
        float rotationOffset = currentBalloonCount > 0 ? -18f : 18f;
        Vector3 spawnPosition = playerTransform.position;
        Quaternion spawnRotation = playerTransform.rotation;

        spawnPosition.x += offset; 
        spawnPosition.y += 1;
        GameObject balloon = Instantiate(balloonPref, spawnPosition, spawnRotation, playerTransform);
        balloon.transform.Rotate(new(spawnRotation.x, spawnRotation.y, rotationOffset));
        balloons.Add(balloon);
    }
}