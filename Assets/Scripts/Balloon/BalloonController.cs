using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BalloonController: MonoBehaviour 
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject balloonPref;
    [SerializeField] private int maxBalloonCount = 2;
    [SerializeField] private float activeSlowdownMultiplier = .8f;
    [SerializeField] private float passiveSlowdownPerBalloon = 600f;
    [SerializeField] private float secondsUntilBurst = 2f;
    [SerializeField] private GameObject balloonLocation;
    private PlayerController playerController;
    [SerializeField] private GameObject stringRenderer;
    private bool actionHeld = false;
    private List<GameObject> balloons = new();
    [SerializeField] private float burstTimer = 0f;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void Start()
    {
        InitializeBalloons();
    }

    void FixedUpdate()
    {
        // If button is held, set slowdown in movement script
        playerController.SetSlowdownMultiplier(actionHeld ? (activeSlowdownMultiplier * balloons.Count) : 0f);
    }

    void Update()
    {
        // Player input for holding balloon button to slow down
        BalloonInput();

        // Monitor balloon input and start/stop timers to burst balloons 
        BalloonBurstMonitor();
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

        Vector3 spawnPosition = balloonLocation.transform.position;
        spawnPosition.y += 2;
        Quaternion spawnRotation = playerTransform.rotation;

        GameObject balloon = Instantiate(balloonPref, spawnPosition, spawnRotation, balloonLocation.transform);
        balloons.Add(balloon);
        OnBalloonListChange();
    }

    private void BalloonInput()
    {
        actionHeld = Input.GetKey(KeyCode.Space);
    }

    private void BalloonBurstMonitor()
    {
        if(balloons.Count == 0) {
            return;
        }

        GameObject balloonGameObject = balloons.Last();
        Balloon balloon = balloonGameObject.GetComponent<Balloon>();

        if(actionHeld)
        {
            burstTimer += Time.deltaTime;

            // Shrink balloon
            Shrink(balloon);

            // Check if balloon should pop
            if(burstTimer >= secondsUntilBurst)
            {
                Pop(balloonGameObject);
            }
        }
        else if(burstTimer > 0) {
            burstTimer -= Time.deltaTime;
            // Inflate balloon
            Inflate(balloon);
        }
        else {
            burstTimer = 0f;
        }
    }

    private void Shrink(Balloon balloon)
    {
        float t = Mathf.Clamp01(burstTimer / secondsUntilBurst); // Normalized time

        // Interpolate between initial scale and min scale based on time
        float targetScale = Mathf.Lerp(balloon.startingScale.x, balloon.minScale, t);
        balloon.Shrink(targetScale);
    }

    private void Inflate(Balloon balloon)
    {
        float t = Mathf.Clamp01(Mathf.Abs(burstTimer / secondsUntilBurst - 1)); // Normalized time

        // Interpolate between initial scale and min scale based on time
        float targetScale = Mathf.Lerp(balloon.minScale, balloon.startingScale.x, t);
        balloon.Inflate(targetScale);
    }

    private void Pop(GameObject balloonObject)
    {
        Balloon balloon = balloonObject.GetComponent<Balloon>();
        if(balloons.Remove(balloonObject)) 
        {
            balloon.Pop();
            burstTimer = 0;
            OnBalloonListChange();
        }
    }

    private void OnBalloonListChange()
    {
        float totalPassiveSlowdown = balloons.Count * passiveSlowdownPerBalloon;
        playerController.SetAdditionalPassiveSlowdown(totalPassiveSlowdown);
    }

    /// <summary>
    /// Detach balloon so it floats upwards
    /// </summary>
    public void DetachBalloon()
    {
        Debug.Log("Detaching balloon");
        Destroy(stringRenderer);

        if(balloons.Count > 0) 
        {
            if(balloons.Last().TryGetComponent<Balloon>(out var balloon))
            {
                balloon.Detach();
            }   
        }
    }
}