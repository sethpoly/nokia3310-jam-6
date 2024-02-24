using UnityEngine;

public class CreditsManager : MonoBehaviour 
{
    public AudioSource gameCompleteSound;

    private void Start() 
    {
        gameCompleteSound.Play();
    }
}