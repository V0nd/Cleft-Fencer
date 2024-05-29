using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IntValue", menuName = "IntValue")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in game")]
    public int currentValue;

    [Header("Value by default when starting")]
    public int initialValue;

    public void Restore(int amount)
    {
        currentValue += amount;
    }

    public void OnAfterDeserialize()
    {
        currentValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
