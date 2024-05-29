using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameObject merchantUI;
    bool canBuy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Buy();
    }

    private void Buy()
    {
        if (canBuy && Input.GetButtonDown("Interact"))
        {
            if (merchantUI.gameObject.activeInHierarchy)
            {
                merchantUI.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                merchantUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canBuy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBuy = false;
        }
    }
}
