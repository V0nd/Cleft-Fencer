using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public CharmInventory charmventory;
    public BoolValue pickedItem;
    public Transform invTransform;

    [Header("Grid")]
    public float xStart;
    public float yStart;
    public int xSpaceBetweemCharms;
    public int numberOfColumns;
    public int ySpaceBetweenCharms;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAfterPickingItem();
    }

    private void UpdateAfterPickingItem()
    {
        if(pickedItem.currentValue)
        {
            UpdateDisplay();
            pickedItem.currentValue = false;
        }
    }

    private void UpdateDisplay()
    {
        for (int i = charmventory.ReturnCharmList().Count-1; i < charmventory.ReturnCharmList().Count; i++)
        {
            var obj = Instantiate(charmventory.ReturnCharmList()[i].charmData.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<CharmUI>().charm = charmventory.ReturnCharmList()[i].charmData;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.SetActive(false);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (xSpaceBetweemCharms * (i % numberOfColumns)), yStart + (-ySpaceBetweenCharms * (i / numberOfColumns)));
    }
}
