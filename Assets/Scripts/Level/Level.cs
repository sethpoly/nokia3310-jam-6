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
        endDoor = FindObjectOfType<Door>();
    }

    void Update()
    {
        totalCoinsInLevel = CountTotalCoinsInLevel();
        AttemptDoorOpen();
    }

    // Count all gameobjects in scene with Coin tag & assign total count
    private int CountTotalCoinsInLevel()
    {
        int coinCount = FindObjectsOfType<Coin>().Length;
        return coinCount;
    }

    // Returns boolean if all coins are collected in level
    private bool CheckAllCoinsCollected()
    {
        return totalCoinsInLevel == 0;
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
