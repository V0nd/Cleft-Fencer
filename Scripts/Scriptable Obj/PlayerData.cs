using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    public FloatValue health;
    public FloatValue temporaryHealth;
    public FloatValue mana;
    public FloatValue money;

    public int attackStrength;
}
