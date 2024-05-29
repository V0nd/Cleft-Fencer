using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Charm Inventory", menuName = "Charm Inventory")]
public class CharmInventory : ScriptableObject, ISerializationCallbackReceiver
{
    public List<Charm> charmList;

    public List<Charm> ReturnCharmList()
    {
        return charmList;
    }

    public void AddCharm(Charm charm)
    {
        charmList.Add(charm);
    }

    public void ClearCharmventory()
    {
        charmList.Clear();
    }

    public void OnAfterDeserialize()
    {
        ClearCharmventory();
    }

    public void OnBeforeSerialize() { }
}