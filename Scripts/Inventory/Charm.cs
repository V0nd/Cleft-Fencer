using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
    public CharmObject charmData;
    public CharmInventory charmList;
    public BoolValue pickedItem;

    private bool enteredArea;
    public Charm prefab; //Unity doesn't accept this item to inventory because it is not prefab,
                         //so I took original prefab from the assets folder and put it into inventory instead
                         //the prefab is assigned the sama charmData as this one in AssingPrefabThisData()

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
    }

    private void PickUp()
    {
        if(enteredArea && Input.GetButtonDown("Interact"))
        {
            AssignPrefabThisData();
            charmList.AddCharm(prefab);
            pickedItem.currentValue = true;
            this.gameObject.SetActive(false);
        }
    }

    private void AssignPrefabThisData()
    {
        prefab.charmData = this.charmData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enteredArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enteredArea = false;
        }
    }
}