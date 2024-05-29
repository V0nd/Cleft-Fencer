using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool inventoryOpened;

    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
    }

    private void OpenInventory()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if(inventoryOpened)
            {
                Close();
            }
            else
            {
                Activate();
            }
        }
    }

    private void Activate()
    {
        SetEachChildren(true);
        inventoryOpened = true;
    }

    private void Close()
    {
        SetEachChildren(false);
        inventoryOpened = false;
    }

    private void SetEachChildren(bool b)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if(b)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}