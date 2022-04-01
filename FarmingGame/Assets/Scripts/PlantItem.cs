using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantItem : MonoBehaviour
{
    public Plant plant;
    public Text nameText;
    public Text priceText;
    public Image icon;
    public Text buttonText;
    public int totalGrowTime;
    public Text growTimeText;

    // Start is called before the first frame update
    void Start()
    {
        totalGrowTime = Mathf.RoundToInt(plant.timeBetweenStages) * plant.plantStages.Length; // getting total grow time
        IntializeUI();
    }

    public void BuyPlant()
    {
        Debug.Log("Bought " + plant.plantName);
        FarmManager.instance.SelectPlant(this);
    }

    void IntializeUI()
    {
        nameText.text = plant.plantName;
        priceText.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
        growTimeText.text = "Grow Time: " + "\n" + totalGrowTime + " Second's";
    }
}
