using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool showPressAnyKey = false;
    [SerializeField] private GameObject pressAnyKeyPrefab;
    [SerializeField] private AudioSource menuMusic;
    public float startButtonDelayInSeconds = 9f;

    private void Start() 
    {
        // Enable any key after portion of animation finishes
        StartCoroutine(EnableStartButtonAfterDelay());
        menuMusic.Play();
    }

    private void Update() {
        pressAnyKeyPrefab.SetActive(showPressAnyKey);

        if(AnyKeyPressed())
        {
            SceneManager.LoadScene("Game");
        }
    }

    private bool AnyKeyPressed()
    {
        if(!showPressAnyKey) return false;
        return Input.anyKey;
    }

    private IEnumerator EnableStartButtonAfterDelay()
    {
        yield return new WaitForSeconds(startButtonDelayInSeconds);
        showPressAnyKey = true;
    }
}
