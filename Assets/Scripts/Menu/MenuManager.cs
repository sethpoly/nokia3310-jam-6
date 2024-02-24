using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool showPressAnyKey = false;
    [SerializeField] private GameObject pressAnyKeyPrefab;

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
}
