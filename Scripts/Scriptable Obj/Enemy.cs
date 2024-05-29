using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;

    public int speed;
    public int attackStrength;
    public int maxHealth;
    public int coinPrize;

    public void CoinDrop(GameObject coin, Transform spawnPoint)
    {
        //playerCoins.currentValue += coinPrize;
        for(int i = 0; i < coinPrize; ++i)
        {
            Instantiate(coin, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

