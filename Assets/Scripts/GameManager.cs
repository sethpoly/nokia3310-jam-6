using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TransitionHandler transitionHandler;
    [SerializeField] private List<Level> levels = new();
    [SerializeField] private int currentLevelIndex = -1;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerStartingPosition;
    private Level currentLevel;

    void Awake()
    {
        playerStartingPosition = player.transform.position;
        if(levels.Count > 0)
        {
             NextLevel();
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
                DisposeLevel(currentLevelIndex);
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
            StartLevel(currentLevelIndex, onTransitionStart: () => {
                Debug.Log("Restarting current level: " + levels[currentLevelIndex].title);
                DisposeLevel(currentLevelIndex);
            });
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
        Level level = levels[levelIndex];
        StartCoroutine(transitionHandler.LoadLevel(1, onCompletion: () => {
            onTransitionStart.Invoke();
            var levelInstance = Instantiate(level);
            player.transform.position = playerStartingPosition;
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
    }
}