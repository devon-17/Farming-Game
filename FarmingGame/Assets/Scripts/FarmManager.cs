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

    [Header("Next Farm")]
    public bool isNextFarmBought;

    public GameObject continueButton;

    public GameObject nextFarmButton;

    [Header("Farm Two")]
    public bool isFreePlayReached = false;

    public GameObject gameOverPanel;

    public GameObject harvestButton;

    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        // checking for tutorial
        if (
            SceneManager.GetActiveScene() !=
            SceneManager.GetSceneByName("Tutorial")
        )
        {
            // if there is a key for the money
            if (PlayerPrefs.HasKey("_money"))
            {
                money = PlayerPrefs.GetInt("_money");
                Debug.Log("Money: " + PlayerPrefs.GetInt("_money"));
            }
            else
            {
                PlayerPrefs.SetInt("_money", money);
            }
        }

        // if we are on farm one
        if (
            SceneManager.GetActiveScene() ==
            SceneManager.GetSceneByName("Farm One")
        )
        {
            // if we have the key for next farm int/bool
            if (PlayerPrefs.HasKey("_next_farm"))
            {
                // if int = 1 which means true
                if (PlayerPrefs.GetInt("_next_farm") == 1)
                {
                    continueButton.SetActive(true);
                    nextFarmButton.SetActive(false);
                }
                else if (
                    PlayerPrefs.GetInt("_next_farm") == 0 // if int = 0 which means true
                )
                {
                    continueButton.SetActive(false);
                    nextFarmButton.SetActive(true);
                }
            } // we dont have the key saved in player prefs
            else
            {
                isNextFarmBought = false;
                continueButton.SetActive(false);
                nextFarmButton.SetActive(true);
                PlayerPrefs.SetInt("_next_farm", BoolToInt(isNextFarmBought));
            }
        }

        instance = this;
        moneyText.text = "$" + money;
    }

    void Update()
    {
        if (
            SceneManager.GetActiveScene() ==
            SceneManager.GetSceneByName("Farm Two")
        )
        {
            if (money >= 150)
            {
                isFreePlayReached = true;
            }
        }

        if (isFreePlayReached)
        {
            gameOverPanel.SetActive(true);
            harvestButton.SetActive(false);
            backButton.SetActive(false);
        }
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

        Debug.Log (toolNumber);
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
        PlayerPrefs.SetInt("_money", money);
    }

    public void BuyNewFarm()
    {
        if (money >= farmTwoCost)
        {
            Debug.Log("Next Farm");
            Transaction(-farmTwoCost);
            PlayerPrefs.SetInt("_money", money);
            SceneTransitions.instance.OpenScene();

            // setting for player prefs
            isNextFarmBought = true;
            PlayerPrefs.SetInt("_next_farm", BoolToInt(isNextFarmBought));
        }
        else
        {
            Debug.Log("Dont Have Enough Money");
        }
    }

    public int BoolToInt(bool value)
    {
        // if the bool is false set int to 0
        if (!value)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public void CloseEverything()
    {
        gameOverPanel.SetActive(false);
        harvestButton.SetActive(true);
        backButton.SetActive(true);
    }
}
