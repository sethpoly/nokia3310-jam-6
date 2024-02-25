using UnityEngine;

public class CoinCollected : MonoBehaviour 
{
    private void Start() {
        Destroy(gameObject.transform.parent.gameObject, .4f);
    }
}