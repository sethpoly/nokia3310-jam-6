using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TransitionHandler transitionHandler;
    [SerializeField] private List<Level> levels = new();
    [SerializeField] private int currentLevelIndex;

    void Awake()
    {
        if(levels.Count > 0)
        {
             currentLevelIndex = 0;
             RestartLevel();
        } else
        {
            Debug.LogError("Cannot start first level. No levels in list");
        }
    }

    void Update()
    {
        // Testing
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(transitionHandler.LoadLevel(1));
        }
    }

    public void NextLevel()
    {
        DisposeLevel(levels[currentLevelIndex]);
        int nextIndex = currentLevelIndex + 1;

        if(levels.Count -1 >= nextIndex)
        {
            StartLevel(levels[nextIndex]);
            currentLevelIndex = nextIndex;
        } else
        {
            Debug.LogError("No next level found");
        }
    }

    public void RestartLevel()
    {
        if (levels.Count - 1 >= currentLevelIndex) 
        {
            Debug.Log("Restarting current level: " + levels[currentLevelIndex].title);
            DisposeLevel(levels[currentLevelIndex]);
            StartLevel(levels[currentLevelIndex]);
        } else
        {
            Debug.LogError("Cannot restart level. No level found");
        }
    }

    private void StartLevel(Level level)
    {
        Debug.Log("Starting level " + level.title);
        Instantiate(level);
    }

    private void DisposeLevel(Level level)
    {
        Debug.Log("Disposing level " + level.title);
        Destroy(level.gameObject);
    }
}