using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public GameObject gameCanvas;
    public Text tutorialText;
    public Text buttonText;
    public bool isWatered, isPlanted, isHarvested;
    public int currentStep;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentStep = 0;
        gameCanvas.SetActive(false);
        gameObject.SetActive(true);
        isWatered = false;
        isPlanted = false;
        isHarvested = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isWatered = true;
            if (gameCanvas.activeInHierarchy)
            {
                gameCanvas.SetActive(false);
                gameObject.SetActive(true);
            }
            else
            {
                gameCanvas.SetActive(false);
                gameCanvas.SetActive(true);
            }
        }

        if (currentStep == 0)
        {
            tutorialText.text = "Welcome to the tutorial!";
            buttonText.text = "Next";
        }
        else if (currentStep == 1)
        {
            buttonText.text = "Close & Try";
            tutorialText.text = "Water a Plot By Clicking" + '\n' + "The Water Icon Button" + '\n' + "Then Clicking On A Plot";
        }
        else if (currentStep == 2)
        {
            tutorialText.text = "Now Plant Something By Clicking" + '\n' + "On The 'Buy' Button" + '\n' + "Of The Plant You Want";
        }

        if (isWatered)
        {
            Debug.Log("Step One Done");
            currentStep = 3;
            gameCanvas.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void HideTutorial()
    {
        currentStep++;
        if (buttonText.text != "Next")
        {
            gameCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void CheckingBools()
    {

    }
}
