using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Charm", menuName = "Charm")]
public class CharmObject : ScriptableObject
{
    public new string name;

    public int socketCost;

    public GameObject prefab;
}
