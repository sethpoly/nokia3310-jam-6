using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string title;
    public int collectedCoins = 0;

    public void CollectCoin(Coin coin)
    {
        Destroy(coin.gameObject);
        collectedCoins += 1;
    }
}
