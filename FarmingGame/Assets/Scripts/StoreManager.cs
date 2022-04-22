using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;

    List<Plant> plantObjects = new List<Plant>();

    List<Plant> icePlants = new List<Plant>();

    void Awake()
    {
        var loadPlants =
            Resources.LoadAll("Normal Farm Plants", typeof (Plant));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((Plant) plant);
        }
        plantObjects.Sort (SortByPrice);

        foreach (var plant in plantObjects)
        {
            PlantItem newPlant =
                Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
        }

        if (
            SceneManager.GetActiveScene() ==
            SceneManager.GetSceneByName("Farm Two")
        )
        {
            var loadIcePlants =
                Resources.LoadAll("Ice Farm Plants", typeof (Plant));
            foreach (var plant in loadIcePlants)
            {
                icePlants.Add((Plant) plant);
            }
            icePlants.Sort (SortByPrice);

            foreach (var plant in icePlants)
            {
                PlantItem newPlant =
                    Instantiate(plantItem, transform).GetComponent<PlantItem>();
                newPlant.plant = plant;
            }
        }
    }

    int SortByPrice(Plant plantObjectOne, Plant plantObjectTwo)
    {
        return plantObjectOne.buyPrice.CompareTo(plantObjectTwo.buyPrice);
    }
}
