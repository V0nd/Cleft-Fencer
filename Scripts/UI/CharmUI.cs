using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmUI : MonoBehaviour
{
    public GameObject selectedMark;
    public CharmObject charm;
    public IntValue playerSockets;

    // Start is called before the first frame update
    void Start()
    {
        selectedMark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if(selectedMark.activeInHierarchy)
        {
            playerSockets.currentValue -= charm.socketCost;
            selectedMark.SetActive(false);
        }
        else
        {
            playerSockets.currentValue += charm.socketCost;
            selectedMark.SetActive(true);
        }
    }
}
