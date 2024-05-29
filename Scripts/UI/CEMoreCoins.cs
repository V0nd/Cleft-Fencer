using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEMoreCoins : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        Effect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect()
    {
        playerData.money.currentValue += 100;
    }
}
