using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool showPressAnyKey = false;
    [SerializeField] private GameObject pressAnyKeyPrefab;

    private void Update() {
        pressAnyKeyPrefab.SetActive(showPressAnyKey);
    }
}
