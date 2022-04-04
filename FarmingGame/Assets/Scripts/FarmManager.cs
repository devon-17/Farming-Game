using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public static FarmManager instance;

    [HideInInspector]
    public PlantItem selectedPlant;

    [HideInInspector]
    public bool isPlantSelected;

    [Header("Money/Cost")]
    public int money;

    public int farmTwoCost;

    public Text moneyText;

    [Header("Tools")]
    [HideInInspector]
    public bool isToolSelected = false;

    public int selectedTool = 0; // 1-water, 2-fertilizer, 3-hoe

    public Image[] buttonImages;

    public Sprite originalButton;

    public Sprite selectedButton;

    [Header("Cursor")]
    public Image cursor;

    [Header("New Farm's")]
    public GameObject newFarmButton;
    public Text notificationText;
    public GameObject notificationSpawn;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        instance = this;
        moneyText.text = "$" + money;
    }

    public void SelectPlant(PlantItem newPlant)
    {
        if (selectedPlant == newPlant)
        {
            CheckSelection();
        }
        else
        {
            CheckSelection();

            selectedPlant = newPlant;
            isPlantSelected = true;
            Debug.Log("Selected " + selectedPlant.plant.plantName);
            selectedPlant.buttonText.text = "Cancel";
            // selectedPlant.buttonText.color = cancelColor;
        }
    }

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            // deselect
            CheckSelection();
        }
        else
        {
            // select tool
            CheckSelection();
            isToolSelected = true;
            selectedTool = toolNumber;
            buttonImages[toolNumber - 1].sprite = selectedButton;
        }

        Debug.Log(toolNumber);
    }

    public void CheckSelection()
    {
        if (isPlantSelected)
        {
            isPlantSelected = false;

            // there is a plant selected
            if (selectedPlant)
            {
                selectedPlant.buttonText.text = "Buy";
                selectedPlant = null;
            }
        }
        else if (isToolSelected)
        {
            if (selectedTool > 0)
            {
                buttonImages[selectedTool - 1].sprite = originalButton;
            }
            isToolSelected = false;
            selectedTool = 0;
        }
    }

    public void Transaction(int value)
    {
        money += value; // adding value to money
        moneyText.text = "$" + money;
    }

    public void BuyNewFarm(string newFarm)
    {
        int difference = farmTwoCost - money;
        if (money >= farmTwoCost)
        {
            Debug.Log("Next Farm");
            // SceneManager.LoadScene (newFarm);
        }
        else
        {
            Instantiate(notificationText, notificationSpawn.transform.position, Quaternion.identity);
            notificationText.text = "You Need $" + difference + " more";
            Debug.LogError("Dont Have Enough Money");
        }
    }
}
