using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform spawnLocation;
    public string title;
    public int collectedCoins = 0;
    private int totalCoinsInLevel;
    private Door endDoor;

    void Start()
    {
        totalCoinsInLevel = CountTotalCoinsInLevel();
        endDoor = FindObjectOfType<Door>();
    }

    // Count all gameobjects in scene with Coin tag & assign total count
    private int CountTotalCoinsInLevel()
    {
        var totalCoins = FindObjectsOfType<Coin>();
        return totalCoins.Length;
    }

    // Returns boolean if all coins are collected in level
    private bool CheckAllCoinsCollected()
    {
        return collectedCoins == totalCoinsInLevel;
    }

    private void AttemptDoorOpen()
    {
        if(CheckAllCoinsCollected())
        {
            endDoor.OpenDoor();
        }
    }

    public void CollectCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        collectedCoins += 1;
        AttemptDoorOpen();
    }
}
