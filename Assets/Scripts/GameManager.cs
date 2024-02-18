using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private TransitionHandler transitionHandler;

    void Update()
    {
        // Testing
        if(Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(transitionHandler.LoadLevel(1));
        }
    }
}