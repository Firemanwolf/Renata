using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Game/Item", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("Stats")]
    public string ItemName;
    public Buff buff;
}
