using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Value", menuName = "Float Value")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in game")]
    public float currentValue;

    [Header("Value by default when starting")]
    public float initialValue;

    public void Restore(float amount)
    {
        currentValue += amount;
    }

    public void OnAfterDeserialize()
    {
        currentValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
