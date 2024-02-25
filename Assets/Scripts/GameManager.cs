using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    [SerializeField] private Screenshake screenshake;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject restartInterfacePrefab;
    [SerializeField] private GameObject rKeyPrefab;
    [SerializeField] private TransitionHandler transitionHandler;
    [SerializeField] private List<Level> levels = new();
    [SerializeField] private int currentLevelIndex = -1;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerStartingPosition;
    [SerializeField] private Quaternion playerStartingRotation;
    [SerializeField] private AudioClip playerDie;
    [SerializeField] private AudioClip balloonPop;
    [SerializeField] private AudioClip levelEnd;
    [SerializeField] private AudioClip coinCollected;
    [SerializeField] private AudioClip doorCollisionWhileClosed;
    [SerializeField] private AudioClip doorOpening;
    [SerializeField] private AudioClip teleport;

    [SerializeField] private int totalCollectedCoins = 0;
    private Level currentLevel;
    private AudioSource audioSource;
    private bool canRestart = true;
    private readonly float restartCooldown = .75f; // Cooldown time in seconds

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(levels.Count > 0)
        {
            // Start first level
             StartLevel(0);
        } else
        {
            Debug.LogError("Cannot start first level. No levels in list");
        }
    }

    void Update()
    {
        // Testing
        if(Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void NextLevel()
    {
        int nextIndex = currentLevelIndex + 1;

        if(levels.Count - 1 >= nextIndex)
        {
            StartLevel(nextIndex);
        } else // Game must be over
        {
            Debug.Log("No next level found. Loading Credits");
            LoadCreditsScene();
        }
    }

    public void RestartLevel()
    {
        if(!canRestart) return;
        if (levels.Count - 1 >= currentLevelIndex) 
        {
            StartCoroutine(StartRestartCooldown());
            StartCoroutine(transitionHandler.LoadLevel(onCompletion: () => {
                var playerSpawnLocation = currentLevel.spawnLocation.position;
                DisposeLevel(currentLevelIndex);
                RespawnPlayer(playerSpawnLocation);

                Level level = levels[currentLevelIndex];
                var levelInstance = Instantiate(level);
                player.transform.position = levelInstance.spawnLocation.position;
                currentLevel = levelInstance;
                Debug.Log("Restarting level " + level.title);
            }));
        } else
        {
            Debug.LogError("Cannot restart level. No level found");
        }
    }

    private void StartLevel(int levelIndex)
    {
        if(levels.Count - 1 < levelIndex) 
        {
            Debug.LogError("Cannot start level with index " + levelIndex);
            return;
        }
        StartCoroutine(transitionHandler.LoadLevel(onCompletion: () => {
            // Secure collected coins
            SaveCollectedCoinsFromCurrentLevel();

            // Get next player spawn location
            if(currentLevel != null)
            {
                playerStartingPosition = currentLevel.spawnLocation.position;
            }

            // Destroy level just completed
            DisposeLevel(currentLevelIndex);

            // Reset player incase balloons were popped
            RespawnPlayer(playerStartingPosition);

            Level level = levels[levelIndex];
            var levelInstance = Instantiate(level);
            player.transform.position = levelInstance.spawnLocation.position;
            currentLevelIndex = levelIndex;
            currentLevel = levelInstance;
            Debug.Log("Starting level " + level.title);
        }));
    }

    private void DisposeLevel(int levelIndex)
    {
        if(levels.ElementAtOrDefault(levelIndex) == null) 
        {
            Debug.Log("Cannot dispose level with index " + levelIndex);
            return;
        }
        Destroy(currentLevel.gameObject);
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Balloon")) {
            Destroy(gameObject);
        }
        Debug.Log("Disposed level");
    }

    private void RespawnPlayer(Vector2 spawnLocation) 
    {
        Destroy(player);
        GameObject newPlayer = Instantiate(playerPrefab, spawnLocation, playerStartingRotation);
        player = newPlayer;
        playerStartingRotation = player.transform.rotation;

        // Hide restart interface
        SetRestartInterfaceVisibility(value: false);

        PlaySound(Sound.levelEnd);
    }

    // Increments the collected coin count for the current level
    //
    // This value will be reset whenever the level is restarted
    public void CollectCoin(Coin coin)
    {
        currentLevel.CollectCoin(coin);
        if(coin != null)
        {
            coin.OnCollect();
            PlaySound(Sound.coinCollected);
        }
    }

    private void SaveCollectedCoinsFromCurrentLevel()
    {
        if(currentLevel != null) 
        {
            totalCollectedCoins += currentLevel.collectedCoins;
        } else 
        {
            Debug.Log("Cannot save collected coins. No level found");
        }
    }

    public void SetRestartInterfaceVisibility(bool value)
    {
        var existingInterface = GameObject.FindGameObjectWithTag("RestartInterface");
        if(existingInterface != null)
        {
            Destroy(existingInterface);
        }
        var existingRKey = GameObject.FindGameObjectWithTag("R_Key");
        if(existingRKey != null)
        {
            Destroy(existingRKey);
        }
        if(value)
        {
            Instantiate(restartInterfacePrefab);
            Instantiate(rKeyPrefab);
        }
    }

    public void OnPlayerDestroyed()
    {
        SetRestartInterfaceVisibility(value: true);
        PlaySound(Sound.playerDie);
    }

    public void Screenshake() 
    {
        StartCoroutine(screenshake.Shake(duration: .1f, magnitude: 1.2f));
    }

    public void PlaySound(Sound sound)
    {
        AudioClip clip = GetAudioClip(sound);
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        return sound switch
        {
            Sound.playerDie => playerDie,
            Sound.balloonPop => balloonPop,
            Sound.levelEnd => levelEnd,
            Sound.coinCollected => coinCollected,
            Sound.doorCollisionWhileClosed => doorCollisionWhileClosed,
            Sound.doorOpening => doorOpening,
            Sound.teleport => teleport,
            _ => balloonPop,
        };
    }

    private void LoadCreditsScene()
    {
        StartCoroutine(transitionHandler.LoadLevel(onCompletion: () => {
            SceneManager.LoadScene("Credits");
        }));
    }

    private IEnumerator StartRestartCooldown()
    {
        canRestart = false;
        yield return new WaitForSeconds(restartCooldown);
        canRestart = true;
    }
}