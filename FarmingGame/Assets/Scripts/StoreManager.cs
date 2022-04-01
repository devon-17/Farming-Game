using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<Plant> plantObjects = new List<Plant>();

    void Awake()
    {
        var loadPlants = Resources.LoadAll("Plants", typeof(Plant));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((Plant)plant);
        }
        plantObjects.Sort(SortByPrice);

        foreach (var plant in plantObjects)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
        }
    }

    int SortByPrice(Plant plantObjectOne, Plant plantObjectTwo)
    {
        return plantObjectOne.buyPrice.CompareTo(plantObjectTwo.buyPrice);
    }
}
