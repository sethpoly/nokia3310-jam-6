using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private TransitionHandler transitionHandler;
    [SerializeField] private List<Level> levels = new();
    [SerializeField] private int currentLevelIndex = -1;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerStartingPosition;
    [SerializeField] private Quaternion playerStartingRotation;
    [SerializeField] private int totalCollectedCoins = 0;
    private Level currentLevel;

    void Awake()
    {
        playerStartingPosition = player.transform.position;
        playerStartingRotation = player.transform.rotation;
        if(levels.Count > 0)
        {
             StartLevel(0, () => {});
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
            StartLevel(nextIndex, () => {
                // Secure collected coins
                SaveCollectedCoinsFromCurrentLevel();

                // Get next player spawn location
                var playerSpawnLocation = currentLevel.spawnLocation;

                // Destroy level just completed
                DisposeLevel(currentLevelIndex);

                // Reset player incase balloons were popped
                RespawnPlayer(playerSpawnLocation.position);
            });
        } else
        {
            Debug.LogError("No next level found");
        }
    }

    public void RestartLevel()
    {
        if (levels.Count - 1 >= currentLevelIndex) 
        {
            StartCoroutine(transitionHandler.LoadLevel(1, onCompletion: () => {
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

    private void StartLevel(int levelIndex, Action onTransitionStart)
    {
        if(levels.Count - 1 < levelIndex) 
        {
            Debug.LogError("Cannot start level with index " + levelIndex);
            return;
        }
        StartCoroutine(transitionHandler.LoadLevel(1, onCompletion: () => {
            onTransitionStart.Invoke();
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
        Debug.Log("Disposed level");
    }

    private void RespawnPlayer(Vector2 spawnLocation) 
    {
        Destroy(player);
        GameObject newPlayer = Instantiate(playerPrefab, spawnLocation, playerStartingRotation);
        player = newPlayer;
        playerStartingPosition = player.transform.position;
        playerStartingRotation = player.transform.rotation;
    }

    // Increments the collected coin count for the current level
    //
    // This value will be reset whenever the level is restarted
    public void CollectCoin(Coin coin)
    {
        currentLevel.CollectCoin(coin);
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
}