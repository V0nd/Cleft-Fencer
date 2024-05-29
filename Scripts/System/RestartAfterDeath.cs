using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAfterDeath : MonoBehaviour
{
    public PlayerData player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RestartScene();
    }

    void RestartScene()
    {
        if(player.health.currentValue <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            player.mana.currentValue = player.mana.initialValue;
            player.health.currentValue = player.health.initialValue;
            player.money.currentValue = player.money.initialValue;
        }
    }
}
