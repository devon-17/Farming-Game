using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class Plant : ScriptableObject
{
    public string plantName;
    public Sprite[] plantStages;
    public float timeBetweenStages;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
    public Sprite plantedWhileDry;
}
