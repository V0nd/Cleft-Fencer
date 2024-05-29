using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bool Value", menuName = "Bool Value")]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in game")]
    public bool currentValue;

    [Header("Value by default when starting")]
    public bool initialValue = false;

    public void OnAfterDeserialize()
    {
        currentValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
